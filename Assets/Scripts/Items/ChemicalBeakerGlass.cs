using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemicalBeakerGlass : MonoBehaviour
{
    public const string Tag = "ChemicalBeakerGlass";

    [SerializeField] public GameObject glassContent;

    [SerializeField] private PickableObject _pickableObject;

    [SerializeField] private float _max = 3.95f;

    [SerializeField] private float _value = 0;

    [SerializeField] private float _timeToEmpty = 1f;

    private Vector3 _initialGlassContentScale;

    private float _initialGlassOffsetYOffset;

    private float _emptyAnimationTime;

    private void Start()
    {
        _initialGlassContentScale = glassContent.transform.localScale;
        _initialGlassOffsetYOffset = glassContent.transform.localPosition.y - _initialGlassContentScale.y;

        _pickableObject.OnPickup += GlassOnPickupHandler;
    }

    private void GlassOnPickupHandler()
    {
        transform.rotation = Quaternion.identity;
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

    public void EmptyGlass()
    {
        var chemicalItem = gameObject.AddComponent<ChemicalItem>();
        if (chemicalItem)
        {
            Destroy(chemicalItem);
        }

        StartCoroutine(EmptyGlassAnim());
    }

    public IEnumerator EmptyGlassAnim()
    {
        _emptyAnimationTime = _timeToEmpty;
        while (_emptyAnimationTime > 0)
        {
            _emptyAnimationTime -= Time.deltaTime;
            var ratio = _emptyAnimationTime / _timeToEmpty;
            var t = 1 - ratio;

            _value = Mathf.Lerp(_max, 0, t);
            yield return 0;
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
