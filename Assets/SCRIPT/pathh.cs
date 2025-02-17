using System.Collections;
using UnityEngine;
using System.Runtime.CompilerServices;
using System.Collections.Generic;

public class pathh : MonoBehaviour
{
    public Color linecolor;
    private List<Transform> node = new List<Transform>();
    private void OnDrawGizmos()
    {
        Gizmos.color = linecolor;
        Transform[] pathtransform = GetComponentsInChildren<Transform>();
        node = new List<Transform>();
        for (int i = 0; i < pathtransform.Length; i++)
        {
            if (pathtransform[i] != transform)
            {
                node.Add(pathtransform[i]);
            }
        }
        for (int i = 0; i < node.Count; i++)
        {
            Vector3 currentnode = node[i].position;
            Vector3 previousnode = Vector3.zero;
            if (i > 0)
            {
                previousnode = node[i - 1].position;
            }
            else if (i == 0 && node.Count > 1)
            {
                previousnode = node[node.Count - 1].position;
            }
            Gizmos.DrawLine(previousnode, currentnode);
            Gizmos.DrawWireSphere(currentnode, 0.3f);
        }

    }
}
