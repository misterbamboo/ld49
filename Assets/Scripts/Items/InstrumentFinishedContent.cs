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

        public ChemicalElements GetChemicalElement()
        {
            return elements.Select(e => e.ChemicalElement).FirstOrDefault();
        }

        public ChemicalStages GetChemicalStage()
        {
            return elements.Select(e => e.ChemicalStage).FirstOrDefault();
        }
    }
}
