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

        // ������ Ǯ�� ��� �ִ� ������Ʈ ����
        // �߰��Ͽ� �����ϸ� Select�� �Ҵ�

        foreach (GameObject go in Pool[i])
        {
            if (!go.activeSelf)
            {
                Select = go;
                Select.SetActive(true);

                break;
            }
        }
        
        // ��� ������̶� ������ �� �� ���� ��� ���� �����Ͽ� Select�� �Ҵ�
        if(!Select)
        {
            Select = Instantiate(Prefabs[i], transform); // transform�� �Ƚᵵ ������ �׷��� ������ â�� �������ϰ� ����

            Pool[i].Add(Select);
        }

        return Select;
    }
}
