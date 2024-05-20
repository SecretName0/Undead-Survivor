using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public ObjectManager om;
    public PlayerController Player;

    private void Awake()
    {
        Instance = this;
    }
}
