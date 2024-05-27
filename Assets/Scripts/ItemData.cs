using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ITEM", menuName = "Scriptable Object/ItemData")]
public class ItemData : ScriptableObject
{
    // CreateAssetMenu: Ŀ���� �޴��� �����ϴ� �Ӽ�
    // �ش��ϴ� �Ӽ��� �����ϸ� ������ �����ǿ� ������� ���� ���� �����ϸ�, �̸� ���� �����۵��� ����� �����ϴ�.

    public enum ItemTypes { Melee, Range, Glove, Shoe, Healing }

    [Header("# Main Info")]
    public ItemTypes ItemType;
    public int ItemID;
    public string ItemName;
    public string ItemDesc; // �������� ����
    public Sprite ItemIcon;

    [Header("# Level Data")]
    public float BaseDamage;
    public int BasePerCount; // ���� Ƚ�� �� �������� ������ �̿�
    public float[] Damages; // �̸� �״�� ������� ������ ���� ��Ű�ų�, �۷������ ��� ������� �������� ��ҷ� �̿�
    public int[] PerCount;

    [Header("# Weapon Data")]
    public GameObject Projectile;
    public Sprite Hand;

}
