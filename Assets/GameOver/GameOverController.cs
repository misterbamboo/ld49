using UnityEngine;

public class GameOverController : MonoBehaviour
{
	private void Start()
	{
		SubscribeEvents();
		gameObject.SetActive(false);
	}

	private void OnDestroy()
	{
		UnsubscribeEvents();
	}

	private void SubscribeEvents()
	{
		if (Flower.Instance != null)
		{
			Flower.Instance.onDied += Flower_onDied;
		}
		foreach (PlayerController player in GameManager.Instance.Players)
		{
			player.onDied += GameOver;
		}
	}

	private void UnsubscribeEvents()
	{
		if (Flower.Instance != null)
		{
			Flower.Instance.onDied -= Flower_onDied;
		}
		foreach (PlayerController player in GameManager.Instance.Players)
		{
			player.onDied -= GameOver;
		}
	}

	private void Flower_onDied()
	{
		GameOver();
	}

	private void GameOver()
	{
		gameObject.SetActive(true);
	}
}
