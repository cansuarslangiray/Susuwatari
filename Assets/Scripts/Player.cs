using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 _direction;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _gravity = -9.8f;
    [SerializeField] private float strength = 5f;
    private Animator _animator;
    [SerializeField] private GameObject gameManager;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }


    private void Update()
    {
        Movement();
    }

    private void OnEnable()
    {
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
        _direction = Vector3.zero;
    }

    private void Movement()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _animator.SetBool("isFly", true);

            _direction = Vector3.up * strength;
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                //  _animator.SetBool("isFly", true);

                _direction = Vector3.up * strength;
            }
        }

        StartCoroutine(FlayingRoutine());

        _direction.y += _gravity * Time.deltaTime;
        transform.position += _direction * Time.deltaTime;
    }

    IEnumerator FlayingRoutine()
    {
        yield return new WaitForSeconds(0.20f);
        _animator.SetBool("isFly", false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            gameManager.GetComponent<GameManager>().GameOver();
            _animator.SetBool("isFalling", true);
        }
        else if (other.gameObject.tag == "Scoring")
        {
            gameManager.GetComponent<GameManager>().IncreaseScore();
        }
    }
}