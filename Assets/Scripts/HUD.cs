using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType { Exp, Level, Kill, Time, HP }
    public InfoType DataType;

    Text MyText;
    Slider MySlider;
    // ����ġ������ �����ϸ� �� ui���� �� ��ũ��Ʈ�� ������ ������ ���� �̸��� �� ���� �������� �ʿ�� ����.

    private void Awake()
    {
        MyText = GetComponent<Text>();
        MySlider = GetComponent<Slider>();
    }

    private void LateUpdate()
    {
        switch(DataType)
        {
            case InfoType.Exp:
                float CurExp = GameManager.Instance.Exp;
                float MaxExp = GameManager.Instance.NextExp[GameManager.Instance.NowLevel];

                MySlider.value = CurExp / MaxExp;
                break;

            case InfoType.Level:
                MyText.text = string.Format("LV.{0:F0}", GameManager.Instance.NowLevel);
                // Format([�ʿ��� ���� "LV. {���� �̸�: F0..." <<- �Ҽ��� �ڸ��� ���� F1�̸� ù°�ڸ� ���� //, ������}
                break;

            case InfoType.Kill:
                MyText.text = string.Format("x{0:F0}", GameManager.Instance.KillCount);
                break;

            case InfoType.Time:
                float RemainTime = GameManager.Instance.MaxGameTime - GameManager.Instance.GameTime;
                int Min = Mathf.FloorToInt(RemainTime / 60);
                int Sec = Mathf.FloorToInt(RemainTime % 60);

                MyText.text = string.Format("{0:D2}:{1:D2}", Min, Sec);
                // {0:D2} <- �ڸ��� ���� D1�̸� 1�ڸ��� ������ D2�� 2�ڸ��� ������ ���
                break;

            case InfoType.HP:
                float CurHp = GameManager.Instance.HP;
                float MaxHP = GameManager.Instance.Max_HP;

                MySlider.value = CurHp / MaxHP;
                break;
        }
    }
}
