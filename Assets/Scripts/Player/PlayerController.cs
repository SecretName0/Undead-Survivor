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
        // 컴포넌트가 비활성화 되어 있으면 걸러지나, 인자값으로 true를 전달해주면 비활성화 대상도 저장된다.
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
            // i가 2인 이유: 플레이어 계층 구조상 본체(0) 그림자(1)까지는 필요하지만 다음부터는 없어도 되기때문에 2번 부터
            // childCount: 자식 오브젝트의 갯수
            for(int i = 2; i < transform.childCount; i++)
            {
                // GetChild: 주어진 인덱스의 자식 오브젝트를 반환하는 함수
                transform.GetChild(i).gameObject.SetActive(false);
            }

            anim.SetBool("IsDead", true);

            GameManager.Instance.GameOver();
        }
    }
}
