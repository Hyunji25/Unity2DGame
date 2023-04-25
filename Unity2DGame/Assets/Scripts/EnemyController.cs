using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject Target;

    public float Speed;
    public float HP;
    private Animator Anim;
    private Vector3 Movement;

    private bool Attack;

    private float curTime;
    private float coolTime = 1.0f;

    public Transform pos;
    public Vector2 boxSize;

    private static EnemyController Instance = null;

    public static EnemyController GetInstance()
    {
        if (Instance == null)
            Instance = new EnemyController();
        return Instance;
    }

    private void Awake()
    {
        Target = GameObject.Find("Player");

        Anim = GetComponent<Animator>();
    }

    void Start()
    {
        Speed = 0.2f;
        Movement = new Vector3(1.0f, 0.0f, 0.0f);
        HP = 3;

        Attack = false;
    }

    void Update()
    {
        Movement = ControllerManager.GetInstance().DirRight ?
            new Vector3(Speed + 1.0f, 0.0f, 0.0f) : new Vector3(Speed, 0.0f, 0.0f);

        float? Distance = null;

        if (Target != null)
        {
            Distance = Vector3.Distance(Target.transform.position, transform.position);
        }
        
        try
        {
            if (Distance < 3.0f)
            {
                if (curTime <= 0)
                {
                    Attack = true;

                    Collider2D[] colliders = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
                    foreach (Collider2D collider in colliders)
                    {
                        if (collider.tag == "Player")
                        {
                            collider.GetComponent<PlayerController>().TakeDamage(ControllerManager.GetInstance().EnemyDamage);
                        }
                    }

                    Anim.SetTrigger("Attack");
                    curTime = coolTime;
                }
                else
                {
                    curTime -= Time.deltaTime;
                }
            }
            else
            {
                Attack = false;
            }

            if (Distance >= 25.0f)
            {
                Destroy(gameObject, 0.016f);
            }
        }
        catch (NullReferenceException ex)
        {
            Debug.Log(ex);
        }

        if (!Attack)
        {
            Anim.SetFloat("Speed", Movement.x);
            transform.position -= Movement * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            HP -= ControllerManager.GetInstance().BulletDamage;

            if (HP <= 0)
            {
                Anim.SetTrigger("Die");
                GetComponent<CapsuleCollider2D>().enabled = false;
            }
        }
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject, 0.016f);
    }
}