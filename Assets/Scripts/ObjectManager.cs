using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    // Prefabs
    public GameObject[] Prefabs;

    // Object List
    List<GameObject>[] Pool;

    private void Awake()
    {
        InitPoolSize();

        InitPool();
    }

    void InitPoolSize()
    {
        Pool = new List<GameObject>[Prefabs.Length];
    }

    void InitPool()
    {
        for (int i = 0; i < Pool.Length; i++)
        {
            Pool[i] = new List<GameObject>();
        }
    }

    public GameObject GetSpawnTarget(int i)
    {
        GameObject Select = null;

        // 선택한 풀의 놀고 있는 오브젝트 접근
        // 발견하여 접근하면 Select에 할당

        foreach (GameObject go in Pool[i])
        {
            if (!go.activeSelf)
            {
                Select = go;
                Select.SetActive(true);

                break;
            }
        }
        
        // 모두 사용중이라 접근을 할 수 없는 경우 새로 생성하여 Select에 할당
        if(!Select)
        {
            Select = Instantiate(Prefabs[i], transform); // transform은 안써도 되지만 그러지 않으면 창에 지저분하게 나옴

            Pool[i].Add(Select);
        }

        return Select;
    }
}
