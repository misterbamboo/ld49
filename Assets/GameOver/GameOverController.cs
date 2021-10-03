using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
	[SerializeField] private GameObject _gameoverScreen;
	[SerializeField] private float _reloadSceneTime = 5f;
	private void Start()
	{
		SubscribeEvents();
		_gameoverScreen.SetActive(false);
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
		_gameoverScreen.SetActive(true);
		StartCoroutine(RestartGame());
	}

	private IEnumerator RestartGame()
	{
		yield return new WaitForSeconds(_reloadSceneTime);

		Scene scene = SceneManager.GetActiveScene();
		SceneManager.LoadScene(scene.name);
	}
}
