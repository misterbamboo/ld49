using Assets.Chemicals;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Flower : MonoBehaviour
{
	public const string Tag = "Flower";

	public static Flower Instance { get; private set; }
	[SerializeField] private Image _progressBar;

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
		if (Died())
		{
			DieEvent();
		}
	}

	private void ReduceTimeToLive()
	{
		_timeToLive -= Time.deltaTime;
		var lifeRatio = _timeToLive / _maxTimeToLive;
		_progressBar.fillAmount = lifeRatio;
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

	public void GiveEffect(FlowerEffects flowerEffect)
	{
		Debug.Log("Flower received the effect: " + flowerEffect);
	}
}
