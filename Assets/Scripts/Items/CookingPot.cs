using System.Collections.Generic;
using Assets.Chemicals;
using Assets.Scripts.Items;
using UnityEngine;

public class CookingPot : InstrumentBase
{
    private List<IChemicalItem> _elements = new List<IChemicalItem>();
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

    public override void AddChemicalItem(IChemicalItem chemical)
    {
        _elements.Add(chemical);
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

    public override InstrumentFinishedContent RemoveFinishedContent()
    {
        return null;
    }

    public override bool HasFinishedContent()
    {
        return false;
    }
}
