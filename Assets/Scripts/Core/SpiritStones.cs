using System;
using UnityEngine;

namespace AncestorsWatching
{
    /// <summary>
    /// The player's permanent currency. Only stores and changes the amount: earning
    /// (SpiritStoneDrops) and spending (upgrades) live elsewhere so each can change on its own.
    /// </summary>
    public class SpiritStones : MonoBehaviour
    {
        public double Amount { get; private set; }

        /// <summary>(new amount)</summary>
        public event Action<double> Changed;

        public void Add(double amount)
        {
            if (amount <= 0) return;
            SetAmount(Amount + amount);
        }

        /// <summary>Spends only if there is enough, so the balance can never go negative.</summary>
        public bool TrySpend(double amount)
        {
            if (amount < 0 || amount > Amount) return false;

            SetAmount(Amount - amount);
            return true;
        }

        private void SetAmount(double value)
        {
            Amount = value;
            Changed?.Invoke(Amount);
        }

        // Temporary: lets us test the HUD and upgrades without farming kills.
        [ContextMenu("Debug: Add 100")]
        private void DebugAdd() => Add(100);
    }
}
