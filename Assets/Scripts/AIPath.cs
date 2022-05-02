using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPath : MonoBehaviour
{
    [SerializeField] private Color color;
    private List<Transform> nodes;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = color;
        Transform[] path = GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();
        foreach(Transform node in path)
        {
            if(node != transform)
            {
                nodes.Add(node);
            }
        }
        for (int i = 0; i < nodes.Count; i++)
        {
            Vector3 current = nodes[i].position;
            Vector3 previous = Vector3.zero;
            if( i > 0)
            {
                previous = nodes[i - 1].position;
            }
            else
            {
                if(i == 0 && nodes.Count > 1)
                {
                    previous = nodes[nodes.Count - 1].position;
                }
            }
            Gizmos.DrawLine(previous, current);
            Gizmos.DrawWireSphere(current, 0.4f);
        }
    }
}
