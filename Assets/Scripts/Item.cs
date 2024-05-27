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
        Icon = GetComponentsInChildren<Image>()[1]; // InChildren�� ��� �ڱ� �ڽ��� 0��°�� ���Խ�Ų �ڽ� ������Ʈ������ �迭�̱⿡ ù��° �׸��� ���������� ����
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
                // �и��� ������ Ÿ���� ���� ó��
                if(Level == 0)
                {
                    GameObject NewWeapon = new GameObject();
                    // �и��� ������ Ÿ���� �޾��� �� ���ο� �� ������Ʈ�� ����

                    weapon = NewWeapon.AddComponent<Weapon>();
                    // �� ������Ʈ�� ���� ������Ʈ�� ���ϰ� �׸� �̸� ������ weapon�� ����

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
                    // �۷���� �Ź� Ÿ���� �޾��� �� ���ο� �� ������Ʈ�� ����

                    gear = NewGear.AddComponent<Gear>();
                    // �� ������Ʈ�� ��� ������Ʈ�� ���ϰ� �׸� �̸� ������ gear�� ����

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
