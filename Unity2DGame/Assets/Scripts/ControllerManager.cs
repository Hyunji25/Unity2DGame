using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static BulletPattern;

public class ControllerManager
{
    private static ControllerManager Instance = null;

    public static ControllerManager GetInstance()
    {
        if (Instance == null)
            Instance = new ControllerManager();
        return Instance;
    }

    public bool DirLeft;
    public bool DirRight;

    public float PlayerSpeed = 5.0f;

    // ½ºÅ³
    public float BulletSpeed = 10.0f;
    public int BulletDamage = 1;

    public int EnemyDamage = 3;
    public int BossDamage = 5;
    public int BossThrow = 2;

    public int Player_HP = 10;
    public int Boss_HP = 30;

    public int ToBossDamage = 10;

    public double CountTime = 0;

    public string pk = "";
}