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

    // Game Time
    [Header("Game Timer")]
    public float GameTime;
    public float MaxGameTime;

    [SerializeField] bool GameEnd;

    // Game Rule Board
    [Header("Game Rule Board")]
    public int NowLevel;
    public int KillCount;
    public int Exp;
    public int[] NextExp = { 10, 30, 60, 100, 150, 210, 280, 360, 450, 600 };

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
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

        if (Exp >= NextExp[NowLevel])
        {
            NowLevel++;

            Exp = 0;
        }
    }
}
