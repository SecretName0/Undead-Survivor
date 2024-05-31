using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [NonSerialized] public Vector2 InputVector;

    public Scanner scanner;

    public Hands[] Hand;

    // Character Value
    public float MoveSpeed;

    // Components
    [NonSerialized] public Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();

        Hand = GetComponentsInChildren<Hands>(true);
        // ������Ʈ�� ��Ȱ��ȭ �Ǿ� ������ �ɷ�����, ���ڰ����� true�� �������ָ� ��Ȱ��ȭ ��� ����ȴ�.
    }

    void Start()
    {
        GameManager.Instance.Player = this;
    }


    private void FixedUpdate()
    {
        if (!GameManager.Instance.TimeLive)
            return;

        /* ���� �̵��� �⺻���� �� ����
        // ���� ����
        rb.AddForce(InputVector);

        // �ӵ� ����
        rb.velocity = InputVector;

        // ��ġ �̵�
        rb.MovePosition(rb.position + InputVector);
        */

        Vector2 NextVector = InputVector * MoveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + NextVector);
    }

    void Update()
    {
        if (!GameManager.Instance.TimeLive)
            return;

        UpdateAnimation();
    }

    private void LateUpdate()
    {
        if (!GameManager.Instance.TimeLive)
            return;

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
