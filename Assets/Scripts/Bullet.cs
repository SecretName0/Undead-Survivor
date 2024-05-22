using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Damage;
    public int Per; // 관통력?

    public void Init(float Damage, int Per)
    {
        this.Damage = Damage; // 클래스의 대미지 = 매개 변수의 대미지
        this.Per = Per;
    }
}
