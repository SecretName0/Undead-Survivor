using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public ObjectManager om;
    public PlayerController Player;

    // Game Time
    public float GameTime;
    public float MaxGameTime;

    [SerializeField] bool GameEnd;

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
}
