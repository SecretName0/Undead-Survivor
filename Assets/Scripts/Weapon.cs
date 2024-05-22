using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public static Weapon Instance;
    public int ID;
    public int PrefabID;
    public float Damage;
    public int Count; // 갯수
    public float Speed; // 회전 속도

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        RotateSet();
    }

    public void LevelUp(float Damage)
    {
        this.Damage = Damage;
        Count++;

        if(ID == 0)
        {
            WeaponPlaced();
        }
    }

    public void Init()
    {
        switch(ID)
        {
            case 0:
                Speed = 150; // 토탈 값이 마이너스여야 시계 방향으로 회전함 (Rotate 참조)

                WeaponPlaced();
                break;

            default:

                break;
        }
    }

    void WeaponPlaced()
    {
        for (int i = 0; i < Count; i++)
        {
            Transform ChaseWeapon;

            if(i < transform.childCount) // 현재 인덱스가 childcount의 범위 내라면 
            {
                ChaseWeapon = transform.GetChild(i); // 원래부터 소속되어있던 녀석들의 정보를 그대로 사용
            }
            
            else // 그 범주를 벗어났다면?
            {
                ChaseWeapon = GameManager.Instance.om.GetSpawnTarget(PrefabID).transform; // 새롭게 생성
            }

            ChaseWeapon.parent = transform; // 플레이어를 쫓아다닐 무기가 플레이어를 제대로 쫓아다닐 수 있게 만들기 위해 스폰이 된 후 부모를 옮김
            // 스폰만 시켜버리면 오브젝트 매니저를 기준으로 생성된다.

            ChaseWeapon.localPosition = Vector3.zero;
            ChaseWeapon.localRotation = Quaternion.identity;
            // 무기의 회전값, 위치, 

            Vector3 RotVec = Vector3.forward * 360 * i / Count;
            // 360도라는 각도를 가지고 [몇번째 것인지] / 돌아가고 있는 무기의 총갯수 로 z축으로 얼마만큼 값을 줄지 결정해준다.
            ChaseWeapon.Rotate(RotVec);
            ChaseWeapon.Translate(ChaseWeapon.up * 1.5f, Space.World); // 체이스 웨폰의 위쪽(즉 플레이어의 위치로부터 윗 방향으로 1.5만큼 거리에 배치), 월드 좌표

            ChaseWeapon.GetComponent<Bullet>().Init(Damage, -1); // 근접 무기는 그냥 계속 관통하는 것이 기본 설정이기 때문에 -1값을 줌(굳이 -1이 아니어도 되기는 함)
        }
    }

    void RotateSet()
    {
        switch (ID)
        {
            case 0:
                transform.Rotate(Vector3.back * Speed * Time.deltaTime);
                // Vector3.back * 양수 or Vector3.forward * 음수
                break;

            default:

                break;
        }
    }
}
