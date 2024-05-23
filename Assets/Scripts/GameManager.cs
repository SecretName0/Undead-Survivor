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
