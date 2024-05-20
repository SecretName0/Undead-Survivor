using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [NonSerialized] public Vector2 InputVector;
    float Hori;
    float Vert;

    // Character Value
    [SerializeField] float MoveSpeed;

    // Components
    [NonSerialized] public Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        GameManager.Instance.Player = this;

        EnemyController.Target = GameManager.Instance.Player.rb;
    }


    private void FixedUpdate()
    {
        /* 물리 이동의 기본적인 논리 순서
        // 힘을 가함
        rb.AddForce(InputVector);

        // 속도 제어
        rb.velocity = InputVector;

        // 위치 이동
        rb.MovePosition(rb.position + InputVector);
        */

        Vector2 NextVector = InputVector * MoveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + NextVector);
    }

    void Update()
    {
        UpdateAnimation();
    }

    private void LateUpdate()
    {
        if (InputVector.x < 0)
        {
            sr.flipX = true;
        }

        else if(InputVector.x > 0)
        {
            sr.flipX = false;
        }
    }

    void InputValue()
    {

    }

    void OnMove(InputValue Value)
    {
        InputVector = Value.Get<Vector2>();
    }

    void OnJump()
    {

    }

    void UpdateAnimation()
    {
        if(InputVector != Vector2.zero)
        {
            anim.SetBool("IsMoving", true);
        }

        else
        {
            anim.SetBool("IsMoving", false);
        }
    }
}
