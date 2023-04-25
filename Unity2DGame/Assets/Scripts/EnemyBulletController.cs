using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyBulletController : MonoBehaviour
{
    // 총알이 날아가는 속도
    private float Speed;

    // 이펙트 효과 원본
    public GameObject fxPrefab;

    // 총알이 날아가야 할 방향
    public Vector3 Direction { get; set; }

    public GameObject Target;

    private void Start()
    {
        // 속도 초기값
        Speed = ControllerManager.GetInstance().BulletSpeed;

        Target = GameObject.Find("Player");
    }

    void Update()
    {
        // 방향으로 속도만큼 위치를 변경
        transform.position += Direction * Speed * Time.deltaTime;

        float Distance = Vector3.Distance(Target.transform.position, transform.position);

        if (Distance >= 25.0f)
        {
            Destroy(gameObject, 0.016f);
        }
    }

    // 충돌체와 물리엔진이 포함된 오브젝트가 다른 충돌체와 충돌한다면 실행되는 함수
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 이펙트 효과 복제
        GameObject Obj = Instantiate(fxPrefab);

        // 이펙트 효과의 위치를 지정
        Obj.transform.position = transform.position;
        // collision = 충돌한 대상
        // 충돌한 대상을 삭제한다
        
        if (collision.transform.tag == "wall")
            Destroy(this.gameObject);
        else if (collision.transform.tag == "Player")
        {
            collision.GetComponent<PlayerController>().TakeDamage(ControllerManager.GetInstance().BossThrow);
            //print(ControllerManager.GetInstance().Player_HP);
        }
    }
}
