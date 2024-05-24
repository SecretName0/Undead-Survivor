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
    // 스위치문으로 제어하며 각 ui마다 이 스크립트를 가지기 때문에 따로 이름을 더 많이 지정해줄 필요는 없다.

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
                // Format([필요한 형태 "LV. {인자 이름: F0..." <<- 소숫점 자릿수 지정 F1이면 첫째자리 까지 //, 데이터}
                break;

            case InfoType.Kill:
                MyText.text = string.Format("x{0:F0}", GameManager.Instance.KillCount);
                break;

            case InfoType.Time:
                float RemainTime = GameManager.Instance.MaxGameTime - GameManager.Instance.GameTime;
                int Min = Mathf.FloorToInt(RemainTime / 60);
                int Sec = Mathf.FloorToInt(RemainTime % 60);

                MyText.text = string.Format("{0:D2}:{1:D2}", Min, Sec);
                // {0:D2} <- 자릿수 고정 D1이면 1자리만 나오고 D2면 2자리만 나오는 방식
                break;

            case InfoType.HP:
                float CurHp = GameManager.Instance.HP;
                float MaxHP = GameManager.Instance.Max_HP;

                MySlider.value = CurHp / MaxHP;
                break;
        }
    }
}
