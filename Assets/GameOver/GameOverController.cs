using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverController : MonoBehaviour
{
    private void Start()
    {
        Flower.Instance.onDied += Flower_onDied;
        this.gameObject.SetActive(false);
    }

    private void Flower_onDied()
    {
        GameOver();
    }

    void GameOver()
    {
        this.gameObject.SetActive(true);
    }
}
