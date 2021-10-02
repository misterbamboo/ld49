using System;
using Assets.Chemicals;
using UnityEngine;

public enum InstrumentType
{
	CookingPot,
	Mortar,
	Mixer
}

public abstract class InstrumentBase : MonoBehaviour
{
	public Action OnTaskDone;
	public InstrumentType InstrumentType;
	public abstract void AddChemicalElement(ChemicalElements element);
	public abstract bool Use();
	public abstract void StopUsing();
}
