// using UnityEngine;

// namespace AncestorsWatching
// {
//     /// <summary>
//     /// Briefly turns the enemy's sprite a flash colour when it takes damage.
//     /// Uses a timer instead of a coroutine so a pooled enemy can never come back stuck on white.
//     /// </summary>
//     [RequireComponent(typeof(Enemy), typeof(SpriteRenderer))]
//     public class HitFlash : MonoBehaviour
//     {
//         [SerializeField] private float duration = 0.1f;
//         [SerializeField] private Color flashColor = Color.white;

//         private Enemy enemy;
//         private SpriteRenderer spriteRenderer;
//         private Color baseColor;
//         private float remaining;
//         private bool flashing;

//         private void Awake()
//         {
//             enemy = GetComponent<Enemy>();
//             spriteRenderer = GetComponent<SpriteRenderer>();
//         }

//         private void OnEnable() => enemy.Damaged += OnDamaged;

//         private void OnDisable()
//         {
//             enemy.Damaged -= OnDamaged;
//             // Returning to the pool mid-flash must not leave the sprite white.
//             EndFlash();
//         }

//         private void Update()
//         {
//             if (!flashing) return;

//             remaining -= Time.deltaTime;
//             if (remaining <= 0f) EndFlash();
//         }

//         private void OnDamaged(Enemy damaged, double amount)
//         {
//             // Save the colour only when a flash begins, so a second hit mid-flash doesn't store white as the base.
//             if (!flashing) baseColor = spriteRenderer.color;

//             flashing = true;
//             remaining = duration;
//             spriteRenderer.color = flashColor;
//         }

//         private void EndFlash()
//         {
//             if (!flashing) return;

//             flashing = false;
//             spriteRenderer.color = baseColor;
//         }
//     }
// }
