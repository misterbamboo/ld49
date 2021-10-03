using Assets.Chemicals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Items
{
    public class InstrumentFinishedContent
    {
        private IEnumerable<IChemicalItem> elements;
        private GameObject prefab;

        public InstrumentFinishedContent(IEnumerable<IChemicalItem> elements, GameObject prefab)
        {
            this.elements = elements;
            this.prefab = prefab;
        }

        public GameObject GetPrefab()
        {
            return prefab;
        }

        public ChemicalElements GetFirstChemicalElement()
        {
            return elements.Select(e => e.ChemicalElement).FirstOrDefault();
        }

        public ChemicalStages GetFirstChemicalStage()
        {
            return elements.Select(e => e.ChemicalStage).FirstOrDefault();
        }

        public IChemicalItem GetFirstChemicalItem()
        {
            return elements.First();
        }

        public IChemicalItem GetSecondChemicalItem()
        {
            return elements.Skip(1).First();
        }

        public bool IsSingleElement()
        {
            return elements.Count() == 1;
        }

        public bool IsMultipleElements()
        {
            return elements.Count() > 1;
        }
    }
}
