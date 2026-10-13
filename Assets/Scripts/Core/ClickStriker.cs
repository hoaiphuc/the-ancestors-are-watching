using UnityEngine;
using UnityEngine.InputSystem;

namespace AncestorsWatching
{
    /// <summary>Turns a mouse click into damage on the enemy under the cursor.</summary>
    public class ClickStriker : MonoBehaviour
    {
        [SerializeField] private Camera gameCamera;
        [SerializeField] private double clickDamage = 5;

        private void Update()
        {
            // The project uses the new Input System only, so the old Input class would throw.
            var mouse = Mouse.current;
            if (mouse == null || !mouse.leftButton.wasPressedThisFrame) return;

            // Orthographic camera: x and y are correct whatever depth we pass in.
            Vector2 worldPoint = gameCamera.ScreenToWorldPoint(mouse.position.ReadValue());

            var hit = Physics2D.OverlapPoint(worldPoint);
            if (hit != null && hit.TryGetComponent(out Enemy enemy))
                enemy.TakeDamage(clickDamage);
        }
    }
}
