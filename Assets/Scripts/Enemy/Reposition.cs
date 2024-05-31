using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    Vector3 PlayerPos; // 플레이어 위치
    Vector3 MyPos; // 이 스크립트를 가진 객체의 위치
    Vector3 PlayerDir; // 플레이어가 나아가는 방향

    Collider2D col;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area")) // 벗어난 게 플레이어의 에리어가 아닌 경우 그냥 반환
            return;

        PlayerPos = GameManager.Instance.Player.transform.position;
        MyPos = transform.position;

        float DifX = Mathf.Abs(PlayerPos.x - MyPos.x); // 플레이어와의 x축 거리차
        float DifY = Mathf.Abs(PlayerPos.y - MyPos.y); // 플레이어와의 y축 거리차

        PlayerDir = GameManager.Instance.Player.InputVector;
        float DirX = PlayerDir.x < 0 ? -1 : 1;
        float DirY = PlayerDir.y < 0 ? -1 : 1;

        switch (transform.tag)
        {
            case "Ground":
                if(DifX > DifY) // 플레이어가 X축으로 멀어지는 경우
                {
                    transform.Translate(Vector3.right * DirX * 40); // 타일맵이 4개가 있고 축당 길이가 개당 20으로 2x2사이즈라 40만큼
                }

                if (DifX < DifY) // 플레이어가 Y축으로 멀어지는 경우
                {
                    transform.Translate(Vector3.up * DirY * 40); // 타일맵이 4개가 있고 축당 길이가 개당 20으로 2x2사이즈라 40만큼
                }

                break;

            case "Enemy":
                if(col.enabled)
                {
                    transform.Translate(PlayerDir * 20 + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0));
                }
                break;
        }
    }
}
