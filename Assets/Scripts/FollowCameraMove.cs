using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCameraMove : MonoBehaviour
{
    RectTransform rt;

    private void Awake()
    {
        rt = GetComponent<RectTransform>();
    }

    private void FixedUpdate()
    {
        rt.position = Camera.main.WorldToScreenPoint(GameManager.Instance.Player.transform.position);
        // Camera.main���� ���� ������� ���� ī�޶� �ٷ� ���ٵ�.
        // WorldToScreenPoint: ���� ���� ������Ʈ ��ġ�� ��ũ�� ��ǥ�� ��ȯ
    }
}
