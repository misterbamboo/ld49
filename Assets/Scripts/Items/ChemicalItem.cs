using Assets.Chemicals;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IChemicalItem
{
    ChemicalElements ChemicalElement { get; }

    ChemicalStages ChemicalStage { get; }

    void Init(ChemicalElements chemicalElement, ChemicalStages chemicalStage);

    void FlagAsAlreadyReact();

    bool HasAlreadyReact();
}

public class ChemicalItem : MonoBehaviour, IChemicalItem
{
    [SerializeField] private ChemicalMaterialsScriptableObject _chemicalMaterialsScriptableObject;

    [SerializeField] private GameObject _chemicalMixturePrefab;

    [SerializeField] private ChemicalElements _chemicalElement;

    [SerializeField] private ChemicalStages _chemicalStage;

    private ChemicalElements _selectedElement;

    public ChemicalElements ChemicalElement => _selectedElement;

    public ChemicalStages ChemicalStage => _chemicalStage;

    private bool _alreadyReact;
    public void FlagAsAlreadyReact()
    {
        _alreadyReact = true;
    }
    public bool HasAlreadyReact()
    {
        return _alreadyReact;
    }

    private void Start()
    {
        Init(_chemicalElement, _chemicalStage);
    }

    public void Init(ChemicalElements chemicalElement, ChemicalStages chemicalStage)
    {
        _chemicalElement = chemicalElement;
        _chemicalStage = chemicalStage;
        DefineSelectedElement();
        TryUpdateColor();
    }

    private void DefineSelectedElement()
    {
        if (_chemicalElement == ChemicalElements.Random)
        {
            _selectedElement = GetRandomElement();
        }
        else
        {
            _selectedElement = _chemicalElement;
        }
    }

    private void TryUpdateColor()
    {
        if (ObjectIsntDisposed())
        {
            var renderer = GetComponent<MeshRenderer>();
            renderer.material.color = _chemicalMaterialsScriptableObject.GetElementColor(_selectedElement);
        }
    }

    // The ChemicalItem reference is keep overtime (event if the GameObject have been deleted)
    private bool ObjectIsntDisposed()
    {
        return this != null;
    }

    private static ChemicalElements GetRandomElement()
    {
        var count = Enum.GetNames(typeof(ChemicalElements)).Length;
        var colorIndex = UnityEngine.Random.Range(1, count - 1);
        var element = (ChemicalElements)colorIndex;
        return element;
    }
}
