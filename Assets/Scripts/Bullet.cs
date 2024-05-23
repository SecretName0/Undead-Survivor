using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Damage;
    public int Per; // 관통력?

    Rigidbody2D rb;

    [SerializeField] float BulletSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Init(float Damage, int Per, Vector3 Dir)
    {
        this.Damage = Damage; // 클래스의 대미지 = 매개 변수의 대미지
        this.Per = Per;

        if(Per > -1)
        {
            rb.velocity = Dir * BulletSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") || Per == -1)
            return;

        Per--; // 관통력이 있는 경우 하나의 적을 관통할 때마다 수치 감소

        if(Per == -1)
        {
            rb.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }
}
