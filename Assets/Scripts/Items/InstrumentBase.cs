using System;
using Assets.Chemicals;
using Assets.Scripts.Items;
using UnityEngine;

public enum InstrumentType
{
    CookingPot,
    Mortar,
    Mixer
}

public abstract class InstrumentBase : MonoBehaviour
{
    [SerializeField] protected GameObject _instrumentFinishedPrefab;

    public Action OnTaskDone;
    public InstrumentType InstrumentType;
    public abstract void AddChemicalItem(IChemicalItem chemical);
    public abstract bool Use();
    public abstract void StopUsing();
    public abstract bool HasFinishedContent();
    public abstract InstrumentFinishedContent RemoveFinishedContent();
}
