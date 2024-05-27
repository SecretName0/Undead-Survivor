using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ITEM", menuName = "Scriptable Object/ItemData")]
public class ItemData : ScriptableObject
{
    // CreateAssetMenu: 커스텀 메뉴를 생성하는 속성
    // 해당하는 속성을 지정하면 엔진의 생성탭에 만들어준 설정 값이 존재하며, 이를 통해 아이템등을 만들기 용이하다.

    public enum ItemTypes { Melee, Range, Glove, Shoe, Healing }

    [Header("# Main Info")]
    public ItemTypes ItemType;
    public int ItemID;
    public string ItemName;
    public string ItemDesc; // 아이템의 설명
    public Sprite ItemIcon;

    [Header("# Level Data")]
    public float BaseDamage;
    public int BasePerCount; // 관통 횟수 및 아이템의 갯수로 이용
    public float[] Damages; // 이름 그대로 대미지의 비율을 증가 시키거나, 글러브등의 경우 연사력의 증가등의 요소로 이용
    public int[] PerCount;

    [Header("# Weapon Data")]
    public GameObject Projectile;
    public Sprite Hand;

}
