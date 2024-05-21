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

        Level = Mathf.Min(Mathf.FloorToInt(GameManager.Instance.GameTime / LevelUpTimer), SP_Data.Length -1); // LevelUpTimer���� ������ �ð����� ������ ���� (�ʴ���)
        // FloorToInt �Ҽ��� �Ʒ��� ������ int������ �ٲ���
    }

    void SpawnMonster()
    {
        Timer += Time.deltaTime;

        if (Timer > (SP_Data[Level].SpawnDelay)) // SpawnData�� ���� ������ŭ�� �ε��� ������ �ִ� ���� ������ ����
        {
            SelectMonster();

            Timer = 0;
        }
    }

    void SelectMonster()
    {
        // GameObject SpawnTarget = GameManager.Instance.om.GetSpawnTarget(Random.Range(0, 2)); // �����ϰ� ���� ���̷��� ����

        GameObject SpawnTarget = GameManager.Instance.om.GetSpawnTarget(0); // (�ֽ�) ������ ���� ó������ �ʰ�, �׳� 1���� �� �����͸� ����� �ð��� ���� ��ȭ�ϰ� ����
        //GameObject SpawnTarget = GameManager.Instance.om.GetSpawnTarget(Level); // ������ ���� ���� ���̷��� ���� (����� 2���� ������ ��ȿ)

        SelectSpawnArea(SpawnTarget);
    }

    void SelectSpawnArea(GameObject SpawnTarget)
    {
        SpawnTarget.transform.position = SpawnPoints[Random.Range(1, SpawnPoints.Length)].position;
        // �ش�Ǵ� ����Ʈ�� �迭�� 0��°, �� ���� ù��° ��Ҵ� �ڱ� �ڽ��̴�. �׷��⿡ �ڱ� �ڽ��� �ʿ䰡 �����ϱ� 1���� �����Ѵ�.

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

    // �߰� Ŭ������ ����Ƽ �ν����Ϳ��� ���� ����ϰ� ���� ��, [System.Serializable]�� ���� ����ȭ��
}
