using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float MoveSpeed;
    public static Rigidbody2D Target;

    bool IsDead;

    Rigidbody2D rb;
    SpriteRenderer sr;

    Vector2 DirVec;
    Vector2 NextVec;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if(IsDead)
            return;

        DirVec = Target.position - rb.position;
        NextVec = DirVec.normalized * MoveSpeed * Time.fixedDeltaTime;

        rb.MovePosition(NextVec + rb.position);
        rb.velocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        if (IsDead)
            return;

        if (DirVec.x < 0)
            sr.flipX = true;

        else if(DirVec.x >0)
            sr.flipX = false;
    }

    public void OnSpawned()
    {
        Target = GameManager.Instance.Player.GetComponent<Rigidbody2D>();
    }
}
