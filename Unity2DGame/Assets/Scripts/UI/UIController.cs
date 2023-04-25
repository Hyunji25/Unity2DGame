using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject SkillCanvas;
    public bool SkillCanvasActive;

    private void Awake()
    {
        SkillCanvas = GameObject.Find("SkillCanvas");
    }

    void Start()
    {
        SkillCanvasActive = true;
        SkillCanvas.SetActive(SkillCanvasActive);
    }

    public void onSkillCanvasActive()
    {
        SkillCanvasActive = !SkillCanvasActive;
        SkillCanvas.SetActive(SkillCanvasActive);
    }
}
