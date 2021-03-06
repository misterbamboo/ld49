using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Chemicals
{
    public class ChemicalMixes : MonoBehaviour
    {
        public static ChemicalMixes Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        [SerializeField] private ChemicalDefinitionScriptableObject[] chemicalDefinition;

        public static IChemicalMixReaction Mix(ChemicalElements first, ChemicalElements second)
        {
            return Instance
                .chemicalDefinition
                .Where(d => d.Element == first && d.SecondElement == second || d.Element == second && d.SecondElement == first)
                .FirstOrDefault();
        }
    }
}
