using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    public List<GameObject> Images = new List<GameObject>();
    public List<GameObject> Buttons = new List<GameObject>();
    public List<Image> ButtonsImages = new List<Image>();

    private float cooldown, cooltime;
    private float cooldown1, cooltime1;
    private float cooldown2, cooltime2;
    private float cooldown3, cooltime3;

    private int Index;

    private bool OnOff = false;

    private BulletPattern bulletPattern;

    private GameObject skill;

    private void Awake()
    {
        skill = Resources.Load("Prefabs/PatternBullet") as GameObject;
    }

    private void Start()
    {
        GameObject SkillsObj = GameObject.Find("Skills");

        for (int i = 0; i < SkillsObj.transform.childCount; ++i)
            Images.Add(SkillsObj.transform.GetChild(i).gameObject);

        for (int i = 0; i < Images.Count; ++i)
            Buttons.Add(Images[i].transform.GetChild(0).gameObject);

        for (int i = 0; i < Buttons.Count; ++i)
            ButtonsImages.Add(Buttons[i].GetComponent<Image>());

        cooldown = 0.0f;
        cooltime = 0.0f;
        cooldown1 = 0.0f;
        cooltime1 = 0.0f;
        cooldown2 = 0.0f;
        cooltime2 = 0.0f;
        cooldown3 = 0.0f;
        cooltime3 = 0.0f;
    }

    public void PushButton()
    {
        ButtonsImages[Index].fillAmount = 1;
        Buttons[Index].GetComponent<Button>().enabled = false;
        
        switch(Index)
        {
            case 0 :
                StartCoroutine(Testcase_Coroutine());
                break;
            case 1:
                StartCoroutine(Testcase1_Coroutine());
                break;
            case 2:
                StartCoroutine(Testcase2_Coroutine());
                break;
            case 3:
                StartCoroutine(Testcase3_Coroutine());
                break;
        }
    }

    IEnumerator Testcase_Coroutine()
    {
        float coold = cooldown;
        float coolt = cooltime;
        int i = Index;
        if (OnOff)
        {
            while (ButtonsImages[i].fillAmount != 0)
            {
                ButtonsImages[i].fillAmount -= Time.deltaTime * cooldown;
                yield return null;
            }

            ControllerManager.GetInstance().BulletSpeed -= 1.0f;
            ControllerManager.GetInstance().BulletDamage -= 2;

            while (ButtonsImages[i].fillAmount != 1)
            {
                ButtonsImages[i].fillAmount += Time.deltaTime * cooltime;
                yield return null;
            }

            Buttons[i].GetComponent<Button>().enabled = true;
            OnOff = false;
        }
    }

    // cooldown이 클수록 쿨타임이 줄어듦
    public void Testcase() // 플레이어 공격력 상승
    {
        Index = 0;

        cooldown = 0.3f;
        cooltime = 0.3f;

        ControllerManager.GetInstance().BulletSpeed += 1.0f;
        ControllerManager.GetInstance().BulletDamage += 2;
        OnOff = true;
    }

    IEnumerator Testcase1_Coroutine()
    {
        float coold = cooldown1;
        float coolt = cooltime1;
        int i = Index;
        if (OnOff)
        {
            while (ButtonsImages[i].fillAmount != 0)
            {
                ButtonsImages[i].fillAmount -= Time.deltaTime * cooldown1;
                yield return null;
            }

            ControllerManager.GetInstance().EnemyDamage += 1;
            ControllerManager.GetInstance().BossDamage += 2;
            ControllerManager.GetInstance().BossThrow += 1;

            while (ButtonsImages[i].fillAmount != 1)
            {
                ButtonsImages[i].fillAmount += Time.deltaTime * cooltime1;
                yield return null;
            }

            Buttons[i].GetComponent<Button>().enabled = true;
            OnOff = false;
        }
    }

    public void Testcase1() // 적과 보스 공격력 감소
    {
        Index = 1;

        cooldown1 = 0.3f; 
        cooltime1 = 0.3f;

        ControllerManager.GetInstance().EnemyDamage -= 1;
        ControllerManager.GetInstance().BossDamage -= 2;
        ControllerManager.GetInstance().BossThrow -= 1;
        OnOff = true;
    }

    IEnumerator Testcase2_Coroutine()
    {
        float coold = cooldown2;
        float coolt = cooltime2;
        int i = Index;
        if (OnOff)
        {
            while (ButtonsImages[i].fillAmount != 0)
            {
                ButtonsImages[i].fillAmount -= Time.deltaTime * cooldown2;
                yield return null;
            }

            while (ButtonsImages[i].fillAmount != 1)
            {
                ButtonsImages[i].fillAmount += Time.deltaTime * cooltime2;
                yield return null;
            }

            Buttons[i].GetComponent<Button>().enabled = true;
            OnOff = false;
        }
    }

    public void Testcase2() // 플레이어 체력 회복
    {
        Index = 2;

        cooldown2 = 0.1f;
        cooltime2 = 0.3f;

        ControllerManager.GetInstance().Player_HP += 10;
        OnOff = true;
    }

    IEnumerator Testcase3_Coroutine()
    {
        float coold = cooldown3;
        float coolt = cooltime3;
        int i = Index;

        if (OnOff)
        {
            while (ButtonsImages[i].fillAmount != 0)
            {
                ButtonsImages[i].fillAmount -= Time.deltaTime * cooldown3;
                yield return null;
            }

            ControllerManager.GetInstance().EnemyDamage = 3;
            ControllerManager.GetInstance().BossDamage = 5;
            ControllerManager.GetInstance().BossThrow = 2;

            while (ButtonsImages[i].fillAmount != 1)
            {
                ButtonsImages[i].fillAmount += Time.deltaTime * cooltime3;
                yield return null;
            }

            Buttons[i].GetComponent<Button>().enabled = true;
            OnOff = false;
        }
    }

    public void Testcase3() // 무적, 적과 보스 공격력 0
    {
        Index = 3;

        cooldown3 = 1.0f;
        cooltime3 = 0.3f;

        ControllerManager.GetInstance().EnemyDamage = 0;
        ControllerManager.GetInstance().BossDamage = 0;
        ControllerManager.GetInstance().BossThrow = 0;
        OnOff = true;
    }
}

// 클수록 빨리 돌아가고 작을수록 느리게 돌아감