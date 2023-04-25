using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP_Test2 : MonoBehaviour
{
    private Image image;
    private int MaxHP = 1500;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            int damage = Random.Range(50, 150);
            image.fillAmount -= (damage * 100 / MaxHP) * 0.01f;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            image.fillAmount += 1;
        }

        if (image.fillAmount == 0)
        {
            Destroy(GameObject.Find("HPCanvas"));
        }
    }
}
