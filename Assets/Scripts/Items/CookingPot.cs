using System.Collections.Generic;
using Assets.Chemicals;
using UnityEngine;

public class CookingPot : InstrumentBase
{
	private List<ChemicalElements> _elements = new List<ChemicalElements>();
	private bool _isCooking = false;

	private void Awake()
	{
		InstrumentType = InstrumentType.CookingPot;
	}

	private void Update()
	{
		if (_isCooking)
		{
			//TODO cook
		}
	}

	public override void AddChemicalElement(ChemicalElements element)
	{
		_elements.Add(element);
	}

	public override bool Use()
	{
		Debug.Log("Started Cooking pot!");
		_isCooking = true;
		return true;
	}

	public override void StopUsing()
	{
		if (_isCooking)
		{
			Debug.Log("Stopped Cooking pot!");
			_isCooking = false;
		}
	}
}
