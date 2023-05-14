using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MiniMap : MonoBehaviour
{

    [Space(10)]
    public Transform player1;       // Put the green arrow on player1
    [Space(10)]
    public Transform player2;       // Put the yellow arrow on player 2 (for split-screen games)

    [Space(10)]
    public Transform[] opponents;   // Put the white circle on the other players(AI)
    [Space(20)]

    [Range(150, 500)]
    public int miniMapScale = 260;

    [Range(0, 1)]
    public float miniMapSize = 0.5f;


    private GameObject miniMapSpritePlayer1;
    private GameObject miniMapSpritePlayer2;
    private GameObject[] miniMapSpriteOpponents;

    [HideInInspector]
    public GameObject miniMapTargetCircle;
    [HideInInspector]
    public GameObject miniMapTargetArrow1;
    [HideInInspector]
    public GameObject miniMapTargetArrow2;

    [HideInInspector]
    public GameObject miniMapCamera;


    private GameObject miniMapTargetContainer;

    [HideInInspector]
    public GameObject miniObj;

    [Space(15)]

    public Material colorRoad;


    void Start()
    {

        //Init();

    }

    void Awake()
    {
        miniObj.transform.localScale = new Vector3(miniMapSize + 0.5f, miniMapSize + 0.5f, 0);

        int iMiniMap = LayerMask.NameToLayer("MiniMapLayer");

        if (iMiniMap == -1)
        {
            throw new UnassignedReferenceException("Layer MiniMapLayer must be assigned in Layer Manager.");

        }



        //Init();

    }

    public void Init()
    {
        if (miniMapTargetContainer)
            Destroy(miniMapTargetContainer);

        miniMapTargetContainer = new GameObject("TargetContainer");
        miniMapTargetContainer.transform.SetParent(transform);

        RoadForMinimapLayer();

        CheckCameras();

        DefinePlayer1();
        DefinePlayer2();
        DefinePlayerOponents();

    }


    void LateUpdate()
    {

    
        miniObj.transform.localScale = new Vector3(miniMapSize + 0.5f, miniMapSize + 0.5f, 0);

        if (player1)
        {

            if (!miniMapSpritePlayer1) DefinePlayer1();

            miniMapSpritePlayer1.transform.position = new Vector3(player1.position.x, player1.position.y + 5.2f, player1.position.z);
            miniMapSpritePlayer1.transform.eulerAngles = new Vector3(90, player1.eulerAngles.y, player1.eulerAngles.z);


            // miniMapCamera.transform.position = new Vector3(player1.position.x, miniMapCamera.transform.position.y, player1.position.z);
            miniMapCamera.transform.position = new Vector3(player1.position.x, miniMapScale, player1.position.z);
            miniMapCamera.transform.eulerAngles = new Vector3(0, player1.eulerAngles.y, 0);

        }
        else if (miniMapSpritePlayer1)
            Destroy(miniMapSpritePlayer1);


        if (player2)
        {

            if (!miniMapSpritePlayer2) DefinePlayer2();

            miniMapSpritePlayer2.transform.position = new Vector3(player2.position.x, player2.position.y + 5.2f, player2.position.z);
            miniMapSpritePlayer2.transform.eulerAngles = new Vector3(90, player2.eulerAngles.y, player2.eulerAngles.z);

        }
        else if (miniMapSpritePlayer2)
            Destroy(miniMapSpritePlayer2);


        for (int i = 0; i < opponents.Length; i++)
        {

            if (opponents[i] && opponents[i].gameObject.activeInHierarchy)
            {


                if (miniMapSpriteOpponents[i])
                    miniMapSpriteOpponents[i].transform.position = new Vector3(opponents[i].position.x, opponents[i].position.y + 5f, opponents[i].position.z);


            }
            else
            {


                if (miniMapSpriteOpponents[i])
                    if (!opponents[i])
                        Destroy(miniMapSpriteOpponents[i]);
                    else
                        miniMapSpriteOpponents[i].SetActive(false);


            }
        }

    }


    private void DefinePlayer1()
    {

        if (player1 && !miniMapSpritePlayer1)
        {
            miniMapSpritePlayer1 = Instantiate(miniMapTargetArrow1, new Vector3(0, 0, 0), Quaternion.Euler(270, 0, 0)) as GameObject;
            miniMapSpritePlayer1.transform.SetParent(miniMapTargetContainer.transform);
            miniMapSpritePlayer1.layer = LayerMask.NameToLayer("MiniMapLayer");
        }

        CheckCameras();
    }

    private void DefinePlayer2()
    {

        if (player2 && !miniMapSpritePlayer2)
        {
            miniMapSpritePlayer2 = Instantiate(miniMapTargetArrow2, new Vector3(0, 0, 0), Quaternion.Euler(270, 0, 0)) as GameObject;
            miniMapSpritePlayer2.transform.SetParent(miniMapTargetContainer.transform);
            miniMapSpritePlayer2.layer = LayerMask.NameToLayer("MiniMapLayer");
        }

        CheckCameras();

    }

    private void DefinePlayerOponents()
    {

        GameObject OpponentsMinimap;

        if (miniMapTargetContainer.transform.Find("OpponentsMarks"))
            Destroy(miniMapTargetContainer.transform.Find("OpponentsMarks").gameObject);

        OpponentsMinimap = new GameObject("OpponentsMarks");

        OpponentsMinimap.transform.SetParent(miniMapTargetContainer.transform);

        miniMapSpriteOpponents = new GameObject[opponents.Length];

        for (int i = 0; i < opponents.Length; i++)
        {
            miniMapSpriteOpponents[i] = Instantiate(miniMapTargetCircle, new Vector3(0, 0, 0), Quaternion.Euler(270, 0, 0)) as GameObject;
            miniMapSpriteOpponents[i].transform.SetParent(OpponentsMinimap.transform);
            miniMapSpriteOpponents[i].layer = LayerMask.NameToLayer("MiniMapLayer");
        }

        CheckCameras();

    }

    public void RoadForMinimapLayer()
    {

        GameObject RoadMinimap;

        if (!miniMapTargetContainer) return;

        if (miniMapTargetContainer.transform.Find("RoadMinimap"))
            Destroy(miniMapTargetContainer.transform.Find("RoadMinimap").gameObject);

        RoadMinimap = new GameObject("RoadMinimap");

        RoadMinimap.transform.SetParent(miniMapTargetContainer.transform);


        //Copy Road objects, and put in the Layer "MiniMapLayer"
        GameObject[] roadObjs = GameObject.FindObjectsOfType(typeof(GameObject)).Select(g => g as GameObject).Where(g => g.name == "Collider-Road").ToArray();
        int n = roadObjs.Length;
        for (int i = 0; i < n; i++)
        {
            GameObject mapRoad = Instantiate(roadObjs[i], roadObjs[i].transform.position + new Vector3(0f, -0.1f, 0f), roadObjs[i].transform.rotation) as GameObject;
            mapRoad.transform.SetParent(RoadMinimap.transform);
            mapRoad.layer = LayerMask.NameToLayer("MiniMapLayer");

            MeshCollider collider = mapRoad.GetComponent<MeshCollider>();
            Destroy(collider);
            mapRoad.AddComponent<MeshRenderer>();
            mapRoad.GetComponent<MeshRenderer>().material = colorRoad;

        }


    }


    void CheckCameras()
    {

        //The Cameras should not see the layer "MiniMapLayer"
        int count = Camera.allCameras.Length;
        for (int i = 0; i < count; i++)
        {
            int old = Camera.allCameras[i].GetComponent<Camera>().cullingMask;
            Camera.allCameras[i].GetComponent<Camera>().cullingMask = old & ~(1 << LayerMask.NameToLayer("MiniMapLayer"));
        }

        //The MiniMap camera sees only the  layer "MiniMapLayer"
        miniMapCamera.transform.Find("Camera").GetComponent<Camera>().cullingMask = (1 << LayerMask.NameToLayer("MiniMapLayer"));

    }

}
