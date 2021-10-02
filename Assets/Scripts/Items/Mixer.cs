using System.Collections.Generic;
using Assets.Chemicals;
using UnityEngine;

public class Mixer : InstrumentBase
{
	private List<ChemicalElements> _elements = new List<ChemicalElements>();
	private bool _isMixing = false;
	private float _timeNeededToMix = 3f;
	private float _overtimeMax = 6f;
	private float _mixingTime = 0f;
	private bool _isDone = false;
	private bool _isOvertime = false;

	private void Awake()
	{
		InstrumentType = InstrumentType.Mixer;
	}

	private void Update()
	{
		if (_isMixing)
		{
			_mixingTime += Time.deltaTime;
			if (_mixingTime >= _timeNeededToMix)
			{
				if (!_isDone)
				{
					Done();
				}
				else if (!_isOvertime && _mixingTime >= _overtimeMax)
				{
					Overtime();
				}
			}
		}
	}

	public override void AddChemicalElement(ChemicalElements element)
	{
		_elements.Add(element);
	}

	public override bool Use()
	{
		if (_elements.Count == 0)
		{
			//TODO empty feedback
			return false;
		}
		else
		{
			Debug.Log("Mixing started!");
			_mixingTime = 0f;
			_isMixing = true;
			_isDone = false;
			_isOvertime = false;

			return true;
		}
	}

	public override void StopUsing()
	{
		StopMixer();
	}

	private void Done()
	{
		Debug.Log("Mixing done!");
		_isDone = true;
	}

	private void Overtime()
	{
		Debug.Log("Mixing overtime!");
		_isOvertime = true;
	}

	private void StopMixer()
	{
		Debug.Log("Mixing stopped!");
		_isMixing = false;
	}
}
