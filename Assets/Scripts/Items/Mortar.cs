using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Chemicals;
using Assets.Scripts.Items;
using UnityEngine;

public class Mortar : InstrumentBase
{
    private const double PediodPerSec = 2 * Math.PI;

    [SerializeField] private ChemicalMaterialsScriptableObject _chemicalMaterials;

    [SerializeField] private MeshRenderer filledIndicator;

    [SerializeField] private MeshRenderer finishedIndicator;

    [SerializeField] private float _timeNeededToMort = 2f;
    private List<IChemicalItem> _elements = new List<IChemicalItem>();
    private bool _isMorting = false;
    private bool _isDone = false;
    private float _currentMortingTime = 0f;

    private Quaternion _initialFilledIndicatorRotation;

    private void Awake()
    {
        InstrumentType = InstrumentType.Mortar;
    }

    private void Start()
    {
        _initialFilledIndicatorRotation = filledIndicator.transform.rotation;
    }

    private void Update()
    {
        if (_isMorting)
        {
            _currentMortingTime += Time.deltaTime;
            if (_currentMortingTime >= _timeNeededToMort)
            {
                MortingDone();
            }
        }

        UpdateFilledAndFinishedIndicator();
        UpdateContentRotation();

    }

    private void UpdateContentRotation()
    {
        if (_isMorting)
        {
            var angleOffset = Math.Sin(PediodPerSec * _currentMortingTime) * 10f;
            var angle = _initialFilledIndicatorRotation.eulerAngles;
            var z = (float)(angle.z + angleOffset);
            filledIndicator.transform.rotation = Quaternion.Euler(new Vector3(angle.x, angle.y, z));
        }
    }

    public override bool AddChemicalItem(IChemicalItem chemical)
    {
        if (chemical.ChemicalStage == ChemicalStages.Raw && chemical.ChemicalElement != ChemicalElements.Blue)
        {
            filledIndicator.material.color = _chemicalMaterials.GetElementColor(chemical.ChemicalElement);
            finishedIndicator.material.color = filledIndicator.material.color;
            _elements.Add(chemical);
            return true;
        }

        return false;
    }

    public override bool Use()
    {
        if (_elements.Count == 0)
        {
            // TODO empty feedback
            return false;
        }
        else
        {
            _isDone = false;
            StartMorting();
            return true;
        }
    }

    public override void StopUsing()
    {
        InterruptMorting();
    }

    private void StartMorting()
    {
        Debug.Log("Start using Mortar");
        _isMorting = true;
        _currentMortingTime = 0f;
    }

    private void MortingDone()
    {
        _isMorting = false;
        _isDone = true;
        Debug.Log("Done using Mortar");
        OnTaskDone?.Invoke();
    }

    private void InterruptMorting()
    {
        _isMorting = false;
        Debug.Log("Mortar interrupted");
    }

    private void UpdateFilledAndFinishedIndicator()
    {
        var haveElements = _elements.Any();
        if (filledIndicator.gameObject.activeInHierarchy != haveElements)
        {
            filledIndicator.gameObject.SetActive(haveElements);
        }

        if (_isDone)
        {
            if (!finishedIndicator.gameObject.activeInHierarchy)
                finishedIndicator.gameObject.SetActive(true);
        }
        else
        {
            if (finishedIndicator.gameObject.activeInHierarchy)
                finishedIndicator.gameObject.SetActive(false);
        }
    }

    public override InstrumentFinishedContent RemoveFinishedContent()
    {
        if (!HasFinishedContent())
        {
            return null;
        }

        var finishElements = CreateUpgradedElements();
        ResetMortarState();
        return new InstrumentFinishedContent(finishElements, _instrumentFinishedPrefab);
    }

    private IEnumerable<ChimicalFinishedContent> CreateUpgradedElements()
    {
        var chimicalFinished = new List<ChimicalFinishedContent>();
        foreach (var finishElement in _elements)
        {
            var chemical = new ChimicalFinishedContent();
            chemical.Init(finishElement.ChemicalElement, ChemicalStages.Powder);
            chimicalFinished.Add(chemical);
        }

        return chimicalFinished;
    }

    private void ResetMortarState()
    {
        _elements = new List<IChemicalItem>();
        _isDone = false;
    }

    public override bool HasFinishedContent()
    {
        return _elements.Count > 0 && _isDone;
    }
}
