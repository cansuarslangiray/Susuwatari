using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    private Vector3 _direction;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _gravity = -9.8f;
    [SerializeField] private float strength = 5f;
    private Animator _animator;
    [SerializeField] private GameObject gameManager;
    public bool isDeath;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }


    private void Update()
    {
        Movement();
    }

    private void OnEnable()
    {
        Debug.Log("onablke");
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
        _direction = Vector3.zero;
        _animator.SetBool("isFalling", false);
    }

    private void Movement()
    {
        if (!isDeath)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !_animator.GetBool("isFalling"))
            {
                _animator.SetBool("isFly", true);

                _direction = Vector3.up * strength;
            }

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    _animator.SetBool("isFly", true);

                    _direction = Vector3.up * strength;
                }
            }
        }

        _direction.y += _gravity * Time.deltaTime;
        transform.position += _direction * Time.deltaTime;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            isDeath = true;
            transform.GetComponent<CircleCollider2D>().enabled = false;

            for (int i = 0; i < other.transform.parent.childCount; i++)
            {
                other.transform.parent.GetChild(i).GetComponent<BoxCollider2D>().enabled = false;
            }

            Debug.Log("ontrigger");
            gameManager.GetComponent<GameManager>().GameOver();
            _animator.SetBool("isFalling", true);
        }
        else if (other.gameObject.tag == "Scoring")
        {
            gameManager.GetComponent<GameManager>().IncreaseScore();
        }
    }
}