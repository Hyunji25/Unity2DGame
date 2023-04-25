using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour
{
    private float Speed;
    private Vector3 Movement;

    private void Start()
    {
        Speed = ControllerManager.GetInstance().PlayerSpeed;
    }

    void Update()
    {
        float Hor = Input.GetAxisRaw("Horizontal");
        float Ver = Input.GetAxisRaw("Vertical");

        //transform.Translate(new Vector3(Hor, Ver, 0.0f) *  Time.deltaTime * 5.0f);

        Movement = new Vector3(
            Hor * Time.deltaTime * Speed,
            Ver * Time.deltaTime * (Speed * 0.5f),
            0.0f);
        transform.position += new Vector3(Movement.x, Movement.y, 0.0f);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
            Debug.Log("zzzzz");
    }
}
