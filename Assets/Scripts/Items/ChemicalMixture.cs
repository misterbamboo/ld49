using Assets.Chemicals;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IChemicalMixture
{
    IChemicalMixReaction Reaction { get; }

    bool React(IChemicalItem firstChemical, IChemicalItem secondChemical);
}

public class ChemicalMixture : MonoBehaviour, IChemicalMixture
{
    [SerializeField] private ChemicalMaterialsScriptableObject _chemicalMaterials;

    [SerializeField] private ChemicalBeakerGlass _chemicalBeakerGlass;

    public IChemicalMixReaction Reaction { get; private set; }

    public bool React(IChemicalItem firstChemical, IChemicalItem secondChemical)
    {
        if (firstChemical.HasAlreadyReact() || secondChemical.HasAlreadyReact())
        {
            return false;
        }

        var reaction = ChemicalMixes.Mix(firstChemical.ChemicalElement, secondChemical.ChemicalElement);
        if (reaction == null)
        {
            Debug.LogWarning($"No reaction defined for chemical : {firstChemical} & {secondChemical}");
            return false;
        }

        firstChemical.FlagAsAlreadyReact();
        secondChemical.FlagAsAlreadyReact();

        if (reaction.HasInstantEffect())
        {

        }
        else
        {
            PutMixtureInBeakerGlass(reaction);
        }

        return true;
    }

    private void PutMixtureInBeakerGlass(IChemicalMixReaction reaction)
    {
        Reaction = reaction;

        var glassContent = _chemicalBeakerGlass.glassContent;
        var meshRenderer = glassContent.GetComponent<MeshRenderer>();
        if (meshRenderer == null)
        {
            throw new Exception("ChemicalMixture should have a chemicalBeakerGlass to get the glass content to update");
        }

        var first = Reaction.GetFirstChemicalElement();
        var second = Reaction.GetSecondChemicalElement();

        var firstColor = _chemicalMaterials.GetElementColor(first);
        var secondColor = _chemicalMaterials.GetElementColor(second);

        var resultColor = (firstColor + secondColor) / 2;

        meshRenderer.material.color = resultColor;
    }
}