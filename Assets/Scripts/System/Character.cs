using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public static float BonusSpeed
    {
        // �� ���� ��ü�� [�Լ�]�� �ƴ� [�Ӽ�]�̸� get set�� �����ϴ� ������ ���δ�
        // (���� �м�)
        // �Ӽ��̶� �Ϲ����� �ϳ��� �����̳�, ���ǰ� ��Ȳ�� ���� �ٸ� ���� �ָ� ���������� Ȱ���� �� �ֵ��� ������� ������ ���δ�.
        get { return GameManager.Instance.PlayerID == 0 ? 1.1f : 1; }
        // get { �÷��̾� ���̵� 0���� ��� 10% �ӵ� ������ ���� 1.1�� ��ȯ, �ƴ� ���� �׳� �⺻ �ӵ��� 1�� ��ȯ }
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
