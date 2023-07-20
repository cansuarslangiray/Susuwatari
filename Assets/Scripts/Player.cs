using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 _direction;
    [SerializeField] private float _gravity = -9.8f;
    [SerializeField] private float strength = 5f;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Movement();
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
                _animator.SetBool("isFly", true);

                _direction = Vector3.up * strength;
            }
        }

        _direction.y += _gravity * Time.deltaTime;
        transform.position += _direction * Time.deltaTime;
    }
}