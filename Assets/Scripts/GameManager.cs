using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int _score;

    public void GameOver(GameObject  gameObject)
    {
        Destroy(gameObject);
        Debug.Log("Game over");
    }

    public void IncreaseScore()
    {
        _score++;
    }
}
