using System.Collections.Generic;
using Assets.Chemicals;

public class Mortar : InstrumentBase
{
	private List<ChemicalElements> _elements = new List<ChemicalElements>();

	public override void AddChemicalElement(ChemicalElements element)
	{
		_elements.Add(element);
	}
}
