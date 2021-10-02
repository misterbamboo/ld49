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
    [SerializeField] private List<ChemicalElementMaterial> _chemicalElementMaterials;

    [SerializeField] private MeshRenderer _meshRenderer;

    public IChemicalMixReaction Reaction { get; private set; }

    public void InitReaction(IChemicalMixReaction reaction)
    {
        Reaction = reaction;

        var first = Reaction.GetFirstChemicalElement();
        var second = Reaction.GetSecondChemicalElement();

        var firstColor = GetElementColor(first);
        var secondColor = GetElementColor(second);

        var resultColor = (firstColor + secondColor) / 2;
        
        var material = _meshRenderer.materials.First();
        material.color = resultColor;
    }

    private Color GetElementColor(ChemicalElements element)
    {
        return _chemicalElementMaterials.Where(e => e.chemicalElement == element).Select(e => e.material.color).FirstOrDefault();
    }
}

[Serializable]
public struct ChemicalElementMaterial
{
    [SerializeField] public ChemicalElements chemicalElement;

    [SerializeField] public Material material;
}