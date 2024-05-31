using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hands : MonoBehaviour
{
    public bool IsLeft;
    public SpriteRenderer sr;

    SpriteRenderer Player;

    Vector3 RightHandPos = new Vector3(0.35f, -0.15f, 0);
    Vector3 ReverseRightHandPos = new Vector3(-0.15f, -0.15f, 0);

    Quaternion LeftRot = Quaternion.Euler(0, 0, -35);
    Quaternion ReverseLeftRot = Quaternion.Euler(0, 0, -135);

    private void Awake()
    {
        Player = GetComponentsInParent<SpriteRenderer>()[1];
    }

    private void LateUpdate()
    {
        bool IsReverse = Player.flipX;

        if(IsLeft) // 근접무기 손
        {
            transform.localRotation = IsReverse ? ReverseLeftRot : LeftRot;
            sr.flipY = IsReverse;
            sr.sortingOrder = IsReverse ? 4 : 6;
        }

        else // 원거리 무기
        {
            transform.localPosition = IsReverse ? ReverseRightHandPos : RightHandPos;
            sr.flipX = IsReverse;
            sr.sortingOrder = IsReverse ? 6 : 4;
        }
    }
}
