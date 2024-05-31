using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystemUI : MonoBehaviour
{
    RectTransform rt;
    Item[] Items;

    private void Awake()
    {
        rt = GetComponent<RectTransform>();
        Items = GetComponentsInChildren<Item>(true);
    }

    public void ShowUI()
    {
        Next();

        rt.localScale = Vector3.one; // ��� �� �������� 1����
        GameManager.Instance.TimeStop();
    }

    public void HideUI()
    {
        rt.localScale = Vector3.zero; // ��� �� �������� 0����
        GameManager.Instance.TimeResume();
    }

    public void Select(int idx)
    {
        Items[idx].OnClick();
    }

    void Next()
    {
        // 1. ��� ������ ��Ȱ��ȭ
        foreach (Item item in Items)
        {
            item.gameObject.SetActive(false);
        }

        // 2. �� �� ������ 3�� �����۸� Ȱ��ȭ
        int[] Rand = new int[3];
        
        while(true)
        {
            Rand[0] = Random.Range(0, Items.Length);

            Rand[1] = Random.Range(0, Items.Length);

            Rand[2] = Random.Range(0, Items.Length);

            if (Rand[0] != Rand[1] && Rand[0] != Rand[2] && Rand[1] != Rand[2])
                break;
        }
        
        for(int i = 0; i < Rand.Length; i++)
        {
            Item RandItem = Items[Rand[i]];

            // 3. ���� �������� ���, �Һ� ���������� ��ü
            if(RandItem.Level == RandItem.ItemInfo.Damages.Length)
            {
                // �������� ������ ������ ������, �� ���� �������� ���� �� ����� �迭�� ���̿� ���ٸ� (��, �����̶��)
                Items[4].gameObject.SetActive(true);
            }

            else
                RandItem.gameObject.SetActive(true);
        }

    }
}
