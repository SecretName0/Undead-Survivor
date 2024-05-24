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
        // Camera.main으로 현재 사용중인 메인 카메라에 바로 접근됨.
        // WorldToScreenPoint: 월드 상의 오브젝트 위치를 스크린 좌표로 변환
    }
}
