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

        rt.localScale = Vector3.one; // 모든 축 스케일을 1으로
        GameManager.Instance.TimeStop();
    }

    public void HideUI()
    {
        rt.localScale = Vector3.zero; // 모든 축 스케일을 0으로
        GameManager.Instance.TimeResume();
    }

    public void Select(int idx)
    {
        Items[idx].OnClick();
    }

    void Next()
    {
        // 1. 모든 아이템 비활성화
        foreach (Item item in Items)
        {
            item.gameObject.SetActive(false);
        }

        // 2. 그 중 랜덤한 3개 아이템만 활성화
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

            // 3. 만랩 아이템의 경우, 소비 아이템으로 대체
            if(RandItem.Level == RandItem.ItemInfo.Damages.Length)
            {
                // 랜덤으로 배정된 무기의 레벨이, 그 랜덤 아이템의 정보 속 대미지 배열의 길이와 같다면 (즉, 만랩이라면)
                Items[4].gameObject.SetActive(true);
            }

            else
                RandItem.gameObject.SetActive(true);
        }

    }
}
