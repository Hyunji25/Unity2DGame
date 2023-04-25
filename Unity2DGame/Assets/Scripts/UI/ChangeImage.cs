using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeImage : MonoBehaviour
{
    public Image TestImage;
    public Sprite TestSprite1;
    public Sprite TestSprite2;
    private bool check = false;

    public void ChangeImg()
    {
        if (check)
        {
            check = !check;
            TestImage.sprite = TestSprite1;
        }
        else
        {
            check = !check;
            TestImage.sprite = TestSprite2;
        }
    }
}
