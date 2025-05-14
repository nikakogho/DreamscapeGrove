using UnityEngine;

namespace DreamscapeGrove.Gameplay
{
    /// <summary>Handles one tree that progresses through species stages.</summary>
    public class StagedTree : MonoBehaviour
    {
        private TreeSpecies species;
        private int stageIndex;
        private GameObject visual;

        public void Init(TreeSpecies species)
        {
            this.species = species;
            LoadStage(0);
        }

        /* ---------- Public API ---------- */

        /// <returns>true if still growing possible</returns>
        public bool Grow(float amount)
        {
            if (!visual) return false;

            float target = species.stages[stageIndex].targetScale;
            visual.transform.localScale = Vector3.MoveTowards(
                visual.transform.localScale,
                Vector3.one * target,
                amount * species.growRate);

            if (Mathf.Approximately(visual.transform.localScale.x, target))
            {
                stageIndex++;
                if (stageIndex < species.stages.Count)
                {
                    LoadStage(stageIndex);
                    return true;
                }
                return false; // fully grown
            }
            return true;
        }

        /// <returns>true if tree is still alive</returns>
        public bool Shrink(float amount)
        {
            if (!visual) return false;

            visual.transform.localScale = Vector3.MoveTowards(
                visual.transform.localScale,
                Vector3.zero,
                amount * species.shrinkRate);

            // optional: tint red while shrinking
            if (visual.TryGetComponent(out Renderer r))
            {
                float t = visual.transform.localScale.x /
                          species.stages[stageIndex].targetScale;
                r.material.color = Color.Lerp(Color.red, Color.white, t);
            }

            if (visual.transform.localScale.x <= 0.01f)
            {
                stageIndex--;
                if (stageIndex >= 0)
                {
                    var previousStage = species.stages[stageIndex];
                    var previousScale = Mathf.Lerp(previousStage.startScale, previousStage.targetScale, 0.8f);

                    LoadStage(stageIndex, previousScale);
                    return true;
                }
                return false; // dead
            }
            return true;
        }

        /* ---------- Helpers ---------- */
        private void LoadStage(int index, float? scale = null)
        {
            if (visual) Destroy(visual);

            var stage = species.stages[index];
            var prefab = species.stages[index].prefab;

            visual = Instantiate(prefab, transform);
            visual.transform.localScale = Vector3.one * (scale ?? stage.startScale); // start tiny
        }
    }
}
