using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public ItemData.ItemTypes Type;
    public float Rate; // ������ ������

    public void Init(ItemData data)
    {
        // Basic Set
        name = "Gear " + data.ItemID;
        transform.parent = GameManager.Instance.Player.transform;
        transform.localScale = Vector3.zero;

        // Property Set
        Type = data.ItemType;
        Rate = data.Damages[0];

        ApplyGearAbility();
    }

    public void LevelUp(float rate)
    {
        Rate = rate;
        ApplyGearAbility();
    }

    void ApplyGearAbility()
    {
        switch(Type)
        {
            case ItemData.ItemTypes.Glove:
                RateUp();
                break;

            case ItemData.ItemTypes.Shoe:
                SpeedUp();
                break;
        }
    }

    void RateUp()
    {
        Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();

        foreach (Weapon weapon in weapons)
        {
            switch(weapon.ID)
            {
                case 0:
                    weapon.Speed = 150 + (150 * Rate);

                    break;

                default:
                    weapon.Speed = 0.5f * (1f - Rate);
                    // 1���� Rate�� ���� ������ �������� �⺻ �� 0.5�� �� ���� ���� ���� ���ڰ� ������ �ӵ��� �������� �ȴ�

                    break;
            }
        }
    }

    void SpeedUp()
    {
        float MoveSpeed = 3;
        GameManager.Instance.Player.MoveSpeed = MoveSpeed + (MoveSpeed * Rate);
    }
}
