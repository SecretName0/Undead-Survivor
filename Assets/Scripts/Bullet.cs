using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Damage;
    public int Per; // �����?

    public void Init(float Damage, int Per)
    {
        this.Damage = Damage; // Ŭ������ ����� = �Ű� ������ �����
        this.Per = Per;
    }
}
