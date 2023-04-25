using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControll : MonoBehaviour
{
    // ** �Ѿ��� ���ư��� �ӵ�
    private float Speed;
    public GameObject Target;

    public bool Option;

    // ** ����Ʈȿ�� ����
    public GameObject fxPrefab;

    // ** �Ѿ��� ���ư����� ����
    public Vector3 Direction { get; set; }

    private void Awake()
    {
        Target = GameObject.Find("Boss");
    }

    private void Start()
    {
        // ** �ӵ� �ʱⰪ
        //Speed = ControllerManager.GetInstance().BulletSpeed;
        //Speed = Option ? 0.75f : 1.0f;
        Speed = Option ? 5.0f : 1.0f;

        // ** ������ ����ȭ.
        if (Option)
            Direction = (Target.transform.position - transform.position);
        Direction.Normalize();

        float fAngle = getAngle(Vector3.down, Direction);

        transform.eulerAngles = new Vector3(
            0.0f, 0.0f, fAngle);
    }

    void Update()
    {
        // ** �ǽð����� Ÿ���� ��ġ�� Ȯ���ϰ� ������ �����Ѵ�.
        if (Option && Target)
        {
            Direction = (Target.transform.position - transform.position).normalized;

            float fAngle = getAngle(Vector3.down, Target.transform.position);

            transform.eulerAngles = new Vector3(
                0.0f, 0.0f, fAngle);
        }

        // ** �������� �ӵ���ŭ ��ġ�� ����
        transform.position += Direction * Speed * Time.deltaTime;
    }

    // ** �浹ü�� ���������� ���Ե� ������Ʈ�� �ٸ� �浹ü�� �浹�Ѵٸ� ����Ǵ� �Լ�. 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ** ����Ʈȿ�� ����.
        GameObject Obj = Instantiate(fxPrefab);

        // ** ����Ʈȿ���� ��ġ�� ����
        Obj.transform.position = transform.position;

        // ** collision = �浹�� ���.
        // ** �浹�� ����� �����Ѵ�. 
        if (collision.transform.tag == "wall")
            Destroy(this.gameObject);
        else
        {
            // ** ����ȿ���� ������ ������ ����.
            GameObject camera = new GameObject("Camera Test");

            // ** ���� ȿ�� ��Ʈ�ѷ� ����.
            camera.AddComponent<CameraController>();
        }
    }

    public float getAngle(Vector3 from, Vector3 to)
    {
        return Quaternion.FromToRotation(Vector3.down, to - from).eulerAngles.z;
    }
}
