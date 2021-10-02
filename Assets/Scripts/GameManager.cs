using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance => _instance;
	private static GameManager _instance;

	public List<PlayerController> Players = new List<PlayerController>();

	private void Awake()
	{
		if (_instance == null)
		{
			_instance = this;
		}
	}
}
