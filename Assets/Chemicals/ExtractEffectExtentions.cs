using Assets.Chemicals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class ExtractEffectExtentions
{
    public static IChemicalMixReaction GetReaction(this PickableObject pickedUpObject)
    {
        var reaction = pickedUpObject.GetChemicalItemReaction();
        if (reaction == null)
        {
            reaction = pickedUpObject.GetChemicalMixtureReaction();
        }

        return reaction;
    }

    public static IChemicalMixReaction GetChemicalItemReaction(this PickableObject pickedUpObject)
    {
        var chemicalItem = pickedUpObject.GetComponent<IChemicalItem>();
        if (chemicalItem != null)
        {
            return ChemicalMixes.Mix(chemicalItem.ChemicalElement, ChemicalElements.None);
        }
        return null;
    }

    public static IChemicalMixReaction GetChemicalMixtureReaction(this PickableObject pickedUpObject)
    {
        var chemicalMixture = pickedUpObject.GetComponent<IChemicalMixture>();
        if (chemicalMixture != null)
        {
            return chemicalMixture.Reaction;
        }
        return null;
    }
}
