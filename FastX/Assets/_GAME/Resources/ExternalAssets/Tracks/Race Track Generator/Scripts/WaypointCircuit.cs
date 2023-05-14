using UnityEngine;
using System.Collections;


    public class WaypointCircuit : MonoBehaviour
    {
    

        private int numPoints;
        private Vector3[] points;
        private float[] distances;

        public float Length { get; private set; }

        public Transform[] Waypoints;


        //this being here will save GC allocs
        private int p0n;
        private int p1n;
        private int p2n;
        private int p3n;

        private float i;
        private Vector3 P0;
        private Vector3 P1;
        private Vector3 P2;
        private Vector3 P3;

        private bool jf = false;

        // Use this for initialization
        private void Awake()
        {
            GetWayPoints();

            if (Waypoints.Length > 1)
            {
                CachePositionsAndDistances();
            }
            numPoints = Waypoints.Length;


        }



        public void AddWayPoint(Transform newTile)
        {



            Vector3 newTilePosition = newTile.position;

            GameObject wp = new GameObject("Waypoint " + Waypoints.Length.ToString("000"));

            wp.transform.localPosition = newTilePosition;
            wp.transform.localRotation = Quaternion.Euler(newTile.eulerAngles);

            wp.transform.SetParent(transform);

            GetWayPoints();

        }

        public void GetWayPoints()
        {
            
            Waypoints = new Transform[transform.childCount];
            int n = 0;
            foreach (Transform child in transform) Waypoints[n++] = child;
            
        }

        public RoutePoint GetRoutePoint(float dist)
        {
            // position and direction
            Vector3 p1 = GetRoutePosition(dist);
            Vector3 p2 = GetRoutePosition(dist + 0.1f);
            Vector3 delta = p2 - p1;
            return new RoutePoint(p1, delta.normalized);
        }

        public Vector3 GetRoutePosition(float dist)
        {
            int point = 0;

            if (!jf)
            {

                if (Waypoints.Length > 1)
                {
                    CachePositionsAndDistances();
                }
                numPoints = Waypoints.Length;
             }

            if (Length == 0)
            {
                Length = distances[distances.Length - 1];
            }

            dist = Mathf.Repeat(dist, Length);

            while (distances[point] < dist)
            {
                ++point;
            }


            p1n = ((point - 1) + numPoints)%numPoints;
            p2n = point;



            i = Mathf.InverseLerp(distances[p1n], distances[p2n], dist);


                p1n = ((point - 1) + numPoints) % numPoints;
                p2n = point;

                return Vector3.Lerp(points[p1n], points[p2n], i);

        




    }



        private void CachePositionsAndDistances()
        {

            jf = true;
            // transfer the position of each point and distances between points to arrays for
            // speed of lookup at runtime
            points = new Vector3[Waypoints.Length + 1];
            distances = new float[Waypoints.Length + 1];

            float accumulateDistance = 0;
            for (int i = 0; i < points.Length; ++i)
            {
                var t1 = Waypoints[(i)%Waypoints.Length];
                var t2 = Waypoints[(i + 1)%Waypoints.Length];
                if (t1 != null && t2 != null)
                {
                    Vector3 p1 = t1.position;
                    Vector3 p2 = t2.position;
                    points[i] = Waypoints[i%Waypoints.Length].position;
                    distances[i] = accumulateDistance;
                    accumulateDistance += (p1 - p2).magnitude;
                }
            }
            
        }


        private void OnDrawGizmos()
        {
            DrawGizmos(false);
        }

        private void OnDrawGizmosSelected()
        {
            DrawGizmos(true);
        }
   
         private void DrawGizmos(bool selected)
         {


             if (Waypoints.Length > 1)
             {
                 numPoints = Waypoints.Length;

                 CachePositionsAndDistances();
                 Length = distances[distances.Length - 1];

                 Gizmos.color = Color.green; //  selected ? Color.green : Color.blue; //new Color(1, 1, 0, 0.5f);
                 Vector3 prev = Waypoints[0].position;


                     for (int n = 0; n < Waypoints.Length; ++n)
                     {
                         Vector3 next = Waypoints[(n + 1) % Waypoints.Length].position;
                         Gizmos.DrawLine(prev, next);
                         prev = next;
                     }


             }

         }
    


        [System.Serializable]

        public struct RoutePoint
        {
            public Vector3 position;
            public Vector3 direction;

            public RoutePoint(Vector3 position, Vector3 direction)
            {
                this.position = position;
                this.direction = direction;
            }

        }


    }



