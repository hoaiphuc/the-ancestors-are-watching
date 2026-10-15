using UnityEngine;

namespace AncestorsWatching
{
    /// <summary>
    /// Stats for one enemy type. A new enemy is a new asset, not new code.
    /// </summary>
    [CreateAssetMenu(menuName = "Ancestors/Enemy Data", fileName = "NewEnemy")]
    public class EnemyData : ScriptableObject
    {
        [SerializeField] private string displayName = "Restless Spirit";
        [SerializeField] private double maxHealth = 10;
        [SerializeField] private float moveSpeed = 1.5f;
        [SerializeField] private double contactDamage = 5;
        [SerializeField] private double spiritStoneReward = 1.0f;

        // Optional: leave the sprite empty to keep whatever the prefab uses.
        [SerializeField] private Sprite sprite;
        [SerializeField] private Color tint = new Color(0.85f, 0.2f, 0.2f);

        public string DisplayName => displayName;
        public double MaxHealth => maxHealth;
        public float MoveSpeed => moveSpeed;
        public double ContactDamage => contactDamage;
        public double SpiritStoneReward => spiritStoneReward;
        public Sprite Sprite => sprite;
        public Color Tint => tint;
    }
}
