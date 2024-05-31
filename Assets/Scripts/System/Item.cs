using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData ItemInfo;

    public int Level;
    public Weapon weapon;
    public Gear gear;

    Image Icon;
    Text TextLevel;
    Text TextName;
    Text TextDesc;

    private void Awake()
    {
        Icon = GetComponentsInChildren<Image>()[1]; // InChildren의 경우 자기 자신을 0번째로 포함시킨 자식 컴포넌트까지의 배열이기에 첫번째 항목인 아이콘으로 지정
        Icon.sprite = ItemInfo.ItemIcon;

        Text[] Texts = GetComponentsInChildren<Text>();
        TextLevel = Texts[0];
        TextName = Texts[1];
        TextDesc = Texts[2];
        // 배열의 순번은 인스펙터에서 배열이 정리되어 있는 순서에 따른다.

        TextName.text = ItemInfo.ItemName;
    }

    private void OnEnable()
    {
        TextLevel.text = "LV." + Level;

        switch(ItemInfo.ItemType)
        {
            case ItemData.ItemTypes.Melee:
            case ItemData.ItemTypes.Range:

                TextDesc.text = string.Format(ItemInfo.ItemDesc, ItemInfo.Damages[Level] *100, ItemInfo.PerCount[Level]);
                // 설명창 세팅 = 아이템 설명 기재, 대미지 기입(표기상 n%로 출력할 것이기 때문에 수치를 백분율화 해준다.), 특수 효과 기입
                break;

            case ItemData.ItemTypes.Glove:
            case ItemData.ItemTypes.Shoe:

                TextDesc.text = string.Format(ItemInfo.ItemDesc, ItemInfo.Damages[Level] * 100);

                break;

            default:

                TextDesc.text = string.Format(ItemInfo.ItemDesc);
                break;
        }

    }

    public void OnClick()
    {
        switch(ItemInfo.ItemType)
        {
            case ItemData.ItemTypes.Melee:
            case ItemData.ItemTypes.Range:
                // 밀리와 레인지 타입은 같이 처리
                if(Level == 0)
                {
                    GameObject NewWeapon = new GameObject();
                    // 밀리와 레인지 타입을 받았을 때 새로운 빈 오브젝트를 생성

                    weapon = NewWeapon.AddComponent<Weapon>();
                    // 빈 오브젝트에 웨폰 컴포넌트를 더하고 그를 미리 선언한 weapon에 저장

                    weapon.Init(ItemInfo);
                }

                else
                {
                    float NextDamage = ItemInfo.BaseDamage;
                    int NextCount = 0;

                    NextDamage += ItemInfo.BaseDamage * ItemInfo.Damages[Level];
                    NextCount += ItemInfo.PerCount[Level];

                    weapon.LevelUp(NextDamage, NextCount);
                }

                Level++;
                break;


            case ItemData.ItemTypes.Glove:
            case ItemData.ItemTypes.Shoe:
                if(Level == 0)
                {
                    GameObject NewGear = new GameObject();
                    // 글러브와 신발 타입을 받았을 때 새로운 빈 오브젝트를 생성

                    gear = NewGear.AddComponent<Gear>();
                    // 빈 오브젝트에 기어 컴포넌트를 더하고 그를 미리 선언한 gear에 저장

                    gear.Init(ItemInfo);
                }

                else
                {
                    float NextRate = ItemInfo.Damages[Level];
                    gear.LevelUp(NextRate);
                }

                Level++;
                break;


            case ItemData.ItemTypes.Healing:
                GameManager.Instance.HP = GameManager.Instance.Max_HP;
                break;
        }

        if(Level == ItemInfo.Damages.Length)
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
