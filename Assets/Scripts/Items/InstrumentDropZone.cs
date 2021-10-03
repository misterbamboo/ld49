using Assets.Chemicals;
using UnityEngine;

public class InstrumentDropZone : MonoBehaviour
{
    [SerializeField] private InstrumentBase _instrument;

    public void DropElement(PickableObject droppedObject)
    {
        if (droppedObject.TryGetComponent(out IChemicalItem chemicalItem))
        {
            if(_instrument.AddChemicalItem(chemicalItem))
            {
                Destroy(droppedObject.gameObject);
            }
        }
    }
}
