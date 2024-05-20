using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    Vector3 PlayerPos; // �÷��̾� ��ġ
    Vector3 MyPos; // �� ��ũ��Ʈ�� ���� ��ü�� ��ġ
    Vector3 PlayerDir; // �÷��̾ ���ư��� ����

    Collider2D col;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area")) // ��� �� �÷��̾��� ����� �ƴ� ��� �׳� ��ȯ
            return;

        PlayerPos = GameManager.Instance.Player.transform.position;
        MyPos = transform.position;

        float DifX = Mathf.Abs(PlayerPos.x - MyPos.x); // �÷��̾���� x�� �Ÿ���
        float DifY = Mathf.Abs(PlayerPos.y - MyPos.y); // �÷��̾���� y�� �Ÿ���

        PlayerDir = GameManager.Instance.Player.InputVector;
        float DirX = PlayerDir.x < 0 ? -1 : 1;
        float DirY = PlayerDir.y < 0 ? -1 : 1;

        switch (transform.tag)
        {
            case "Ground":
                if(DifX > DifY) // �÷��̾ X������ �־����� ���
                {
                    transform.Translate(Vector3.right * DirX * 40); // Ÿ�ϸ��� 4���� �ְ� ��� ���̰� ���� 20���� 2x2������� 40��ŭ
                }

                if (DifX < DifY) // �÷��̾ Y������ �־����� ���
                {
                    transform.Translate(Vector3.up * DirY * 40); // Ÿ�ϸ��� 4���� �ְ� ��� ���̰� ���� 20���� 2x2������� 40��ŭ
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
