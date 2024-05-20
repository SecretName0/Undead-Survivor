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
        GameObject SpawnTarget = GameManager.Instance.om.GetSpawnTarget(Random.Range(0, 2)); // 랜덤하게 좀비나 스켈레톤 생성

        SelectSpawnArea(SpawnTarget);
    }

    void SelectSpawnArea(GameObject SpawnTarget)
    {
        SpawnTarget.transform.position = SpawnPoints[Random.Range(1, SpawnPoints.Length)].position;
        // 해당되는 포인트의 배열의 0번째, 즉 가장 첫번째 요소는 자기 자신이다. 그렇기에 자기 자신은 필요가 없으니까 1부터 시작한다.
    }
}
