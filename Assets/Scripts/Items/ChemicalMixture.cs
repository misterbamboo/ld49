using Assets.Chemicals;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IChemicalMixture
{
    IChemicalMixReaction Reaction { get; }

    void InitReaction(IChemicalMixReaction reaction);
}

public class ChemicalMixture : MonoBehaviour, IChemicalMixture
{
    [SerializeField] private ChemicalMaterialsScriptableObject _chemicalMaterials;

    [SerializeField] private MeshRenderer _meshRenderer;

    public IChemicalMixReaction Reaction { get; private set; }

    public void InitReaction(IChemicalMixReaction reaction)
    {
        Reaction = reaction;

        var first = Reaction.GetFirstChemicalElement();
        var second = Reaction.GetSecondChemicalElement();

        var firstColor = _chemicalMaterials.GetElementColor(first);
        var secondColor = _chemicalMaterials.GetElementColor(second);

        var resultColor = (firstColor + secondColor) / 2;
        
        var material = _meshRenderer.materials.First();
        material.color = resultColor;
    }
}