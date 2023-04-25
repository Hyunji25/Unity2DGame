using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossController : MonoBehaviour
{
    const int STATE_WALK = 1;
    const int STATE_ATTACK = 2;

    private GameObject Target;

    private Animator Anim;

    // 플레이어의 SpriteRenderer 구성요소를 받아오기 위해...
    private SpriteRenderer Brenderer;

    private Vector3 Movement;
    private Vector3 EndPoint;

    private float CoolDown;
    private float Speed;
    public float HP;

    private bool Attack;
    private bool Walk;
    private bool active;

    private int choice;

    private float curTime;
    private float coolTime = 0.5f;

    public Transform pos;
    public Vector2 boxSize;

    private bool onThrow;

    // ** 복제할 총알 원본
    private GameObject EBulletPrefab;

    // ** 복제할 FX 원본
    private GameObject EfxPrefab;

    // ** 복제된 총알의 저장공간.
    private List<GameObject> EnemyBullets = new List<GameObject>();

    private void Awake()
    {
        Target = GameObject.Find("Player");

        Anim = GetComponent<Animator>();

        Brenderer = GetComponent<SpriteRenderer>();

        // ** [Resources] 폴더에서 사용할 리소스를 들고온다.
        EBulletPrefab = Resources.Load("Prefabs/Enemy/EnemyBullet") as GameObject;
        EfxPrefab = Resources.Load("Prefabs/FX/Hit") as GameObject;
    }

    void Start()
    {
        CoolDown = 1.8f;
        Speed = 1.0f;
        HP = ControllerManager.GetInstance().Boss_HP;

        active = true;

        Attack = false;
        Walk = false;

        onThrow = true;
    }

    void Update()
    {
        if (ControllerManager.GetInstance().Boss_HP <= 0)
        {
            Anim.SetTrigger("Die");
            GetComponent<CapsuleCollider2D>().enabled = false;
            Destroy(gameObject, 1.0f);
            SceneManager.LoadScene("GameClear");
        }

        float result = Target.transform.position.x - transform.position.x;

        if (result < 0.0f)
            Brenderer.flipX = false;
        else if (result > 0.0f)
            Brenderer.flipX = true;

        if (ControllerManager.GetInstance().DirRight)
            transform.position -= new Vector3(1.0f, 0.0f, 0.0f) * Time.deltaTime;

        float Distance = Vector3.Distance(Target.transform.position, transform.position);

        if (onThrow)
        {
            onThrow = false;
            StartCoroutine(OnThrow());
        }

        if (active)
        {
            active = false;
            choice = onController();
        }
        else if (Distance < 1.0f)
        {
            onAttack();
        }
        else
        {
            switch (choice)
            {
                case STATE_WALK:
                    onWalk();
                    break;

                case STATE_ATTACK:
                    onAttack();
                    break;
            }
        }
    }

    IEnumerator OnThrow()
    {
        // ** 총알원본을 본제한다.
        GameObject Obj = Instantiate(EBulletPrefab);

        // ** 복제된 총알의 위치를 현재 플레이어의 위치로 초기화한다.
        Obj.transform.position = transform.position + new Vector3(1.8f, 1.8f, 0.0f);

        // ** 총알의 BullerController 스크립트를 받아온다.
        BulletController Controller = Obj.AddComponent<BulletController>();

        // ** 총알 스크립트내부의 방향 변수를 현재 플레이어의 방향 변수로 설정 한다.
        Controller.Direction = new Vector3(1.0f, 0.0f, 0.0f);

        // ** 총알 스크립트내부의 FX Prefab을 설정한다.
        Controller.fxPrefab = EfxPrefab;

        // ** 총알의 SpriteRenderer를 받아온다.
        SpriteRenderer buleltRenderer = Obj.GetComponent<SpriteRenderer>();

        // ** 모든 설정이 종료되었다면 저장소에 보관한다.
        EnemyBullets.Add(Obj);

        while (CoolDown > 0.0f)
        {
            CoolDown -= Time.deltaTime;
            yield return null;
        }

        CoolDown = 1.8f;
        onThrow = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            ControllerManager.GetInstance().Boss_HP -= ControllerManager.GetInstance().BulletDamage;
            Anim.SetTrigger("Hit");
        }

        if (collision.tag == "Skill")
        {
            ControllerManager.GetInstance().Boss_HP -= ControllerManager.GetInstance().ToBossDamage;
            Anim.SetTrigger("Hit");

            if (collision.gameObject.tag.Equals("Skill"))
            //부딪힌 객체의 태그를 비교해서 적인지 판단합니다.
            {
                //적을 파괴합니다.
                Destroy(collision.gameObject);
            }
        }
    }

    private int onController()
    {
        // 초기화
        if (Walk)
        {
            Movement = new Vector3(0.0f, 0.0f, 0.0f);
            Anim.SetFloat("Speed", Movement.x);
            Walk = false;
        }

        if (Attack)
        {
            Attack = false;
        }

        // 어디로 움직일지 정하는 시점에 플레이어의 위치를 도착지점으로 셋팅
        EndPoint = Target.transform.position;

        // * [return]
        // * 1 : 이동           STATE_WALK
        // * 2 : 공격          STATE_ATTACK
        return Random.Range(STATE_WALK, STATE_ATTACK + 1);
    }

    private IEnumerator onCooldown()
    {
        float fTime = CoolDown;

        while (fTime > 0.0f)
        {
            fTime -= Time.deltaTime;
            yield return null;
        }
    }

    private void onAttack()
    {
        Attack = true;
        if (curTime <= 0)
        {
            Collider2D[] colliders = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
            foreach (Collider2D collider in colliders)
            {
                if (collider.tag == "Player")
                {
                    collider.GetComponent<PlayerController>().TakeDamage(ControllerManager.GetInstance().BossDamage);
                }
            }

            Anim.SetTrigger("Attack");
            curTime = coolTime;
        }
        else
        {
            curTime -= Time.deltaTime;
        }

        active = true;
    }

    private void onWalk()
    {
        Walk = true;

        // 목적지에 도착할 때까지......
        float Distance = Vector3.Distance(EndPoint, transform.position);

        if (Distance > 0.5f)
        {
            Vector3 Direction = (EndPoint - transform.position).normalized;

            Movement = new Vector3(
                Speed * Direction.x,
                Speed * Direction.y,
                0.0f);

            transform.position += Movement * Time.deltaTime;
            Anim.SetFloat("Speed", Mathf.Abs(Movement.x));
        }
        else
            active = true;
    }
}
