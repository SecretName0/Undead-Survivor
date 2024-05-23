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
    public int Count; // ����, �����
    public float Speed; // ȸ�� �ӵ�, ���� �ӵ�

    float Timer;

    PlayerController Player;

    private void Awake()
    {
        Instance = this;

        Player = GetComponentInParent<PlayerController>();
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        AttackSet();
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
                Speed = 150; // ��Ż ���� ���̳ʽ����� �ð� �������� ȸ���� (Rotate ����)

                WeaponPlaced();
                break;

            default:
                Speed = 0.3f;
                break;
        }
    }

    void WeaponPlaced()
    {
        for (int i = 0; i < Count; i++)
        {
            Transform ChaseWeapon;

            if(i < transform.childCount) // ���� �ε����� childcount�� ���� ����� 
            {
                ChaseWeapon = transform.GetChild(i); // �������� �ҼӵǾ��ִ� �༮���� ������ �״�� ���
            }
            
            else // �� ���ָ� ����ٸ�?
            {
                ChaseWeapon = GameManager.Instance.om.GetSpawnTarget(PrefabID).transform; // ���Ӱ� ����
            }

            ChaseWeapon.parent = transform; // �÷��̾ �Ѿƴٴ� ���Ⱑ �÷��̾ ����� �Ѿƴٴ� �� �ְ� ����� ���� ������ �� �� �θ� �ű�
            // ������ ���ѹ����� ������Ʈ �Ŵ����� �������� �����ȴ�.

            ChaseWeapon.localPosition = Vector3.zero;
            ChaseWeapon.localRotation = Quaternion.identity;
            // ������ ȸ����, ��ġ, 

            Vector3 RotVec = Vector3.forward * 360 * i / Count;
            // 360����� ������ ������ [���° ������] / ���ư��� �ִ� ������ �Ѱ��� �� z������ �󸶸�ŭ ���� ���� �������ش�.
            ChaseWeapon.Rotate(RotVec);
            ChaseWeapon.Translate(ChaseWeapon.up * 1.5f, Space.World); // ü�̽� ������ ����(�� �÷��̾��� ��ġ�κ��� �� �������� 1.5��ŭ �Ÿ��� ��ġ), ���� ��ǥ

            ChaseWeapon.GetComponent<Bullet>().Init(Damage, -1, Vector3.zero); // ���� ����� �׳� ��� �����ϴ� ���� �⺻ �����̱� ������ -1���� ��(���� -1�� �ƴϾ �Ǳ�� ��)
        }
    }

    void AttackSet()
    {
        switch (ID)
        {
            case 0: // �������� ȸ�� ����
                transform.Rotate(Vector3.back * Speed * Time.deltaTime);
                // Vector3.back * ��� or Vector3.forward * ����
                break;

            default: // ���Ÿ� ���� �߻� ����
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
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, Dir); // ������ ���� �߽����� ��ǥ�� ���� ��ü�� ȸ����Ű�� �Լ�
        // Vector3.up ��, z���� �������� Dir������ ���ϵ��� ȸ��

        bullet.GetComponent<Bullet>().Init(Damage, Count, Dir);
    }
}
