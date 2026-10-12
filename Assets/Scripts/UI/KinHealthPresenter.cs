using UnityEngine;

namespace AncestorsWatching
{
    /// <summary>Connects the Kin's health event to its bar, so Kin itself never touches UI.</summary>
    public class KinHealthPresenter : MonoBehaviour
    {
        [SerializeField] private Kin kin;
        [SerializeField] private BarView healthBar;

        private void OnEnable() => kin.HealthChanged += OnHealthChanged;

        private void OnDisable() => kin.HealthChanged -= OnHealthChanged;

        private void OnHealthChanged(double current, double max) => healthBar.SetRatio((float)(current / max));
    }
}
