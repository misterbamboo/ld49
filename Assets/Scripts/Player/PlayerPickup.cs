using Assets.Chemicals;
using Assets.Scripts.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    [SerializeField] private Transform _pickableObjectParent;
    [SerializeField] private ChemicalMaterialsScriptableObject _chemicalMaterialsScriptableObject;
    private List<PickableObject> _pickableObjects = new List<PickableObject>();
    private List<InstrumentBase> _closeInstruments = new List<InstrumentBase>();
    private PickableObject _pickedUpObject = null;

    public PickableObject PickedUpObject => _pickedUpObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == PickableObject.Tag && other.TryGetComponent(out PickableObject pickableObject))
        {
            _pickableObjects.Add(pickableObject);
        }

        if (other.tag == InstrumentBase.Tag && other.TryGetComponent(out InstrumentBase instrument))
        {
            _closeInstruments.Add(instrument);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == PickableObject.Tag && other.TryGetComponent(out PickableObject pickableObject))
        {
            _pickableObjects.Remove(pickableObject);
        }

        if (other.tag == InstrumentBase.Tag && other.TryGetComponent(out InstrumentBase instrument))
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

        PickableObject closestPickableObject = FindClosestPickableObject(p => p.IsAvailableForPickup);
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

        HandleSingleElement(content, pickableGO);
        HandleMultipleElements(content, pickableGO);

        var pickable = pickableGO.GetComponent<PickableObject>();
        pickable.Pickup(_pickableObjectParent);
        _pickedUpObject = pickable;
    }

    private static void HandleSingleElement(InstrumentFinishedContent content, GameObject gameobject)
    {
        if (content.IsSingleElement())
        {
            var newStageChemicalItem = gameobject.GetComponent<IChemicalItem>();
            if (newStageChemicalItem != null)
            {
                newStageChemicalItem.Init(content.GetFirstChemicalElement(), content.GetFirstChemicalStage());
            }
        }
    }

    private static void HandleMultipleElements(InstrumentFinishedContent content, GameObject gameobject)
    {
        if (content.IsMultipleElements())
        {
            var chemicalMixture = gameobject.GetComponent<IChemicalMixture>();
            if (chemicalMixture != null)
            {
                chemicalMixture.React(content.GetFirstChemicalItem(), content.GetSecondChemicalItem());
            }
        }
    }

    private PickableObject FindClosestPickableObject(Func<PickableObject, bool> func = null)
    {
        if (func == null)
        {
            func = p => true;
        }

        PickableObject closestPickableObject = null;
        float minDist = Mathf.Infinity;

        List<PickableObject> objectsToRemoved = new List<PickableObject>();

        foreach (PickableObject pickableObject in _pickableObjects.Where(func))
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
