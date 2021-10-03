using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Chemicals;
using Assets.Scripts.Items;
using UnityEngine;

public class WaterPump : InstrumentBase
{
    private const double PediodPerSec = 2 * Math.PI;

    [SerializeField] private GameObject _pumpHandle;

    [SerializeField] private ChemicalMaterialsScriptableObject _chemicalMaterials;

    [SerializeField] private float _timeNeededToPump = 2f;

    private bool _isPumping = false;
    private float _currentPumpingTime = 0f;
    private List<ChemicalBeakerGlass> _snapedChemicalBeakerGlass = new List<ChemicalBeakerGlass>();

    private ChemicalBeakerGlass CurrentGlass => _snapedChemicalBeakerGlass.First();

    private void Awake()
    {
        InstrumentType = InstrumentType.WaterPump;
    }

    private void Start()
    {
        UpdateHandleAngle();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == PickableObject.Tag && other.TryGetComponent(out ChemicalBeakerGlass snappableSurface))
        {
            _snapedChemicalBeakerGlass.Add(snappableSurface);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == PickableObject.Tag && other.TryGetComponent(out ChemicalBeakerGlass snappableSurface))
        {
            _snapedChemicalBeakerGlass.Remove(snappableSurface);
        }
    }

    private void Update()
    {
        if (_isPumping)
        {
            _currentPumpingTime += Time.deltaTime;
            if (_currentPumpingTime >= _timeNeededToPump)
            {
                PumpingDone();
            }
            else
            {
                FillGlass();
            }
        }

        UpdateContentRotation();
    }

    private void FillGlass()
    {
        CurrentGlass.Fill(Time.deltaTime);
    }

    private void UpdateContentRotation()
    {
        if (_isPumping)
        {
            UpdateHandleAngle();
        }
    }

    private void UpdateHandleAngle()
    {
        float angleOffset = GetAngleOffset();
        _pumpHandle.transform.localRotation = Quaternion.Euler(new Vector3(angleOffset, 0, 0));
    }

    private float GetAngleOffset()
    {
        // See https://www.desmos.com/calculator/d3z2u2e3gr
        var reverseCos = (float)-(Math.Cos(PediodPerSec * _currentPumpingTime) + 1);
        var angleOffset = reverseCos * 10f;
        return angleOffset;
    }

    public override bool AddChemicalItem(IChemicalItem chemical)
    {
        return false;
    }

    public override bool Use()
    {
        if (_snapedChemicalBeakerGlass.Count == 0)
        {
            // TODO empty feedback
            return false;
        }
        else
        {
            StartPumping();
            return true;
        }
    }

    public override void StopUsing()
    {
        InterruptPumping();
    }

    private void StartPumping()
    {
        Debug.Log("Start using WaterPump");

        var color = _chemicalMaterials.GetElementColor(ChemicalElements.Blue);
        CurrentGlass.SwitchColor(color);

        _isPumping = true;
        _currentPumpingTime = 0f;
    }

    private void PumpingDone()
    {
        _isPumping = false;
        Debug.Log("Done using WaterPump");
        OnTaskDone?.Invoke();
    }

    private void InterruptPumping()
    {
        _isPumping = false;
        Debug.Log("WaterPump interrupted");
    }

    public override InstrumentFinishedContent RemoveFinishedContent()
    {
        // WaterPump never hold an element
        return null;
    }

    public override bool HasFinishedContent()
    {
        // WaterPump never hold an element
        return false;
    }
}
