using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchieveManager : MonoBehaviour
{
    public GameObject[] LockCharacters;
    public GameObject[] UnLockCharacters;

    public GameObject UINotice;

    WaitForSecondsRealtime WaitTime;

    enum Achieve { UnlockChara2, UnlockChara3 }
    Achieve[] Achieves;

    private void Awake()
    {
        Achieves = (Achieve[])Enum.GetValues(typeof(Achieve));
        // Achieves = (명시적 형 변환) Enum.GetValue(무슨 타입?(Achieve))
        // Enum.GetValue << 자료형 없이 그냥 배열로 반환하기 때문에 형변환 필수

        WaitTime = new WaitForSecondsRealtime(4);

        if (!PlayerPrefs.HasKey("SaveData"))
            Init();
    }

    void Init()
    {
        // PlayerPrefs: 간단한 저장 기능을 제공하는 클래스
        PlayerPrefs.SetInt("SaveData", 1);

        foreach (Achieve achieve in Achieves)
        {
            PlayerPrefs.SetInt(achieve.ToString(), 0);
        }
    }

    private void Start()
    {
        UnlockChara();
    }

    void UnlockChara()
    {
        for(int i = 0; i < LockCharacters.Length; i++)
        {
            string AchieveName = Achieves[i].ToString();
            bool IsUnlock = PlayerPrefs.GetInt(AchieveName) == 1;

            LockCharacters[i].SetActive(!IsUnlock); // 잠금창 비활성화
            UnLockCharacters[i].SetActive(IsUnlock); // 해금
        }
    }

    private void LateUpdate()
    {
        foreach(Achieve achieve in Achieves)
        {
            CheckAchieve(achieve);
        }
    }

    void CheckAchieve(Achieve achieve)
    {
        bool isAchieve = false;

        switch(achieve)
        {
            case Achieve.UnlockChara2:
                isAchieve = GameManager.Instance.KillCount >= 10;
                break;

            case Achieve.UnlockChara3:
                isAchieve = GameManager.Instance.GameTime == GameManager.Instance.MaxGameTime;
                break;
        }

        if(isAchieve && PlayerPrefs.GetInt(achieve.ToString()) == 0)
        {
            PlayerPrefs.SetInt(achieve.ToString(), 1);

            for(int i = 0; i < UINotice.transform.childCount; i++)
            {
                bool IsActive = i == (int)achieve;

                UINotice.transform.GetChild(i).gameObject.SetActive(IsActive);
            }

            StartCoroutine(NoticeRoutine());
        }
    }

    IEnumerator NoticeRoutine()
    {
        UINotice.SetActive(true);

        yield return WaitTime;

        UINotice.SetActive(false);
    }
}
