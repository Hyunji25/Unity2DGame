using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGizmo2 : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawSphere(this.transform.position, 0.2f);
    }
}
