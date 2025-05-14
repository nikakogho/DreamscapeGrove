using System.Collections.Generic;
using UnityEngine;
using DreamscapeGrove.Core;

namespace DreamscapeGrove.Gameplay
{
    public class TreeSpawnController : MonoBehaviour
    {
        [SerializeField] private List<TreeSpecies> treeSpecies;
        [SerializeField] private Transform spawnParent;
        [SerializeField] private Vector3 startPos;
        [SerializeField] private float spacing = 2f;

        private readonly List<StagedTree> forest = new();
        private StagedTree current;

        private void Start() => SpawnNewTree();

        private void Update()
        {
            var f = FocusManager.Instance.CurrentFrame;

            if (f.confidence < FocusManager.ConfidenceThreshold) return;

            bool grow = f.focus >= FocusManager.FocusThreshold;

            float dt = Time.deltaTime;

            if (grow) HandleGrowing(dt);
            else HandleShrinking(dt);
        }

        void HandleGrowing(float dt)
        {
            if (current == null || !current.Grow(dt))
                SpawnNewTree();
        }

        void HandleShrinking(float dt)
        {
            if (current == null) return;

            if (!current.Shrink(dt))
            {
                Destroy(forest[^1].gameObject);
                forest.RemoveAt(forest.Count - 1);
                current = forest.Count > 0 ? forest[^1] : null;
            }
        }

        Vector3 GetSpawnPosition(int index)
        {
            int currentSquare = Mathf.FloorToInt(Mathf.Sqrt(index + 1));
            if (currentSquare * currentSquare < index + 1) currentSquare++;

            int lastSquare = currentSquare - 1;

            int offset = index - lastSquare * lastSquare;

            /*
            0 1 8
            2 3 7
            4 5 6
            */

            int x = offset < currentSquare ? offset : currentSquare;
            int z = offset < currentSquare ? currentSquare : (lastSquare - (offset - currentSquare));

            return new Vector3(x, 0, z) * spacing;
        }

        void SpawnNewTree()
        {
            if (treeSpecies.Count == 0) return;

            int pick = Random.Range(0, treeSpecies.Count);
            var species = treeSpecies[pick];

            Vector3 pos = startPos + GetSpawnPosition(forest.Count);
            GameObject treeObject = new GameObject(species.name);
            treeObject.transform.SetParent(spawnParent);
            treeObject.transform.position = pos;

            var st = treeObject.AddComponent<StagedTree>();
            st.Init(species);

            current = st;
            forest.Add(current);
        }
    }
}
