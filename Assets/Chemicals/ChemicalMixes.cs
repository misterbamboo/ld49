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

        [SerializeField] private ChemicalDefinition[] chemicalDefinition;

        public static ChemicalDefinition Mix(ChemicalElements first, ChemicalElements second)
        {
            return ChemicalMixes
                .Instance
                .chemicalDefinition
                .Where(d => d.Element == first && d.SecondElement == second)
                .FirstOrDefault();
        }
    }
}
