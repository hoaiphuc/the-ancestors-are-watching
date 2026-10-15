using TMPro;
using UnityEngine;

namespace AncestorsWatching
{
    /// <summary>Shows the Spirit Stone total. Only listens to the wallet and never changes it.</summary>
    public class SpiritStoneHud : MonoBehaviour
    {
        [SerializeField] private SpiritStones wallet;
        [SerializeField] private TMP_Text label;
        [SerializeField] private string prefix = "Spirit Stones: ";

        private void OnEnable()
        {
            wallet.Changed += OnChanged;
            // Show the current value too, so it doesn't matter which object starts first.
            OnChanged(wallet.Amount);
        }

        private void OnDisable() => wallet.Changed -= OnChanged;

        private void OnChanged(double amount) => label.text = prefix + NumberFormatter.Format(amount);
    }
}
