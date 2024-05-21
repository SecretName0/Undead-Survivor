using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] SpawnPoints;
    public SpawnData[] SP_Data;

    float Timer;
    [SerializeField] int Level;
    [SerializeField] float LevelUpTimer;

    private void Awake()
    {
        SpawnPoints = GetComponentsInChildren<Transform>();
    }

    private void Update()
    {
        SpawnMonster();

        Level = Mathf.Min(Mathf.FloorToInt(GameManager.Instance.GameTime / LevelUpTimer), SP_Data.Length -1); // LevelUpTimer에서 지정한 시간마다 레벨이 오름 (초단위)
        // FloorToInt 소수점 아래는 버리고 int형으로 바꿔줌
    }

    void SpawnMonster()
    {
        Timer += Time.deltaTime;

        if (Timer > (SP_Data[Level].SpawnDelay)) // SpawnData의 현재 레벨만큼의 인덱스 정보에 있는 스폰 딜레이 참조
        {
            SelectMonster();

            Timer = 0;
        }
    }

    void SelectMonster()
    {
        // GameObject SpawnTarget = GameManager.Instance.om.GetSpawnTarget(Random.Range(0, 2)); // 랜덤하게 좀비나 스켈레톤 생성

        GameObject SpawnTarget = GameManager.Instance.om.GetSpawnTarget(0); // (최신) 레벨에 따라 처리하지 않고, 그냥 1개의 적 데이터만 사용해 시간에 따라 변화하게 설정
        //GameObject SpawnTarget = GameManager.Instance.om.GetSpawnTarget(Level); // 레벨에 따라 좀비나 스켈레톤 생성 (현재는 2레벨 까지만 유효)

        SelectSpawnArea(SpawnTarget);
    }

    void SelectSpawnArea(GameObject SpawnTarget)
    {
        SpawnTarget.transform.position = SpawnPoints[Random.Range(1, SpawnPoints.Length)].position;
        // 해당되는 포인트의 배열의 0번째, 즉 가장 첫번째 요소는 자기 자신이다. 그렇기에 자기 자신은 필요가 없으니까 1부터 시작한다.

        SpawnTarget.GetComponent<EnemyController>().Init(SP_Data[Level]);
    }
}

[System.Serializable]
public class SpawnData
{
    // To Do List -> Sptrite Type / Spawn Delay / HP / Speed
    public int SpriteType;
    public float SpawnDelay;
    public int HP;
    public float MonsterSpeed;

    // 추가 클래스를 유니티 인스펙터에서 보고 사용하고 싶을 때, [System.Serializable]을 통해 직렬화함
}
