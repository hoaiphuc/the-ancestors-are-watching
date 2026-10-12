using System;
using UnityEngine;

namespace AncestorsWatching
{
    /// <summary>
    /// The injured kin the player protects. Health doubles as his recovery: he starts each run
    /// nearly dead and slowly heals, and healing to full means a breakthrough to the next realm.
    /// Knows nothing about the UI; anything that cares listens to the events instead.
    /// </summary>
    public class Kin : MonoBehaviour
    {
        // Doubles because incremental games outgrow float precision quickly.
        [SerializeField] private double maxHealth = 100;
        [SerializeField] private double startHealth = 1;
        [SerializeField] private double regenPerSecond = 1;

        private bool breakthroughRaised;

        public double Health { get; private set; }
        public bool IsAlive => Health > 0;

        /// <summary>(current, max)</summary>
        public event Action<double, double> HealthChanged;
        public event Action Died;
        public event Action BreakthroughReady;

        private void Start()
        {
            // Start rather than Awake so listeners subscribed in OnEnable receive the initial values.
            ResetForRun();
        }

        private void Update()
        {
            if (!IsAlive || Health >= maxHealth) return;
            SetHealth(Health + regenPerSecond * Time.deltaTime);
        }

        public void ResetForRun()
        {
            breakthroughRaised = false;
            SetHealth(startHealth);
        }

        public void TakeDamage(double amount)
        {
            if (!IsAlive || amount <= 0) return;

            SetHealth(Health - amount);
            if (!IsAlive) Died?.Invoke();
        }

        private void SetHealth(double value)
        {
            Health = Math.Clamp(value, 0, maxHealth);
            HealthChanged?.Invoke(Health, maxHealth);

            // Once per run, even if he is hurt and heals back to full again.
            if (Health >= maxHealth && !breakthroughRaised)
            {
                breakthroughRaised = true;
                BreakthroughReady?.Invoke();
            }
        }

        // Temporary: lets us watch the Health bar move before enemies exist.
        [ContextMenu("Debug: Take 10 Damage")]
        private void DebugTakeDamage() => TakeDamage(10);

        // Temporary: with a 1 HP start, damage tests need some health to lose first.
        [ContextMenu("Debug: Heal 50")]
        private void DebugHeal() => SetHealth(Health + 50);
    }
}
