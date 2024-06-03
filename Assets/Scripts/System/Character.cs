using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public static float BonusSpeed
    {
        // 이 문장 자체는 [함수]가 아닌 [속성]이며 get set을 관리하는 것으로 보인다
        // (추정 분석)
        // 속성이란 일반적인 하나의 변수이나, 조건과 상황에 따라 다른 값을 주며 유동적으로 활용할 수 있도록 만들어진 것으로 보인다.
        get { return GameManager.Instance.PlayerID == 0 ? 1.1f : 1; }
        // get { 플레이어 아이디가 0번인 경우 10% 속도 버프를 위한 1.1을 반환, 아닌 경우는 그냥 기본 속도인 1을 반환 }
    }

    public static float BonusAttackSpeed
    {
        get { return GameManager.Instance.PlayerID == 1 ? 0.9f : 1; }
    }

    public static float BonusAttackRate
    {
        get { return GameManager.Instance.PlayerID == 1 ? 0.9f : 1; }
    }

    public static float BonusDamage
    {
        get { return GameManager.Instance.PlayerID == 2 ? 1.05f : 1; }
    }

    public static float BonusHP
    {
        get { return GameManager.Instance.PlayerID == 3 ? 1.1f : 1; }
    }
}
