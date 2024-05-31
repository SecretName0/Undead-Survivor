using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float ScanRange;
    public LayerMask TargetLayer;
    public RaycastHit2D[] HitResults;
    public Transform NearestTarget;

    private void FixedUpdate()
    {
        HitResults = Physics2D.CircleCastAll(transform.position, ScanRange, Vector2.zero, 0, TargetLayer);
        // ĳ���� ���� ��ġ, ���� ������(��Ŭ ĳ��Ʈ�̱� ����), ĳ������ ����, ĳ������ ��� ����, ��� ���̾�

        NearestTarget = GetNearest();
    }

    Transform GetNearest()
    {
        Transform result = null;
        float diff = 100;

        foreach(RaycastHit2D Target in HitResults)
        {
            Vector3 PlayerPos = transform.position;
            Vector3 TargetPos = Target.transform.position;

            float CurDiff = Vector3.Distance(PlayerPos, TargetPos);

            if(CurDiff < diff)
            {
                diff = CurDiff;

                result = Target.transform;
            }
        }

        return result;
    }
}
