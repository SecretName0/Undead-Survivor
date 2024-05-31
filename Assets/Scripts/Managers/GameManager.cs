using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Game Time
    [Header("Game Timer")]
    public float GameTime;
    public float MaxGameTime;

    public bool TimeLive;

    [SerializeField] bool GameEnd;

    // Game Rule Board
    [Header("Game Rule Board")]
    public int HP;
    public int Max_HP = 100;
    public int NowLevel;
    public int KillCount;
    public int Exp;
    public int[] NextExp = { 10, 30, 60, 100, 150, 210, 280, 360, 450, 600 };

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        HP = Max_HP;

        TimeLive = true;

        // �ӽ� ��ũ��Ʈ
        LevelUI.Select(0);
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
        }
    }

    public void GetExp()
    {
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
