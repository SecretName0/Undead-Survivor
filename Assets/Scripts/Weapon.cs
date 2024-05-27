using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int ID;
    public int PrefabID;
    public float Damage;
    public int Count; // ����, �����
    public float Speed; // ȸ�� �ӵ�, ���� �ӵ�

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
            // ������Ʈ �Ŵ����� ������ �迭���� ���� ������ �������� �߻�ü ������ ������ �׸��� �߰ߵǸ� ������ ���̵� �ش� �ε����� �����ϰ� Ż��
        }

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

        // Hands Set
        Hands Hand = Player.Hand[(int)data.ItemType];
        Hand.sr.sprite = data.Hand;
        Hand.gameObject.SetActive(true);

        Player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver); // SendMessageOptions.DontRequireReceiver: ������ �� �׸��� ������ �־�� �ϴ� ���� �ƴϴ�.
        // �ش� ���ÿ����� ũ�� �ʿ��� ���� �ƴϳ�, ���� �ɷ��� ��ȭ�����ִ� �� �����Ѵٸ�, �׶����� ���Ӱ� ���ö��� ����� �Լ��� ����
        // ��� �͵鿡�� �޼����� �����Ͽ� �̸� �����ϵ��� �� �ʿ䰡 �ִ�.
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
