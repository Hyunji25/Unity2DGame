using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // �Ѿ��� ���ư��� �ӵ�
    private float Speed;

    // �Ѿ��� �浹�� Ƚ��
    private int hp;

    // ����Ʈ ȿ�� ����
    public GameObject fxPrefab;

    // �Ѿ��� ���ư��� �� ����
    public Vector3 Direction { get; set; }

    private void Start()
    {
        // �ӵ� �ʱⰪ
        Speed = ControllerManager.GetInstance().BulletSpeed;

        // �浹 Ƚ���� 3���� �����Ѵ�
        hp = 1;
    }

    void Update()
    {
        // �������� �ӵ���ŭ ��ġ�� ����
        transform.position += Direction * Speed * Time.deltaTime;
    }
    
    // �浹ü�� ���������� ���Ե� ������Ʈ�� �ٸ� �浹ü�� �浹�Ѵٸ� ����Ǵ� �Լ�
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �浹 Ƚ�� ����
        --hp;

        // ����Ʈ ȿ�� ����
        GameObject Obj = Instantiate(fxPrefab);

        // ����Ʈ ȿ���� ��ġ�� ����
            Obj.transform.position = transform.position;

        

        // collision = �浹�� ���
        // �浹�� ����� �����Ѵ�
        if(collision.transform.tag == "wall")
            Destroy(this.gameObject);
        else
        {
            // ���� ȿ���� ������ ������ ����
            GameObject camera = new GameObject("Camera Test");

            // ���� ȿ�� ��Ʈ�ѷ� ����
            camera.AddComponent<CameraController>();
        }

        // �Ѿ��� �浹 Ƚ���� 0�� �Ǹ� �Ѿ� ����
        if (hp == 0)
            Destroy(this.gameObject, 0.016f);
    }
}