using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // ** �����̴� �ӵ�
    private float Speed;

    // ** �������� �����ϴ� ����
    private Vector3 Movement;

    // ** �÷��̾��� Animator ������Ҹ� �޾ƿ�������...
    private Animator animator;

    // ** �÷��̾��� SpriteRenderer ������Ҹ� �޾ƿ�������...
    private SpriteRenderer playerRenderer;

    // ** [����üũ]
    private bool onAttack; // ���ݻ���
    private bool onHit; // �ǰݻ���
    private bool onDeath; // �������

    // ** ������ �Ѿ� ����
    private GameObject BulletPrefab;

    // ** ������ FX ����
    private GameObject fxPrefab;

    // ���� list�� ����
    public GameObject[] stageBack = new GameObject[6];

    // ** ������ �Ѿ��� �������.
    private List<GameObject> Bullets = new List<GameObject>();

    // ** �÷��̾ ���������� �ٶ� ����.
    private float Direction;

    [Header("����")]
    // ** �÷��̾ �ٶ󺸴� ����

    [Tooltip("����")]
    public bool DirLeft;
    [Tooltip("������")]
    public bool DirRight;

    private Animator Anim;

    private void Awake()
    {
        // ** player �� Animator�� �޾ƿ´�.
        animator = this.GetComponent<Animator>();

        // ** player �� SpriteRenderer�� �޾ƿ´�.
        playerRenderer = this.GetComponent<SpriteRenderer>();

        // ** [Resources] �������� ����� ���ҽ��� ���´�.
        BulletPrefab = Resources.Load("Prefabs/Bullet") as GameObject;
        fxPrefab = Resources.Load("Prefabs/FX/Hit") as GameObject;

        Anim = GetComponent<Animator>();
    }

    // ** ����Ƽ �⺻ ���� �Լ�
    // ** �ʱⰪ�� ������ �� ���
    void Start()
    {
        // ** �ӵ��� �ʱ�ȭ.
        Speed = ControllerManager.GetInstance().PlayerSpeed;

        // ** �ʱⰪ ����
        onAttack = true;
        onHit = false;
        onDeath = false;
        Direction = 1.0f;

        DirLeft = false;
        DirRight = false;

        for (int i = 0; i < 6; ++i)
            stageBack[i] = GameObject.Find(i.ToString());
    }

    // ** ����Ƽ �⺻ ���� �Լ�
    // ** �����Ӹ��� �ݺ������� ����Ǵ� �Լ�.
    void Update()
    {
        if (ControllerManager.GetInstance().Player_HP <= 0)
        {
            OnDeath();
            SceneManager.LoadScene("GameOver");
        }

        // ** �����̽��ٸ� �Է��Ѵٸ�..
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // ** ����
            //OnAttack();
            animator.SetTrigger("Attack");

            // ** �Ѿ� ������ �����Ѵ�.
            GameObject Obj = Instantiate(BulletPrefab);

            // ** ������ �Ѿ��� ��ġ�� ���� �÷��̾��� ��ġ�� �ʱ�ȭ�Ѵ�.
            if (Direction < 0)
                Obj.transform.position = transform.position + new Vector3(-2.0f, 1.2f, 0.0f);
            Obj.transform.position = transform.position + new Vector3(2.0f, 1.2f, 0.0f);

            // ** �Ѿ��� BullerController ��ũ��Ʈ�� �޾ƿ´�.
            BulletController Controller = Obj.AddComponent<BulletController>();

            // ** �Ѿ� ��ũ��Ʈ������ ���� ������ ���� �÷��̾��� ���� ������ ���� �Ѵ�.
            Controller.Direction = new Vector3(Direction, 0.0f, 0.0f);


            // ** �Ѿ� ��ũ��Ʈ������ FX Prefab�� �����Ѵ�.
            Controller.fxPrefab = fxPrefab;

            // ** �Ѿ��� SpriteRenderer�� �޾ƿ´�.
            SpriteRenderer buleltRenderer = Obj.GetComponent<SpriteRenderer>();

            // ** �Ѿ��� �̹��� ���� ���¸� �÷��̾��� �̹��� ���� ���·� �����Ѵ�.
            buleltRenderer.flipX = playerRenderer.flipX;

            // ** ��� ������ ����Ǿ��ٸ� ����ҿ� �����Ѵ�.
            Bullets.Add(Obj);
        }

        // **  Input.GetAxis =     -1 ~ 1 ������ ���� ��ȯ��. 
        float Hor = Input.GetAxisRaw("Horizontal"); // -1 or 0 or 1 ���߿� �ϳ��� ��ȯ.
        float Ver = Input.GetAxisRaw("Vertical"); // -1 or 0 or 1 ���߿� �ϳ��� ��ȯ.

        // ** �Է¹��� ������ �÷��̾ �����δ�.
        Movement = new Vector3(
            Hor * Time.deltaTime * Speed,
            Ver * Time.deltaTime * (Speed * 0.5f),
            0.0f);

        transform.position += new Vector3(0.0f, Movement.y, 0.0f);

        // ** Hor�� 0�̶�� �����ִ� �����̹Ƿ� ����ó���� ���ش�. 
        if (Hor != 0)
            Direction = Hor;

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            // ** �÷��̾��� ��ǥ�� 0.1f ���� ������ �÷��̾ �����δ�.
            if (transform.position.x < 0.1f)
                transform.position += Movement;
            else
            {
                ControllerManager.GetInstance().DirRight = true;
                ControllerManager.GetInstance().DirLeft = false;
            }
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            ControllerManager.GetInstance().DirRight = false;
            ControllerManager.GetInstance().DirLeft = true;

            // ** �÷��̾��� ��ǥ�� -15.0 ���� Ŭ�� �÷��̾ �����δ�.
            if (transform.position.x > -15.0f)
                // ** ���� �÷��̾ �����δ�.
                transform.position += Movement;
        }

        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow) ||
            Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            ControllerManager.GetInstance().DirRight = false;
            ControllerManager.GetInstance().DirLeft = false;
        }

        // ** �÷��̾ �ٶ󺸰��ִ� ���⿡ ���� �̹��� ���� ����.
        if (Direction < 0)
        {
            playerRenderer.flipX = DirLeft = true;
        }
        else if (Direction > 0)
        {
            playerRenderer.flipX = false;
            DirRight = true;
        }

        // ** �÷����� �����ӿ� ���� �̵� ����� ���� z�Ѵ�.
        animator.SetFloat("Speed", Hor);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �浹�� ����� �����Ѵ�
        if (collision.transform.tag == "Enemy")
        {
            OnHit();
            ControllerManager.GetInstance().Player_HP -= 1;
        }
        else
        {
            // ���� ȿ���� ������ ������ ����
            GameObject camera = new GameObject("Camera Test");

            // ���� ȿ�� ��Ʈ�ѷ� ����
            camera.AddComponent<CameraController>();
        }
    }

    private void ReleaseEnemy()
    {
        Destroy(gameObject, 0.016f);
    }

    private void OnAttack()
    {
        // ** �̹� ���ݸ���� �������̶��
        if (onAttack)
            // ** �Լ��� �����Ų��.
            return;

        // ** �Լ��� ������� �ʾҴٸ�...
        // ** ���ݻ��¸� Ȱ��ȭ �ϰ�.
        onAttack = true;

        // ** ���ݸ���� ���� ��Ų��.
        animator.SetTrigger("Attack");
    }

    private void SetAttack()
    {
        // ** �Լ��� ����Ǹ� ���ݸ���� ��Ȱ��ȭ �ȴ�.
        // ** �Լ��� �ִϸ��̼� Ŭ���� �̺�Ʈ ���������� ���Ե�.
        onAttack = false;
    }

    private void OnHit()
    {
        // ** �̹� �ǰݸ���� �������̶��
        if (onHit)
            // ** �Լ��� �����Ų��.
            return;

        // ** �Լ��� ������� �ʾҴٸ�...
        // ** �ǰݻ��¸� Ȱ��ȭ �ϰ�.
        onHit = true;

        // ** �ǰݸ���� ���� ��Ų��.
        animator.SetTrigger("Hit");
    }

    private void SetHit()
    {
        // ** �Լ��� ����Ǹ� �ǰݸ���� ��Ȱ��ȭ �ȴ�.
        // ** �Լ��� �ִϸ��̼� Ŭ���� �̺�Ʈ ���������� ���Ե�.
        onHit = false;
    }

    private void OnDeath()
    {
        // ** �̹� �ǰݸ���� �������̶��
        if (onDeath)
            // ** �Լ��� �����Ų��.
            return;

        // ** �Լ��� ������� �ʾҴٸ�...
        // ** �ǰݻ��¸� Ȱ��ȭ �ϰ�.
        onDeath = true;

        // ** �ǰݸ���� ���� ��Ų��.
        animator.SetTrigger("Death");
    }

    private void SetDeath()
    {
        // ** �Լ��� ����Ǹ� �ǰݸ���� ��Ȱ��ȭ �ȴ�.
        // ** �Լ��� �ִϸ��̼� Ŭ���� �̺�Ʈ ���������� ���Ե�.
        onDeath = false;
    }

    public void TakeDamage(int damage)
    {
        OnHit();
        ControllerManager.GetInstance().Player_HP -= damage;
        //print(ControllerManager.GetInstance().Player_HP);
    }
}