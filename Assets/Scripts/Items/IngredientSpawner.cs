using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IngredientSpawner : MonoBehaviour
{
	[SerializeField] private List<IngredientProbability> _ingredientProbabilities = new List<IngredientProbability>();
	[SerializeField] private Transform _spawnPoint;
	[SerializeField] private float _spawnInterval = 2f;
	private float _currentTime = 0f;
	private void Update()
	{
		_currentTime += Time.deltaTime;
		if (_spawnInterval <= _currentTime)
		{
			SpawnIngredient();
			_currentTime = 0;
		}
	}

	private void SpawnIngredient()
	{
		GameObject ingredientToSpawn = GetRandomIngredient();
		Instantiate(ingredientToSpawn, _spawnPoint.position, Quaternion.identity);
	}

	private GameObject GetRandomIngredient()
	{
		float probSum = GetIngredientProbabilitySum();
		float randomProb = UnityEngine.Random.Range(0f, probSum);
		float currentProb = 0f;

		foreach (IngredientProbability prob in _ingredientProbabilities)
		{
			currentProb += prob.Probability;
			if (randomProb <= currentProb)
			{
				return prob.GameObject;
			}
		}

		return null;
	}

	private float GetIngredientProbabilitySum()
	{
		return _ingredientProbabilities.Sum(i => i.Probability);
	}
}

[Serializable]
public struct IngredientProbability
{
	public GameObject GameObject;
	[Range(0f, 1f)]
	public float Probability;
}
