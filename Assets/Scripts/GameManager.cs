using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class GameManager : MonoBehaviour
{
    private int _score;
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject table;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private Player player;
    [SerializeField] private GameObject startGame;
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject border;
    [SerializeField] private Animator tableAnimator;
    private AnimatorStateInfo _stateInfo;


    private void Awake()
    {
        Debug.Log("awawk");
        Application.targetFrameRate = 60;
        Pause();
    }

    private void Update()
    {
        _stateInfo = tableAnimator.GetCurrentAnimatorStateInfo(0);
        if (_stateInfo.IsName("Base Layer.GameOverTable"))
        {
            if (_stateInfo.normalizedTime >= 0.95)
            {
                table.transform.GetChild(0).GetComponent<Button>().interactable = true;
            }
        }
    }

    public void Play()
    {
        player.isDeath = false;
        player.transform.GetComponent<CircleCollider2D>().enabled = true;
        
        for (int i = 0; i < border.transform.childCount; i++)
        {
            border.transform.GetChild(i).GetComponent<BoxCollider2D>().enabled = true;
        }

        Pipes[] pipes = FindObjectsOfType<Pipes>();
        for (int i = 0; i < pipes.Length; i++)
        {
            Destroy(pipes[i].gameObject);
        }

        Time.timeScale = 1f;
        _score = 0;
        scoreText.text = _score.ToString();
        scoreText.enabled = true;
        background.transform.GetChild(0).GetComponent<Parallax>().animationSpeed = 0.05f;
        background.transform.GetChild(1).GetComponent<Parallax>().animationSpeed = 0.06f;
        background.transform.GetChild(2).GetComponent<Parallax>().animationSpeed = 0.07f;
        player.enabled = true;
        table.SetActive(false);
        gameOver.SetActive(false);
        startGame.SetActive(false);
        table.GetComponent<Animator>().SetBool("isGameOver", false);   
        Debug.Log("play");

    }

    private void Pause()
    {
        Debug.Log("Pause");
        Time.timeScale = 0;
        player.enabled = false;
    }

    public void GameOver()
    {

        Debug.Log("Gameover");
        table.SetActive(true);
        player.GetComponent<Animator>().SetBool("isFly", false);

        table.GetComponent<Animator>().SetBool("isGameOver", true);

        background.transform.GetChild(0).GetComponent<Parallax>().animationSpeed = 0;
        background.transform.GetChild(1).GetComponent<Parallax>().animationSpeed = 0;
        background.transform.GetChild(2).GetComponent<Parallax>().animationSpeed = 0;
        var pipes = GameObject.FindGameObjectsWithTag("Pipe");
        for (int i = 0; i < pipes.Length; i++)
        {
            pipes[i].GetComponent<Pipes>().Speed = 0;
            if (i != 0)
            {
                Destroy(pipes[i].gameObject);
            }
        }

        table.transform.GetChild(2).GetComponent<Text>().text = _score.ToString();
        int highScore = Int32.Parse(table.transform.GetChild(4).GetComponent<Text>().text);
        if (_score > highScore)
        {
            table.transform.GetChild(4).GetComponent<Text>().text = _score.ToString();
            table.transform.GetChild(5).GetComponent<Text>().enabled = true;
        }

        gameOver.SetActive(true);
        scoreText.enabled = false;  
        StartCoroutine(PauseRoutine());

    }

   IEnumerator PauseRoutine()
    {
      
            Debug.Log("PaRoutine");
            yield return new WaitForSeconds(2f);
            Pause();
        
    }

   public void IncreaseScore()
    {
        _score++;
        scoreText.text = _score.ToString();
    }
}