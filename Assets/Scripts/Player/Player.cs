using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public Animator animator;
    public Projectile projectile;
    public float cooldown;
    
    private float _timer;
    private Vector2 movement;

    void Start()
    {
        _timer = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Horizontal",movement.x);
        animator.SetFloat("Vertical",movement.y);
        if (movement.x == 0 & movement.y == 0)
        {
            animator.SetBool("IsIdle",true);
        }
        else
        {
            animator.SetBool("IsIdle",false);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed);
    }

    private void Shoot()
    {
        _timer += Time.deltaTime;
        if (Input.GetMouseButton(0) && _timer > 1/ cooldown)
        {
            projectile.targetTag = "Enemy";
            projectile.CreateProjectileToMousePosition(transform.position);
            _timer = 0;
        }
    }
}
