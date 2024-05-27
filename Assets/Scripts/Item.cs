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

    private void Awake()
    {
        Icon = GetComponentsInChildren<Image>()[1]; // InChildren의 경우 자기 자신을 0번째로 포함시킨 자식 컴포넌트까지의 배열이기에 첫번째 항목인 아이콘으로 지정
        Icon.sprite = ItemInfo.ItemIcon;

        Text[] Texts = GetComponentsInChildren<Text>();
        TextLevel = Texts[0];
    }

    private void LateUpdate()
    {
        TextLevel.text = "LV." + Level;
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
