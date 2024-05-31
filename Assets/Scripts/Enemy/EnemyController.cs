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
            return; // anim.GetCurrentAnimatorStateInfo(0).IsName("Hit")�� ���� �ִϸ������� ������ �������µ�, �ŰԺ����� �� ���ڴ� �ִϸ������� ���̾� �ѹ��̴�.
        // �� ������ ��� ���̾�� ���̽� �ϳ� ������, ���� ù���� �ε����� 0�� �ְ�, �� �� Hit�� ������ �����´�.

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
        // �ִϸ������� ��Ÿ�� �ִϸ����� ��Ʈ�ѷ� ������ �츮�� ������ ������ RAC[������.��������Ʈ Ÿ��] �ε����� ������ �����Ѵ�

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
        yield return Wait; // ���� �ϳ��� ���� ������ ����
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
