using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPBar : MonoBehaviour
{
    public GameObject Target;
    private Vector3 offset;
    private Slider BHPBar;

    private void Awake()
    {
        BHPBar = GetComponent<Slider>();
    }

    void Start()
    {
        offset = new Vector3(0.0f, 4.2f, 0.0f);
        BHPBar.maxValue = ControllerManager.GetInstance().Boss_HP;
        BHPBar.value = BHPBar.maxValue;
    }

    void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(Target.transform.position + offset);

        if (BHPBar != null)
        {
            BHPBar.value = ControllerManager.GetInstance().Boss_HP;
            if (BHPBar.value <= 0)
            {
                //Destroy(gameObject);
            }
        }
    }
}
