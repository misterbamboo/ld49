using Assets.Chemicals;
using UnityEngine;

public class InstrumentDropZone : MonoBehaviour
{
	[SerializeField] private InstrumentBase _instrument;

	public void DropElement(PickableObject droppedObject)
	{
		_instrument.AddChemicalElement(ChemicalElements.None);
		Destroy(droppedObject.gameObject);
	}
}
