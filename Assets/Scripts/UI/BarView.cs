using UnityEngine;

namespace AncestorsWatching
{
    /// <summary>
    /// A horizontal bar made from a scaled sprite. Works with any sprite, so swapping art
    /// means changing the sprite on the Fill and Background objects, not this script.
    /// </summary>
    public class BarView : MonoBehaviour
    {
        [SerializeField] private Transform fill;
        [SerializeField] private float width = 2f;

        public void SetRatio(float ratio)
        {
            ratio = Mathf.Clamp01(ratio);

            // Scaling from the centre would shrink the bar on both sides,
            // so shift it to keep the left edge fixed. This also works with any sprite pivot.
            var scale = fill.localScale;
            scale.x = width * ratio;
            fill.localScale = scale;

            var position = fill.localPosition;
            position.x = -width * 0.5f + scale.x * 0.5f;
            fill.localPosition = position;
        }
    }
}
