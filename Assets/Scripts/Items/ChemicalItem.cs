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

    [SerializeField] private ChemicalElements _chemicalElement;

    [SerializeField] private ChemicalStages _chemicalStage;

    private ChemicalElements _selectedElement;

    public ChemicalElements ChemicalElement => _selectedElement;

    public ChemicalStages ChemicalStage => _chemicalStage;

    private bool _alreadyReact;

    private MeshRenderer _meshRenderer;
    private Color[] _colorsWeel;
    private float _colorWeelTotalTime = 4;
    private float _currentColorTime = 0;

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

        _meshRenderer = GetComponent<MeshRenderer>();
        _colorsWeel = new Color[]
        {
            _chemicalMaterialsScriptableObject.GetElementColor(ChemicalElements.Green),
            _chemicalMaterialsScriptableObject.GetElementColor(ChemicalElements.Purple),
            _chemicalMaterialsScriptableObject.GetElementColor(ChemicalElements.Red),
            _chemicalMaterialsScriptableObject.GetElementColor(ChemicalElements.Yellow),
        };
    }

    private void Update()
    {
        if (_chemicalElement == ChemicalElements.Random)
        {
            _currentColorTime += Time.deltaTime;
            if (_currentColorTime > _colorWeelTotalTime)
            {
                _currentColorTime -= _colorWeelTotalTime;
            }

            var ratio = _currentColorTime / _colorWeelTotalTime;
            var freeIndex = ((float)_colorsWeel.Length) * ratio;
            var index = (int)freeIndex;

            var primaryIndex = index >= _colorsWeel.Length ? 0 : index;
            _meshRenderer.material.color = _colorsWeel[primaryIndex];
        }
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
        if (ObjectIsntDisposed() && _chemicalElement != ChemicalElements.Random)
        {
            try
            {
                var renderer = GetComponent<MeshRenderer>();
                renderer.material.color = _chemicalMaterialsScriptableObject.GetElementColor(_selectedElement);
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Error to fix: {e.Message}");
            }
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
        // 2 to lengh -1 (2 because want to skip blue)
        var colorIndex = UnityEngine.Random.Range(2, count - 1);
        var element = (ChemicalElements)colorIndex;
        return element;
    }

    private void OnTriggerEnter(Collider other)
    {
        var otherChimical = other.gameObject.GetComponent<IChemicalItem>();
        MakeChemicalReact(other.gameObject, otherChimical);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var otherChimical = collision.gameObject.GetComponent<IChemicalItem>();
        MakeChemicalReact(collision.gameObject, otherChimical);
    }

    private void MakeChemicalReact(GameObject otherGameObject, IChemicalItem otherChimical)
    {
        if (otherChimical == null)
        {
            return;
        }

        if (HasAlreadyReact() || otherChimical.HasAlreadyReact())
        {
            return;
        }

        var reaction = this.React(otherChimical);
        if (reaction != null && reaction.HasInstantEffect())
        {
            ExplosionManager.ExploseAt(transform.position);
            Destroy(gameObject);
            Destroy(otherGameObject);
        }
    }
}
