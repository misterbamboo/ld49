using System.Collections.Generic;
using Assets.Chemicals;

public class CookingPot : InstrumentBase
{
	private List<ChemicalElements> _elements = new List<ChemicalElements>();

	public override void AddChemicalElement(ChemicalElements element)
	{
		_elements.Add(element);
	}

	public override void StopUsing()
	{
		throw new System.NotImplementedException();
	}

	public override void Use()
	{
		throw new System.NotImplementedException();
	}
}
