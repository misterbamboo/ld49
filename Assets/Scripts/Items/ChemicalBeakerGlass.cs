using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemicalBeakerGlass : MonoBehaviour
{
    public const string Tag = "ChemicalBeakerGlass";

    [SerializeField] public GameObject glassContent;

    [SerializeField] private float _max = 3.95f;

    [SerializeField] private float _value = 0;

    private Vector3 _initialGlassContentScale;

    private float _initialGlassOffsetYOffset;

    private void Start()
    {
        _initialGlassContentScale = glassContent.transform.localScale;
        _initialGlassOffsetYOffset = glassContent.transform.localPosition.y - _initialGlassContentScale.y;
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
            glassContent.SetActive(_value != 0);
        }
    }

    private bool ShouldGlassContentChangeActiveState()
    {
        bool isEmpty = _value == 0;
        return isEmpty == glassContent.activeInHierarchy;
    }

    private void UpdateGlassContentScale()
    {
        if (glassContent.activeInHierarchy)
        {
            var newYScale = _initialGlassContentScale.y * _value / _max;

            var localPos = glassContent.transform.localPosition;
            localPos.y = newYScale + _initialGlassOffsetYOffset;
            glassContent.transform.localPosition = localPos;

            var scale = _initialGlassContentScale;
            scale.y = newYScale;
            glassContent.transform.localScale = scale;
        }
    }

    public void Fill(float quantity)
    {
        _value += quantity;
        if (_value > _max)
        {
            _value = _max;
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
