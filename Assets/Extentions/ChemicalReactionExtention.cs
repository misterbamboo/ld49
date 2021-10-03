using Assets.Chemicals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public static class ChemicalReactionExtention
{
    public static IChemicalMixReaction React(this IChemicalItem firstChemical, IChemicalItem secondChemical)
    {
        if (firstChemical.HasAlreadyReact() || secondChemical.HasAlreadyReact())
        {
            return null;
        }

        var reaction = ChemicalMixes.Mix(firstChemical.ChemicalElement, secondChemical.ChemicalElement);
        if (reaction == null)
        {
            Debug.LogWarning($"No reaction defined for chemical : {firstChemical} & {secondChemical}");
            return null;
        }

        firstChemical.FlagAsAlreadyReact();
        secondChemical.FlagAsAlreadyReact();

        return reaction;
    }
}