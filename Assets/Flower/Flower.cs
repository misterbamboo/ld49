using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    public static Flower Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private float _maxTimeToLive;

    [SerializeField] private GameObject[] _petals;

    public event Action onDied;

    private float _timeToLive;

    

    void Start()
    {
        _timeToLive = _maxTimeToLive;
    }

    void Update()
    {
        if (Alive())
        {
            CheckLife();
        }
    }

    private void CheckLife()
    {
        ReduceTimeToLive();
        RemoveLostPetals();
        if (Died())
        {
            DieEvent();
        }
    }

    private void ReduceTimeToLive()
    {
        _timeToLive -= Time.deltaTime;
    }

    private void RemoveLostPetals()
    {
        var lifeRatio = _timeToLive / _maxTimeToLive;
        var totalPetals = (double)_petals.Length;
        var wantedPetalCount = (int)Math.Ceiling(totalPetals * lifeRatio);

        if (PetalCountChanged(wantedPetalCount))
        {
            UpdateVisiblePetals(wantedPetalCount);
        }
    }

    private bool PetalCountChanged(int wantedPetalCount)
    {
        return wantedPetalCount != _petals.Length;
    }

    private void UpdateVisiblePetals(int targetCount)
    {
        for (int i = 0; i < _petals.Length; i++)
        {
            var petalVisible = i < targetCount;
            _petals[i].SetActive(petalVisible);
        }
    }

    private bool Alive()
    {
        return !Died();
    }

    private bool Died()
    {
        return _timeToLive <= 0;
    }

    private void DieEvent()
    {
        _timeToLive = 0;
        onDied?.Invoke();
    }
}
