using System.Collections;
using System.Collections.Generic;
using Assets.Chemicals;
using UnityEngine;

public abstract class InstrumentBase : MonoBehaviour
{
	public abstract void AddChemicalElement(ChemicalElements element);
}
