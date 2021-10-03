using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Chemicals
{
    public interface IChemicalMaterials
    {
        Color GetElementColor(ChemicalElements element);
    }

    [CreateAssetMenu(fileName = "ChemicalMaterials", menuName = "ScriptableObjects/ChemicalMaterialsScriptableObject", order = 2)]
    public class ChemicalMaterialsScriptableObject : ScriptableObject
    {
        [SerializeField] private List<ChemicalElementMaterial> _chemicalMaterials;

        public Color GetElementColor(ChemicalElements element)
        {
            return _chemicalMaterials.Where(e => e.chemicalElement == element).Select(e => e.material.color).FirstOrDefault();
        }
    }

    [Serializable]
    public struct ChemicalElementMaterial
    {
        [SerializeField] public ChemicalElements chemicalElement;

        [SerializeField] public Material material;
    }
}
