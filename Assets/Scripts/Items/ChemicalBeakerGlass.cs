using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemicalBeakerGlass : MonoBehaviour
{
    public const string Tag = "ChemicalBeakerGlass";

    [SerializeField] private GameObject _glassContent;

    [SerializeField] private float _max = 3.95f;

    private float _total;

    private Vector3 _initialGlassContentScale;

    private float _initialGlassOffsetYOffset;

    private void Start()
    {
        _initialGlassContentScale = _glassContent.transform.localScale;
        _initialGlassOffsetYOffset = _glassContent.transform.localPosition.y - _initialGlassContentScale.y;
    }

    private void Update()
    {
        CheckGlassContentActiveState();
        UpdateGlassContentScale();
    }

    private void CheckGlassContentActiveState()
    {
        if (ShouldGlassContentChangeActiveState())
        {
            _glassContent.SetActive(_total != 0);
        }
    }

    private bool ShouldGlassContentChangeActiveState()
    {
        bool isEmpty = _total == 0;
        return isEmpty == _glassContent.activeInHierarchy;
    }

    private void UpdateGlassContentScale()
    {
        if (_glassContent.activeInHierarchy)
        {
            var newYScale = _initialGlassContentScale.y * _total / _max;

            var localPos = _glassContent.transform.localPosition;
            localPos.y = newYScale + _initialGlassOffsetYOffset;
            _glassContent.transform.localPosition = localPos;

            var scale = _initialGlassContentScale;
            scale.y = newYScale;
            _glassContent.transform.localScale = scale;
        }
    }

    public void Fill(float quantity)
    {
        _total += quantity;
        if (_total > _max)
        {
            _total = _max;
            AddChemicalElement();
        }
    }

    private void AddChemicalElement()
    {
        var chemicalItem = GetComponent<IChemicalItem>();
        if (chemicalItem != null)
        {
            return;
        }

        chemicalItem = gameObject.AddComponent<ChemicalItem>();
        chemicalItem.Init(Assets.Chemicals.ChemicalElements.Blue, Assets.Chemicals.ChemicalStages.Raw);
    }
}
