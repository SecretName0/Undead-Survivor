using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float MoveSpeed;
    [SerializeField] float HP;
    [SerializeField] float MaxHP;
    [SerializeField] RuntimeAnimatorController[] RAC;

    [SerializeField] Rigidbody2D Target;
    Collider2D col;

    public bool IsDead;

    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;

    Vector2 DirVec;
    Vector2 NextVec;

    WaitForFixedUpdate Wait;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        Wait = new WaitForFixedUpdate();
        col = GetComponent<Collider2D>();
    }

    private void Start()
    {
        OnSpawned();
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.TimeLive)
            return;

        if (IsDead || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            return; // anim.GetCurrentAnimatorStateInfo(0).IsName("Hit")은 현재 애니메이터의 정보를 가져오는데, 매게변수로 준 숫자는 애니메이터의 레이어 넘버이다.
        // 이 예제의 경우 레이어는 베이스 하나 뿐으로, 가장 첫번쨰 인덱스인 0을 주고, 그 중 Hit의 정보를 가져온다.

        DirVec = Target.position - rb.position;
        NextVec = DirVec.normalized * MoveSpeed * Time.fixedDeltaTime;

        rb.MovePosition(NextVec + rb.position);
        rb.velocity = Vector2.zero;
    }

    private void Update()
    {
        if (!GameManager.Instance.TimeLive)
            return;
    }

    private void LateUpdate()
    {
        if (!GameManager.Instance.TimeLive)
            return;

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

        IsDead = false;
        col.enabled = true;
        rb.simulated = true;
        sr.sortingOrder = 3;
        anim.SetBool("Dead", false);
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
        if(collision.CompareTag("Bullet") || !IsDead)
        {
            HP -= collision.GetComponent<Bullet>().Damage;
            StartCoroutine(KnockBack());

            if(HP > 0)
            {
                // Hit Reaction
                anim.SetTrigger("Hit");
            }
            else
            {
                IsDead = true;
                col.enabled = false;
                rb.simulated = false;
                sr.sortingOrder = 2;
                anim.SetBool("Dead", true);

                GameManager.Instance.KillCount++;
                GameManager.Instance.GetExp();
            }
        }
    }

    IEnumerator KnockBack()
    {
        yield return Wait; // 다음 하나의 물리 프레임 쉬기
        Vector3 PlayerPos = GameManager.Instance.Player.transform.position;
        Vector3 DirVec = transform.position;

        rb.AddForce(DirVec.normalized * 3, ForceMode2D.Impulse);
    }

    void OnDead()
    {
        gameObject.SetActive(false);

        OnSpawned();
    }
}
