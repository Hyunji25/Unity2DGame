using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScopeController : MonoBehaviour
{
    // 0 ~ 180
    [Range(0.0f, 360.0f)]
    public float Angle = 90.0f;

    // 가상의 선의 개수
    [Range(10, 72)]
    public int Segments = 30;

    // 반지름
    [Range(1.0f, 50.0f)]
    public float radius = 5.0f;

    public List<Vector3> PointList = new List<Vector3>();

    private MeshFilter meshFilter;
    private Mesh mesh;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        mesh = new Mesh();
    }

    void Start()
    {
        Angle = 90.0f;
        Segments = 36;
        radius = 5.0f;


#if UNITY_EDITOR

#else

#endif
    }

    
    void Update()
    {
        int PointCount = Segments + 1;
        Vector3[] vertices = new Vector3[PointCount];

        vertices[0] = Vector3.zero;

        float deltaAngle = transform.eulerAngles .y - (Angle * 0.5f);
        for (int i = 1; i < vertices.Length; ++i)
        {
            vertices[i] = new Vector3(
                Mathf.Sin(deltaAngle * Mathf.Deg2Rad),
                0.0f,
                Mathf.Cos(deltaAngle * Mathf.Deg2Rad)) * radius;
            deltaAngle += Angle / (Segments-1);
        }

        int[] triangles = new int[(PointCount - 2) * 3];

        for (int i = 0; i < PointCount - 2; ++i)
        {
            triangles[i * 3] = 0;
            triangles[i * 3 + 1] = i + 1;
            triangles[i * 3 + 2] = i + 2;
        }

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        meshFilter.mesh = mesh;





        //transform.eulerAngles = new Vector3(0.0f, transform.eulerAngles.y + 15.0f * Time.deltaTime, 0.0f);
        //transform.rotation = Quaternion.Euler(0.0f, transform.eulerAngles.y + 15.0f * Time.deltaTime, 0.0f);



#if UNITY_EDITOR

#else

#endif
    }
}