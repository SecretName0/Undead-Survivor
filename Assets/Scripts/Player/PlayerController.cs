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
    public RuntimeAnimatorController[] rc;

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

    private void OnEnable()
    {
        MoveSpeed *= Character.BonusSpeed;

        anim.runtimeAnimatorController = rc[GameManager.Instance.PlayerID];
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

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!GameManager.Instance.TimeLive)
            return;

        GameManager.Instance.HP -= Time.deltaTime * 10;

        if(GameManager.Instance.HP < 0)
        {   
            // i�� 2�� ����: �÷��̾� ���� ������ ��ü(0) �׸���(1)������ �ʿ������� �������ʹ� ��� �Ǳ⶧���� 2�� ����
            // childCount: �ڽ� ������Ʈ�� ����
            for(int i = 2; i < transform.childCount; i++)
            {
                // GetChild: �־��� �ε����� �ڽ� ������Ʈ�� ��ȯ�ϴ� �Լ�
                transform.GetChild(i).gameObject.SetActive(false);
            }

            anim.SetBool("IsDead", true);

            GameManager.Instance.GameOver();
        }
    }
}
