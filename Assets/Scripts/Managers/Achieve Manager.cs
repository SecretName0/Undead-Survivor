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
        // Achieves = (����� �� ��ȯ) Enum.GetValue(���� Ÿ��?(Achieve))
        // Enum.GetValue << �ڷ��� ���� �׳� �迭�� ��ȯ�ϱ� ������ ����ȯ �ʼ�

        WaitTime = new WaitForSecondsRealtime(4);

        if (!PlayerPrefs.HasKey("SaveData"))
            Init();
    }

    void Init()
    {
        // PlayerPrefs: ������ ���� ����� �����ϴ� Ŭ����
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

            LockCharacters[i].SetActive(!IsUnlock); // ���â ��Ȱ��ȭ
            UnLockCharacters[i].SetActive(IsUnlock); // �ر�
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
