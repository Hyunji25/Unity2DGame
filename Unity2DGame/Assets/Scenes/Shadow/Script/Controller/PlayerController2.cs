using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    private GameObject Parent = null;
    private string EnemyName = "Enemy2";

    private void Awake()
    {
        Parent = new GameObject("ObjectList");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            Instantiate(PrefabManager.GetInstence.getPrefabByName(EnemyName)).transform.SetParent(Parent.transform);
        }
    }
}
