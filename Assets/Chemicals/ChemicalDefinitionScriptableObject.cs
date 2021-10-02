using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Chemicals
{
    public interface IChemicalMixReaction
    {
        bool HasInstantEffect();
        PlayerEffects GetPlayerEffect();
        FlowerEffects GetFlowerEffect();
        ChemicalElements GetFirstChemicalElement();
        ChemicalElements GetSecondChemicalElement();
    }

    [CreateAssetMenu(fileName = "ChemicalDefinitions", menuName = "ScriptableObjects/ChemicalDefinitionScriptableObject", order = 1)]
    public class ChemicalDefinitionScriptableObject : ScriptableObject, IChemicalMixReaction
    {
        public ChemicalElements Element;

        public ChemicalElements SecondElement;

        public InstantEffects InstantEffect;

        public PlayerEffects PlayerEffect;

        public FlowerEffects FlowerEffect;

        public bool HasInstantEffect()
        {
            return InstantEffect != InstantEffects.None;
        }

        public FlowerEffects GetFlowerEffect()
        {
            return FlowerEffect;
        }

        public PlayerEffects GetPlayerEffect()
        {
            return PlayerEffect;
        }

        public ChemicalElements GetFirstChemicalElement()
        {
            return Element;
        }

        public ChemicalElements GetSecondChemicalElement()
        {
            return SecondElement;
        }

        public override string ToString()
        {
            return $"instant: {InstantEffect}; player:{PlayerEffect}; flower:{FlowerEffect}";
        }
    }
}
