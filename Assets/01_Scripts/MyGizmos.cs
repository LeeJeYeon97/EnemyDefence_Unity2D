using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MyGizmos : MonoBehaviour
{

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1.0f, 1.0f, 1.0f, 0.8f);
        Gizmos.DrawSphere(transform.position, 1.0f);
    }
}
