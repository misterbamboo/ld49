using Assets.Chemicals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    [SerializeField] private Transform _pickableObjectParent;
    [SerializeField] private ChemicalMaterialsScriptableObject _chemicalMaterialsScriptableObject;
    private List<PickableObject> _pickableObjects = new List<PickableObject>();
    private List<InstrumentBase> _closeInstruments = new List<InstrumentBase>();
    private PickableObject _pickedUpObject = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PickableObject" && other.TryGetComponent<PickableObject>(out PickableObject pickableObject))
        {
            _pickableObjects.Add(pickableObject);
        }

        if (other.tag == "Instrument" && other.TryGetComponent<InstrumentBase>(out InstrumentBase instrument))
        {
            _closeInstruments.Add(instrument);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PickableObject" && other.TryGetComponent<PickableObject>(out PickableObject pickableObject))
        {
            _pickableObjects.Remove(pickableObject);
        }

        if (other.tag == "Instrument" && other.TryGetComponent<InstrumentBase>(out InstrumentBase instrument))
        {
            _closeInstruments.Remove(instrument);
        }
    }

    public void TogglePickup()
    {
        if (_pickedUpObject != null)
        {
            _pickedUpObject.Drop();
            _pickableObjects.Remove(_pickedUpObject);
            _pickedUpObject = null;
            return;
        }

        PickableObject closestPickableObject = FindClosestPickableObject();
        if (closestPickableObject != null)
        {
            closestPickableObject.Pickup(_pickableObjectParent);
            _pickedUpObject = closestPickableObject;
        }
        else
        {
            PickupFromInstrument();
        }
    }

    private void PickupFromInstrument()
    {
        var closestInstrument = _closeInstruments.FindClosest(transform.position);
        if (closestInstrument != null && closestInstrument.HasFinishedContent())
        {
            PickupInstrumentContent(closestInstrument);
        }
    }

    private void PickupInstrumentContent(InstrumentBase closestInstrument)
    {
        var content = closestInstrument.RemoveFinishedContent();

        var pickableGO = Instantiate(content.GetPrefab());

        var renderer = pickableGO.GetComponent<MeshRenderer>();
        renderer.material.color = _chemicalMaterialsScriptableObject.GetElementColor(content.GetChemicalElement());

        var pickable = pickableGO.GetComponent<PickableObject>();
        pickable.Pickup(_pickableObjectParent);
        _pickedUpObject = pickable;
    }

    private PickableObject FindClosestPickableObject()
    {
        PickableObject closestPickableObject = null;
        float minDist = Mathf.Infinity;

        List<PickableObject> objectsToRemoved = new List<PickableObject>();

        foreach (PickableObject pickableObject in _pickableObjects)
        {
            if (pickableObject == null)
            {
                objectsToRemoved.Add(pickableObject);
                continue;
            }
            float dist = Vector3.Distance(pickableObject.transform.position, transform.position);
            if (dist < minDist)
            {
                closestPickableObject = pickableObject;
                minDist = dist;
            }
        }

        foreach (PickableObject pickableObject in objectsToRemoved)
        {
            _pickableObjects.Remove(pickableObject);
        }

        return closestPickableObject;
    }
}
