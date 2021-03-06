using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Chemicals;
using Assets.Scripts.Items;
using UnityEngine;

public class Mixer : InstrumentBase
{
    [SerializeField] private ChemicalMaterialsScriptableObject _chemicalMaterialsScriptableObject;
    [SerializeField] private Transform _structureSection;
    [SerializeField] private MeshRenderer _topContent;
    [SerializeField] private MeshRenderer _bottomContent;
    [SerializeField] private float _timeNeededToMix = 3f;
    [SerializeField] private float _overtimeMax = 6f;

    private List<IChemicalItem> _elements = new List<IChemicalItem>();
    private bool _isMixing = false;
    private float _mixingTime = 0f;
    private bool _isDone = false;
    private bool _isOvertime = false;

    private Quaternion _initialStructureSectionRotation;

    private void Awake()
    {
        InstrumentType = InstrumentType.Mixer;
    }

    private void Start()
    {
        _initialStructureSectionRotation = _structureSection.rotation;
        _topContent.gameObject.SetActive(false);
        _bottomContent.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_isMixing)
        {
            _mixingTime += Time.deltaTime;
            if (_mixingTime >= _timeNeededToMix)
            {
                if (!_isDone)
                {
                    Done();
                }
                else if (!_isOvertime && _mixingTime >= _overtimeMax)
                {
                    Overtime();
                }
            }

            UpdateMixerStructureRotation();
        }
    }

    private void UpdateMixerStructureRotation()
    {
        var rotationOnY = _mixingTime * 360;
        var angle = _initialStructureSectionRotation.eulerAngles;
        angle.y = angle.y + rotationOnY;

        _structureSection.rotation = Quaternion.Euler(angle);
    }

    public override bool AddChemicalItem(IChemicalItem chemical)
    {
        if (CanBeAdded(chemical))
        {
            UpdateContentColor(chemical);

            _elements.Add(chemical);
            CheckChimicalRaction();
            return true;
        }

        return false;
    }

    private void CheckChimicalRaction()
    {
        if (_elements.Count > 1)
        {
            var first = _elements[0];
            var second = _elements[1];
            var reaction = first.React(second);
            if (reaction.HasInstantEffect())
            {
                ResetMixerState();
                ExplosionManager.ExploseAt(transform.position);
            }
        }
    }

    private void UpdateContentColor(IChemicalItem chemical)
    {
        var contentToChange = _bottomContent;
        if (_elements.Count > 0)
        {
            contentToChange = _topContent;
        }

        contentToChange.gameObject.SetActive(true);
        contentToChange.material.color = _chemicalMaterialsScriptableObject.GetElementColor(chemical.ChemicalElement);
    }

    private bool CanBeAdded(IChemicalItem chemical)
    {
        return FreeSpot() && ChimicalAccepted(chemical);
    }

    private static bool ChimicalAccepted(IChemicalItem chemical)
    {
        return IsPowder(chemical) || IsWater(chemical);
    }

    private static bool IsPowder(IChemicalItem chemical)
    {
        return chemical.ChemicalStage == ChemicalStages.Powder;
    }

    private static bool IsWater(IChemicalItem chemical)
    {
        return chemical.ChemicalElement == ChemicalElements.Blue && chemical.ChemicalStage == ChemicalStages.Raw;
    }

    private bool FreeSpot()
    {
        return _elements.Count < 2;
    }

    public override bool Use()
    {
        if (_elements.Count < 2)
        {
            //TODO empty feedback
            return false;
        }
        else
        {
            Debug.Log("Mixing started!");
            _mixingTime = 0f;
            _isMixing = true;
            _isDone = false;
            _isOvertime = false;

            return true;
        }
    }

    public override void StopUsing()
    {
        StopMixer();
    }

    private void Done()
    {
        var firstColor = _chemicalMaterialsScriptableObject.GetElementColor(_elements[0].ChemicalElement);
        var secondColor = _chemicalMaterialsScriptableObject.GetElementColor(_elements[1].ChemicalElement);

        var mixedColor = (firstColor + secondColor) / 2;
        _topContent.material.color = mixedColor;
        _bottomContent.material.color = mixedColor;

        Debug.Log("Mixing done!");
        _isDone = true;
    }

    private void Overtime()
    {
        Debug.Log("Mixing overtime!");
        _isOvertime = true;
    }

    private void StopMixer()
    {
        Debug.Log("Mixing stopped!");
        _isMixing = false;
    }

    public override InstrumentFinishedContent RemoveFinishedContent()
    {
        var upgratedElements = UpgradeToNextStage();
        ResetMixerState();
        return new InstrumentFinishedContent(upgratedElements, _instrumentFinishedPrefab);
    }

    private List<ChimicalFinishedContent> UpgradeToNextStage()
    {
        var mortaredElements = new List<ChimicalFinishedContent>();
        foreach (var element in _elements)
        {
            var mortaredElement = new ChimicalFinishedContent();
            mortaredElement.Init(element.ChemicalElement, ChemicalStages.Mixed);
            mortaredElements.Add(mortaredElement);
        }

        return mortaredElements;
    }

    private void ResetMixerState()
    {
        _mixingTime = 0f;
        _isMixing = false;
        _isDone = false;
        _isOvertime = false;
        _elements = new List<IChemicalItem>();
        _topContent.gameObject.SetActive(false);
        _bottomContent.gameObject.SetActive(false);
        _structureSection.rotation = _initialStructureSectionRotation;
    }

    public override bool HasFinishedContent()
    {
        return _isDone;
    }
}
