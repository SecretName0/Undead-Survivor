using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int ID;
    public int PrefabID;
    public float Damage;
    public int Count; // 갯수, 관통력
    public float Speed; // 회전 속도, 연사 속도

    float Timer;

    PlayerController Player;

    private void Awake()
    {
        //Player = GetComponentInParent<PlayerController>();
        Player = GameManager.Instance.Player;
    }

    private void Start()
    {

    }

    private void Update()
    {
        AttackSet();
    }

    public void LevelUp(float Damage, int Count)
    {
        this.Damage = Damage;
        this.Count = Count;

        if (ID == 0)
        {
            WeaponPlaced();
        }

        Player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    public void Init(ItemData data)
    {
        // Basic Set
        name = "Weapon " + data.ItemID;
        transform.parent = Player.transform;
        transform.localPosition = Vector3.zero;

        // Property Set
        ID = data.ItemID;
        Damage = data.BaseDamage;
        Count = data.BasePerCount;

        for(int i = 0; i < GameManager.Instance.om.Prefabs.Length; i++)
        {
            if(data.Projectile == GameManager.Instance.om.Prefabs[i])
            {
                PrefabID = i;

                break;
            }
            // 오브젝트 매니저의 프리팹 배열에서 만약 아이템 데이터의 발사체 변수와 동일한 항목이 발견되면 프리팹 아이디를 해당 인덱스로 지정하고 탈출
        }

        switch(ID)
        {
            case 0:
                Speed = 150; // 토탈 값이 마이너스여야 시계 방향으로 회전함 (Rotate 참조)

                WeaponPlaced();
                break;

            default:
                Speed = 0.3f;
                break;
        }

        // Hands Set
        Hands Hand = Player.Hand[(int)data.ItemType];
        Hand.sr.sprite = data.Hand;
        Hand.gameObject.SetActive(true);

        Player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver); // SendMessageOptions.DontRequireReceiver: 무조건 그 항목을 가지고 있어야 하는 것은 아니다.
        // 해당 예시에서는 크게 필요한 것은 아니나, 만약 능력을 강화시켜주는 기어가 존재한다면, 그때마다 새롭게 어플라이 기어라는 함수를 가진
        // 모든 것들에게 메세지를 전달하여 이를 실행하도록 할 필요가 있다.
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

            ChaseWeapon.GetComponent<Bullet>().Init(Damage, -1, Vector3.zero); // 근접 무기는 그냥 계속 관통하는 것이 기본 설정이기 때문에 -1값을 줌(굳이 -1이 아니어도 되기는 함)
        }
    }

    void AttackSet()
    {
        switch (ID)
        {
            case 0: // 근접무기 회전 설정
                transform.Rotate(Vector3.back * Speed * Time.deltaTime);
                // Vector3.back * 양수 or Vector3.forward * 음수
                break;

            default: // 원거리 무기 발사 설정
                Timer += Time.deltaTime;

                if (Timer > Speed)
                {
                    Fire();

                    Timer = 0;
                }
                break;
        }
    }

    void Fire()
    {
        if (!Player.scanner.NearestTarget)
            return;

        Vector3 TargetPos = Player.scanner.NearestTarget.position;
        Vector3 Dir = TargetPos - transform.position;
        Dir = Dir.normalized;

        Transform bullet = GameManager.Instance.om.GetSpawnTarget(PrefabID).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, Dir); // 지정된 축을 중심으로 목표를 향해 객체를 회전시키는 함수
        // Vector3.up 즉, z축을 기준으로 Dir쪽으로 향하도록 회전

        bullet.GetComponent<Bullet>().Init(Damage, Count, Dir);
    }
}
