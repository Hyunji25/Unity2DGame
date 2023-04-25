using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyBulletController : MonoBehaviour
{
    // �Ѿ��� ���ư��� �ӵ�
    private float Speed;

    // ����Ʈ ȿ�� ����
    public GameObject fxPrefab;

    // �Ѿ��� ���ư��� �� ����
    public Vector3 Direction { get; set; }

    public GameObject Target;

    private void Start()
    {
        // �ӵ� �ʱⰪ
        Speed = ControllerManager.GetInstance().BulletSpeed;

        Target = GameObject.Find("Player");
    }

    void Update()
    {
        // �������� �ӵ���ŭ ��ġ�� ����
        transform.position += Direction * Speed * Time.deltaTime;

        float Distance = Vector3.Distance(Target.transform.position, transform.position);

        if (Distance >= 25.0f)
        {
            Destroy(gameObject, 0.016f);
        }
    }

    // �浹ü�� ���������� ���Ե� ������Ʈ�� �ٸ� �浹ü�� �浹�Ѵٸ� ����Ǵ� �Լ�
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ����Ʈ ȿ�� ����
        GameObject Obj = Instantiate(fxPrefab);

        // ����Ʈ ȿ���� ��ġ�� ����
        Obj.transform.position = transform.position;
        // collision = �浹�� ���
        // �浹�� ����� �����Ѵ�
        
        if (collision.transform.tag == "wall")
            Destroy(this.gameObject);
        else if (collision.transform.tag == "Player")
        {
            collision.GetComponent<PlayerController>().TakeDamage(ControllerManager.GetInstance().BossThrow);
            //print(ControllerManager.GetInstance().Player_HP);
        }
    }
}
