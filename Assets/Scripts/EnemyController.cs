using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float MoveSpeed;
    [SerializeField] float HP;
    [SerializeField] float MaxHP;
    [SerializeField] RuntimeAnimatorController[] RAC;

    public static Rigidbody2D Target;

    bool IsDead;

    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;

    Vector2 DirVec;
    Vector2 NextVec;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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

        // IsLive가 강의중 쓰이지만 여기선 IsDead 사용중이라 우선 논외
        HP = MaxHP;
    }

    public void Init(SpawnData Data)
    {
        anim.runtimeAnimatorController = RAC[Data.SpriteType];
        // 애니메이터의 런타임 애니메이터 컨트롤러 정보를 우리가 사전에 정의한 RAC[데이터.스프라이트 타입] 인덱스의 정보로 저장한다

        MoveSpeed = Data.MonsterSpeed;
        MaxHP = Data.HP;
        HP = Data.HP;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet"))
        {
            HP -= collision.GetComponent<Bullet>().Damage;

            if(HP > 0)
            {
                // Hit Reaction
            }
            else
            {
                OnDead();
            }
        }
    }

    void OnDead()
    {
        IsDead = true;
        gameObject.SetActive(false);
    }
}
