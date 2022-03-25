using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed;
    private Vector2 _direction;
    private Animator _animator;
    private static readonly int YDir = Animator.StringToHash("yDir");
    private static readonly int XDir = Animator.StringToHash("xDir");

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        TakeInput();
        Move();
    }

    private void TakeInput()
    {
        _direction = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            _direction += Vector2.up;
        }
        if (Input.GetKey(KeyCode.S))
        {
            _direction += Vector2.down;
        }
        if (Input.GetKey(KeyCode.A))
        {
            _direction += Vector2.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            _direction += Vector2.right;
        }
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed=8;
        }
    }

    private void Move()
    {
        transform.Translate(_direction * (speed * Time.deltaTime));
        SetAnimationDirection(_direction);
    }

    private void SetAnimationDirection(Vector2 direction)
    {
        _animator.SetFloat("xDir", direction.x);
    }
}
