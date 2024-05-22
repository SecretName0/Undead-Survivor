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
    public int Count; // ����
    public float Speed; // ȸ�� �ӵ�

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
                Speed = 150; // ��Ż ���� ���̳ʽ����� �ð� �������� ȸ���� (Rotate ����)

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

            ChaseWeapon.GetComponent<Bullet>().Init(Damage, -1); // ���� ����� �׳� ��� �����ϴ� ���� �⺻ �����̱� ������ -1���� ��(���� -1�� �ƴϾ �Ǳ�� ��)
        }
    }

    void RotateSet()
    {
        switch (ID)
        {
            case 0:
                transform.Rotate(Vector3.back * Speed * Time.deltaTime);
                // Vector3.back * ��� or Vector3.forward * ����
                break;

            default:

                break;
        }
    }
}
