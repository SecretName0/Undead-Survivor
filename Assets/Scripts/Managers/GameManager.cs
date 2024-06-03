using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // [Header("Game Control")]
    // Header: �ν������� �Ӽ����� ���н����� �� �ִ� Ÿ��Ʋ (���� �����ݷ��� �ʿ� ����)
    // �𸮾��� Category = "asdf"; �� ���� ����� ��

    [Header("Game Managers")]
    public static GameManager Instance;
    public ObjectManager om;
    public PlayerController Player;
    public LevelSystemUI LevelUI;
    public Result UIResult;

    public GameObject EnemyCleaner;

    [Header("Player Data")]
    public int PlayerID;

    // Game Time
    [Header("Game Timer")]
    public float GameTime;
    public float MaxGameTime;

    public bool TimeLive;

    [SerializeField] bool GameEnd;

    // Game Rule Board
    [Header("Game Rule Board")]
    public float HP;
    public float Max_HP = 100;
    public int NowLevel;
    public int KillCount;
    public int Exp;
    public int[] NextExp = { 10, 30, 60, 100, 150, 210, 280, 360, 450, 600 };

    private void Awake()
    {
        Instance = this;
    }

    public void GameStart(int id)
    {
        PlayerID = id;

        HP = Max_HP;

        Player.gameObject.SetActive(true);

        TimeResume();
        // �ӽ� ��ũ��Ʈ
        LevelUI.Select(id % 2);
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        TimeLive = false;

        yield return new WaitForSeconds(1);

        UIResult.gameObject.SetActive(true);
        UIResult.Lose();
        TimeStop();
    }

    public void GameWin()
    {
        StartCoroutine(GameWinRoutine());
    }

    IEnumerator GameWinRoutine()
    {
        TimeLive = false;
        EnemyCleaner.SetActive(true);

        yield return new WaitForSeconds(1);

        UIResult.gameObject.SetActive(true);
        UIResult.Win();
        TimeStop();
    }

    public void GoTitle()
    {
        SceneManager.LoadScene(0);
    }

    private void Update()
    {
        if (!TimeLive)
            return;

        if(!GameEnd)
            GameTime += Time.deltaTime;

        if(GameTime > MaxGameTime && !GameEnd)
        {
            GameTime = MaxGameTime;
            GameEnd = true;
            GameWin();
        }
    }

    public void GetExp()
    {
        if (!TimeLive)
            return;

        Exp++;

        if (Exp >= NextExp[Mathf.Min(NowLevel, NextExp.Length -1)])
        {
            // ����ġ�� ���� ����ġ �迭�� �� �̻��� ���
            // ���� ����ġ�� ����: �� �� ���� ���� ��ȯ [1. ���� ������ �������� �ε���] [2. ���� ����ġ �迭�� ���̺��� �ϳ� ���� ��(������ ��� ������ �ʰ��ϸ� ���� �� ����)]
            NowLevel++;
            Exp = 0;
            LevelUI.ShowUI();
        }
    }

    public void TimeStop()
    {
        TimeLive = false;

        Time.timeScale = 0; // timeScale: ����Ƽ�� �ð� �ӵ� (����) [�⺻ ���� 1]
    }
    
    public void TimeResume()
    {
        TimeLive = true;

        Time.timeScale = 1;
    }
}
