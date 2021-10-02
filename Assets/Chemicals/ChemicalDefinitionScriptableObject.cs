using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Chemicals
{
    [CreateAssetMenu(fileName = "ChemicalDefinitions", menuName = "ScriptableObjects/ChemicalDefinitionScriptableObject", order = 1)]
    public class ChemicalDefinitionScriptableObject : ScriptableObject
    {
        public ChemicalElements Element;

        public ChemicalElements SecondElement;

        public PlayerEffects PlayerEffect;

        public FlowerEffects FlowerEffect;

        public InstantEffects InstantEffect;
    }
}
