using System.Collections.Generic;
using UnityEngine;

namespace DreamscapeGrove.Gameplay
{
    [CreateAssetMenu(fileName = "DreamscapeGrove/Tree Species")]
    public class TreeSpecies : ScriptableObject
    {
        new public string name;

        public List<TreeStage> stages;
        public float growRate = 0.3f;
        public float shrinkRate = 0.2f;
    }
}
