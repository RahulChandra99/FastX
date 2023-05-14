
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class RTGWaypointsContainer : MonoBehaviour
{

    public List<Transform> waypoints = new List<Transform>();

    private float GetAngulo(Transform origem, Transform target)
    {
        float r;

        GameObject bulsola = new GameObject("Bulsola");
        bulsola.transform.parent = origem;
        bulsola.transform.localPosition = new Vector3(0, 0, 0);

        bulsola.transform.LookAt(target);
        r = bulsola.transform.localEulerAngles.y;

        Destroy(bulsola);
        return r;

    }

    void OnDrawGizmos()
    {


        for (int i = 0; i < waypoints.Count; i++)
        {

            Gizmos.color = Color.green;
            Gizmos.DrawSphere(waypoints[i].transform.position, 1);
            Gizmos.DrawWireSphere (waypoints[i].transform.position, 5f);

            if (i < waypoints.Count - 1)
            {
                if (waypoints[i] && waypoints[i + 1])
                {


                    if (waypoints.Count > 0)
                    {
                        //Gizmos.color = Color.green;
                        if (i < waypoints.Count - 1)
                        {
                            Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
                            waypoints[i].LookAt(waypoints[i + 1]);

                        }

                        //if(i < waypoints.Count - 2)
                        //Gizmos.DrawLine(waypoints[waypoints.Count - 1].position, waypoints[0].position); 

                    }
                }
            }
            else if (i == waypoints.Count - 1)
            {
                waypoints[i].rotation = waypoints[i - 1].rotation; // Quaternion.LookRotation(waypoints[i].position - waypoints[i - 1].position);
            }

        }

    }


}
