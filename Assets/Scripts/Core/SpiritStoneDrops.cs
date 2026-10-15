using UnityEngine;

namespace AncestorsWatching
{
    /// <summary>
    /// Pays out an enemy's Spirit Stone reward when the player kills it.
    /// Kept apart from the wallet so the earning rule, and later the gain upgrade, has one home.
    /// </summary>
    public class SpiritStoneDrops : MonoBehaviour
    {
        [SerializeField] private EnemySpawner enemySpawner;
        [SerializeField] private SpiritStones wallet;

        private void OnEnable() => enemySpawner.EnemyDied += OnEnemyDied;

        private void OnDisable() => enemySpawner.EnemyDied -= OnEnemyDied;

        private void OnEnemyDied(Enemy enemy) => wallet.Add(enemy.Data.SpiritStoneReward);
    }
}
