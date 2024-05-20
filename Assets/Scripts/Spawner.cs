using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] SpawnPoints;

    float Timer;

    private void Awake()
    {
        SpawnPoints = GetComponentsInChildren<Transform>();
    }

    private void Update()
    {
        SpawnMonster();
    }

    void SpawnMonster()
    {
        Timer += Time.deltaTime;

        if (Timer > 3)
        {
            SelectMonster();

            Timer = 0;
        }
    }

    void SelectMonster()
    {
        GameObject SpawnTarget = GameManager.Instance.om.GetSpawnTarget(Random.Range(0, 2)); // �����ϰ� ���� ���̷��� ����

        SelectSpawnArea(SpawnTarget);
    }

    void SelectSpawnArea(GameObject SpawnTarget)
    {
        SpawnTarget.transform.position = SpawnPoints[Random.Range(1, SpawnPoints.Length)].position;
        // �ش�Ǵ� ����Ʈ�� �迭�� 0��°, �� ���� ù��° ��Ҵ� �ڱ� �ڽ��̴�. �׷��⿡ �ڱ� �ڽ��� �ʿ䰡 �����ϱ� 1���� �����Ѵ�.
    }
}
