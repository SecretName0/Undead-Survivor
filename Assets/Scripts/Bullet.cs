using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Damage;
    public int Per; // �����?

    Rigidbody2D rb;

    [SerializeField] float BulletSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Init(float Damage, int Per, Vector3 Dir)
    {
        this.Damage = Damage; // Ŭ������ ����� = �Ű� ������ �����
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

        Per--; // ������� �ִ� ��� �ϳ��� ���� ������ ������ ��ġ ����

        if(Per == -1)
        {
            rb.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }
}
