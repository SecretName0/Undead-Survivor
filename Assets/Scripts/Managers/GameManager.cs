using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // [Header("Game Control")]
    // Header: 인스펙터의 속성들을 구분시켜줄 수 있는 타이틀 (따로 세미콜론은 필요 없다)
    // 언리얼의 Category = "asdf"; 와 같은 기능을 함

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

        // 임시 스크립트
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
            // 경험치가 다음 경험치 배열의 값 이상일 경우
            // 다음 경험치의 조건: 둘 중 작은 값을 반환 [1. 현재 레벨을 정보로한 인덱스] [2. 현재 경험치 배열의 길이보다 하나 작은 값(레벨이 허용 범위를 초과하면 터질 수 있음)]
            NowLevel++;
            Exp = 0;
            LevelUI.ShowUI();
        }
    }

    public void TimeStop()
    {
        TimeLive = false;

        Time.timeScale = 0; // timeScale: 유니티의 시간 속도 (배율) [기본 값은 1]
    }
    
    public void TimeResume()
    {
        TimeLive = true;

        Time.timeScale = 1;
    }
}
