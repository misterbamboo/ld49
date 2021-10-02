using System.Collections.Generic;
using Assets.Chemicals;
using UnityEngine;

public class Mortar : InstrumentBase
{
	[SerializeField] private float _timeNeededToMort = 2f;
	private List<ChemicalElements> _elements = new List<ChemicalElements>();
	private bool _isMorting = false;
	private float _currentMortingTime = 0f;

	private void Update()
	{
		if (_isMorting)
		{
			_currentMortingTime += Time.deltaTime;
			if (_currentMortingTime >= _timeNeededToMort)
			{
				MortingDone();
			}
		}
	}

	public override void AddChemicalElement(ChemicalElements element)
	{
		_elements.Add(element);
	}

	public override void Use()
	{
		if (_elements.Count == 0)
		{
			// TODO empty feedback
		}
		else
		{
			StartMorting();
		}
	}

	public override void StopUsing()
	{
		StopMorting();
	}

	private void StartMorting()
	{
		Debug.Log("Start using Mortar");
		_isMorting = true;
		_currentMortingTime = 0f;
	}

	private void MortingDone()
	{
		StopUsing();
		// TODO Mortar done feedback
		Debug.Log("Done using Mortar");
	}

	private void StopMorting()
	{
		_isMorting = false;
	}
}
