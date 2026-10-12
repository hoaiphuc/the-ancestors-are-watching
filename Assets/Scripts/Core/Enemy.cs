using System;
using UnityEngine;

namespace AncestorsWatching
{
    /// <summary>
    /// Walks toward the Kin and hurts him on contact. Never destroys itself: it raises
    /// Finished and lets whoever owns it (the pool) decide what happens next.
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public class Enemy : MonoBehaviour
    {
        // Depends on the Kin sprite size; kept here so the enemy stops at his edge, not his centre.
        [SerializeField] private float reachDistance = 0.5f;

        private SpriteRenderer spriteRenderer;
        private EnemyData data;
        private Kin target;
        private double health;
        private bool finished;

        /// <summary>Killed by the player. Used later for Spirit Stone drops.</summary>
        public event Action<Enemy> Died;

        /// <summary>Left the field for any reason (killed or reached the Kin). The pool listens to this.</summary>
        public event Action<Enemy> Finished;

        private void Awake() => spriteRenderer = GetComponent<SpriteRenderer>();

        /// <summary>Pooled objects are reused, so all per-life state is reset here, not in Awake.</summary>
        public void Initialize(EnemyData enemyData, Kin kin, Vector3 position)
        {
            data = enemyData;
            target = kin;
            health = data.MaxHealth;
            finished = false;

            transform.position = position;
            spriteRenderer.color = data.Tint;
            if (data.Sprite != null) spriteRenderer.sprite = data.Sprite;
        }

        private void Update()
        {
            var destination = target.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, destination, data.MoveSpeed * Time.deltaTime);

            if ((destination - transform.position).sqrMagnitude <= reachDistance * reachDistance)
                Reach();
        }

        public void TakeDamage(double amount)
        {
            if (finished) return;

            health -= amount;
            if (health > 0) return;

            Died?.Invoke(this);
            Finish();
        }

        private void Reach()
        {
            // Leave before dealing damage: if this hit kills the Kin, the spawner clears
            // every active enemy, and this one must not be released a second time.
            Finish();
            target.TakeDamage(data.ContactDamage);
        }

        private void Finish()
        {
            finished = true;
            Finished?.Invoke(this);
        }

        // Temporary: nothing can click enemies until step 3, so test the pool with this.
        [ContextMenu("Debug: Kill")]
        private void DebugKill() => TakeDamage(health);
    }
}
