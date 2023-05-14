
using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(RTGWaypointsContainer))]
public class RTGWPEditor : Editor
{

    RTGWaypointsContainer wpScript;

    void OnSceneGUI()
    {

        Event e = Event.current;
        wpScript = (RTGWaypointsContainer)target;

        if (e != null)
        {

            if (e.isMouse && e.shift && e.type == EventType.MouseDown)
            {

                Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
                RaycastHit hit = new RaycastHit();
                if (Physics.Raycast(ray, out hit, 5000.0f))
                {

                    Vector3 newTilePosition = hit.point;

                    GameObject wp = new GameObject("Waypoint " + (wpScript.waypoints.Count + 1).ToString("00"));

                    wp.transform.position = newTilePosition;
                    wp.transform.SetParent(wpScript.transform);

                    GetWaypoints();

                }

            }

            if (wpScript)
                Selection.activeGameObject = wpScript.gameObject;

        }

        GetWaypoints();

    }

    public void GetWaypoints()
    {

        wpScript.waypoints = new List<Transform>();

        Transform[] allTransforms = wpScript.transform.GetComponentsInChildren<Transform>();

        foreach (Transform t in allTransforms)
        {

            if (t != wpScript.transform)
                wpScript.waypoints.Add(t);

        }

    }

}
