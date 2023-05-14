using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
using System.Linq;


public class Track_Generator : MonoBehaviour
{


    public Tipos track;

    public GameObject[] backgrounds;
    private GameObject bk;


    public MapMaterials mapMaterials;


    Track_Config trackConfig;

    [HideInInspector]
    public GameObject screenCameraPrefab;
    [HideInInspector]
    public GameObject AudienceSound;

    private GameObject cameraBigScreen;

    int idx = 0;
    int idx_mb = 0;   //Change Material Background

    private int mIdx;
    private int tIdx;
    private int tTipo;

    [System.Serializable]
    public class MapMaterials
    {
        public Material[] roadMaterial;
        public Material[] grassMaterial;
        public Material[] fencesBaseMaterial;
    }

    [System.Serializable]
    public class Tipos
    {
        //public string tag = "street";
        public GameObject[] R11;
        public GameObject[] R12;
        public GameObject[] R13;

        public GameObject[] R21;
        public GameObject[] R22;
        public GameObject[] R23;

        public GameObject[] R31;
        public GameObject[] R32;
        public GameObject[] R33;

        public GameObject[] C11;
        public GameObject[] C12;
        public GameObject[] C13;

        public GameObject[] C21;
        public GameObject[] C22;
        public GameObject[] C23;

        public GameObject[] C31;
        public GameObject[] C32;
        public GameObject[] C33;


        public GameObject[] DC11;
        public GameObject[] DC12;
        public GameObject[] DC13;

        public GameObject[] DC21;
        public GameObject[] DC22;
        public GameObject[] DC23;

        public GameObject[] DC31;
        public GameObject[] DC32;
        public GameObject[] DC33;


        //public GameObject[] Grid11;
        public GameObject[] Grid22;
        //public GameObject[] Grid33;


    }






    private GameObject tContainer;
    private int NextType;




    private string alreadyGone = "";


    public GameObject GetContainer()
    {
        SetContainer();
        return tContainer.gameObject;

    }


    private bool TestAlreadyGone(string t)   {

         if (alreadyGone.Contains(t))
            return true;
        
                
        if (t.Contains("(P)") && alreadyGone.Contains("(P)"))
            return true;

        if (t.Contains("(A)") && alreadyGone.Contains("(A)"))
            return true;

        if (t.Contains("(B)") && alreadyGone.Contains("(B)"))
            return true;


        alreadyGone += t + " ";
        return false;

    }

    private bool SetContainer()
    {

        bool r = false;

        if (!tContainer)
        {

            tContainer = GameObject.Find("RTG Track");
            if (tContainer)
                if (tContainer.GetComponent<Track_Config>())
                {
                    trackConfig = tContainer.GetComponent<Track_Config>();
                    return true;
                }


            if (GameObject.FindObjectOfType<Track_Config>())
                tContainer = GameObject.FindObjectOfType<Track_Config>().gameObject;

            if (tContainer)
            {
                trackConfig = tContainer.GetComponent<Track_Config>();
                r = true;
            }

        }
        else
        {
            trackConfig = tContainer.GetComponent<Track_Config>();
            r = true;
        }
        return r;
    }

    public bool CreateTrack(bool enableAudience, bool audioAudience, int _size)
    {

        bool mobile = ((Application.platform == RuntimePlatform.IPhonePlayer || (Application.platform == RuntimePlatform.Android)));

        if (mobile) audioAudience = false;

        alreadyGone = "";

        DestroyImmediate(GameObject.Find("RTG Track"));

        tContainer = new GameObject("RTG Track");
        trackConfig = tContainer.AddComponent(typeof(Track_Config)) as Track_Config;

        if (!tContainer) return false;

        int tp;

        mIdx = -1;



        int ns;
        if (_size == 1)
        {
            ns = Random.Range(1, 50);
        }
        else
        {
            ns = Random.Range(51, 90);


        }

        

        if (ns <= 20)  // Small
        {

            trackConfig.Newlist(6);

            tp = CreateModuloNew(6, 0, 2, 0, 0, true);
            NextType = CreateModuloNew(7, 1, 2, tp, 0, false);
            NextType = CreateModuloNew(11, 1, 3, NextType, 0, false);
            NextType = CreateModuloNew(10, 0, 4, NextType, 0, false);
            NextType = CreateModuloNew(9, 1, 4, NextType, 0, false);
            NextType = CreateModuloNew(5, 1, 1, NextType, tp, false);


        }
        else if (ns <= 30)
        {   // parte é Double

            trackConfig.Newlist(5);

            tp = CreateModuloNew(6, 0, 2, 0, 0, true);
            NextType = CreateModuloNew(7, 10, 3, tp, 0, false);
            NextType = CreateModuloNew(10, 0, 4, NextType, 0, false);
            NextType = CreateModuloNew(9, 1, 4, NextType, 0, false);
            NextType = CreateModuloNew(5, 1, 1, NextType, tp, false);

        }
        else if (ns <= 40)
        {   // parte é Double

            trackConfig.Newlist(5);

            tp = CreateModuloNew(6, 0, 2, 0, 0, true);
            NextType = CreateModuloNew(7, 1, 2, tp, 0, false);
            NextType = CreateModuloNew(11, 1, 3, NextType, 0, false);
            NextType = CreateModuloNew(10, 0, 4, NextType, 0, false);
            NextType = CreateModuloNew(9, 10, 1, NextType, tp, false);


        }
        else if (ns <= 50)
        {   // parte é Double

            trackConfig.Newlist(4);

            tp = CreateModuloNew(6, 0, 2, 0, 0, true);
            NextType = CreateModuloNew(7, 10, 3, tp, 0, false);
            NextType = CreateModuloNew(10, 0, 4, NextType, 0, false);
            NextType = CreateModuloNew(9, 10, 1, NextType, tp, false);

        }


        else if (ns <= 60)
        {

            trackConfig.Newlist(8);

            tp = CreateModuloNew(6, 0, 2, 0, 0, true);
            NextType = CreateModuloNew(7, 0, 2, tp, 0, false);
            NextType = CreateModuloNew(8, 1, 2, NextType, 0, false);
            NextType = CreateModuloNew(12, 1, 3, NextType, 0, false);
            NextType = CreateModuloNew(11, 0, 4, NextType, 0, false);
            NextType = CreateModuloNew(10, 0, 4, NextType, 0, false);
            NextType = CreateModuloNew(9, 1, 4, NextType, 0, false);
            NextType = CreateModuloNew(5, 1, 1, NextType, tp, false);


        }
        else if (ns <= 70)
        {

            trackConfig.Newlist(7);

            tp = CreateModuloNew(6, 0, 2, 0, 0, true);
            NextType = CreateModuloNew(7, 0, 2, tp, 0, false);
            NextType = CreateModuloNew(8, 1, 2, NextType, 0, false);
            NextType = CreateModuloNew(12, 1, 3, NextType, 0, false);
            NextType = CreateModuloNew(11, 0, 4, NextType, 0, false);
            NextType = CreateModuloNew(10, 0, 4, NextType, 0, false);
            NextType = CreateModuloNew(9, 10, 1, NextType, tp, false);

        }
        else if (ns <= 80)
        {

            trackConfig.Newlist(7);

            tp = CreateModuloNew(6, 0, 2, 0, 0, true);
            NextType = CreateModuloNew(7, 0, 2, tp, 0, false);
            NextType = CreateModuloNew(8, 10, 3, NextType, 0, false);
            NextType = CreateModuloNew(11, 0, 4, NextType, 0, false);
            NextType = CreateModuloNew(10, 0, 4, NextType, 0, false);
            NextType = CreateModuloNew(9, 1, 4, NextType, 0, false);
            NextType = CreateModuloNew(5, 1, 1, NextType, tp, false);



        }
        else
        {

            trackConfig.Newlist(6);

            tp = CreateModuloNew(6, 0, 2, 0, 0, true);
            NextType = CreateModuloNew(7, 0, 2, tp, 0, false);
            NextType = CreateModuloNew(8, 10, 3, NextType, 0, false);
            NextType = CreateModuloNew(11, 0, 4, NextType, 0, false);
            NextType = CreateModuloNew(10, 0, 4, NextType, 0, false);
            NextType = CreateModuloNew(9, 10, 1, NextType, tp, false);



        }


        CreateWayPoints();

        //Apply random materials to Roads, Geass and Fences
        ChangeTextureRoad (0 , tContainer, Random.Range(0, mapMaterials.roadMaterial.Length));
        ChangeTextureGrass(0, tContainer, Random.Range(0, mapMaterials.grassMaterial.Length));
        ChangeTextureFenceBase(0, tContainer, Random.Range(0, mapMaterials.fencesBaseMaterial.Length));



        //CameraScreen

        //if (!mobile) {
            cameraBigScreen = (GameObject)Instantiate(screenCameraPrefab, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
            cameraBigScreen.name = "Camera-BigScreens";
            cameraBigScreen.transform.SetParent(tContainer.transform);
        //} else {
        //    audioAudience = false;
        //}



        //Define background
        ChangeBackGround(tContainer.transform);


        //Audience and sound audience
        AddAudience(enableAudience);
        if (enableAudience == false) audioAudience = false;
        if (audioAudience) AddSoundAudience(audioAudience);


        /*
        if (Camera.main)
        {
            Camera.main.transform.position = GameObject.Find("Camera_Point").transform.position;
            Camera.main.transform.rotation = GameObject.Find("Camera_Point").transform.rotation;
        }
        */

        return true;

    }

    public void ChangeBackGround(Transform container)
    {

        Transform pai = container;

        if (backgrounds.Length > 0)
        {

            bk = GameObject.Find("_Background");

            if (bk) pai = bk.transform.parent;

            if (bk) DestroyImmediate(bk.gameObject);

            idx_mb++;
            if (idx_mb > backgrounds.Length - 1)
                idx_mb = 0;

            bk = (GameObject)Instantiate(backgrounds[idx_mb], new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
            bk.name = "_Background";

            bk.transform.rotation = Quaternion.Euler(0, Random.Range(1, 359), 0);

            CenterBackGround(bk.transform);

            if (pai) bk.transform.SetParent(pai.transform);

            

        }

    }



    private int CreateModuloNew(int bloco, int tipo, int orientacao, int tipoBegin, int tipoEnd, bool pit)
    {



        GameObject m;
        GameObject md;
        int i = 0;

        mIdx++;


        if (pit)
        {

            tipoBegin = Random.Range(1, 4);

            idx++;
            if (idx > track.Grid22.Length - 1)
                idx = 0;


            tIdx = idx;



            //tipoBegin = 2;
            NextType = tipoBegin;

            m = track.Grid22[idx];

            md = (GameObject)Instantiate(m, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
            md.transform.SetParent(tContainer.transform);
            md.transform.localPosition = GetSetPosition(bloco) + new Vector3(0, 0, (125 * (tipoBegin - 2)) * -1);



        }
        else
        {


            do
            {
                m = GetTrack(tipo, tipoBegin, tipoEnd, 0);
                i++;
            } while (TestAlreadyGone(m.name) && i < 20);

            md = (GameObject)Instantiate(m, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
            md.transform.SetParent(tContainer.transform);
            md.transform.localPosition = GetSetPosition(bloco); //+ modulo.transform.position;

        }

        md.name = md.name.Replace("(Clone)", "");

        int[] _direction = { 0, 0, 90, 180, 270 };

        md.transform.localRotation = Quaternion.Euler(0, _direction[orientacao], 0);

        trackConfig.modulo[mIdx] = md;
        trackConfig.AddConfig(mIdx, tTipo, tipoBegin, NextType, tIdx);

        return NextType;

    }





    public GameObject ChangeSection(GameObject section, bool nextSection)
    {


        if (!tContainer)
        {

            if (section.transform.root.gameObject.GetComponent<Track_Config>())
                tContainer = section.transform.root.gameObject;
            else
                SetContainer();

            if (tContainer)
            {

                trackConfig = tContainer.GetComponent<Track_Config>();
                mIdx = trackConfig.modulo.Length - 1;
            }

        }

        if (!tContainer) return null;

        if (section.transform.root != tContainer.transform)
        {
            Debug.Log("Select the section of circuit you want to change");
            return null;
        }


        if (section.transform.root == section.transform)
        {
            Debug.Log("Select the section of circuit you want to change");
            return null;
        }


        Transform t = section.transform;

        for (int i = 0; i < 10; i++)
        {

            if (t.parent == tContainer.transform)
            {
               return ChangeSection2(t, nextSection);
            }
            else
            {
                t = t.parent;
                if (tContainer.transform.Find("W-P-C"))
                    CreateWayPoints();
            }

        }

        return null;

    }


    public GameObject ChangeSection2(Transform section, bool nextSection)
    {

        int _num = -1;

        if (!tContainer)
            SetContainer();
        else if (!trackConfig)
            trackConfig = tContainer.GetComponent<Track_Config>();


        //int q = trackConfig.modulo.Length;

        mIdx = trackConfig.modulo.Length - 1;

        for (int i = 0; i <= mIdx; i++)
        {
            if (trackConfig.modulo[i].gameObject == section.gameObject)
            {
                _num = i;
                break;
            }
        }


        if (_num == -1) return null;

        int _tipo = trackConfig._tipo[_num];
        int _begin = trackConfig._begin[_num];
        int _end = trackConfig._end[_num];
        int _idx = trackConfig._idx[_num];


        if (nextSection && (_num < trackConfig.modulo.Length - 1)) _end = 0;

        GameObject GP;

        if (_num == 0)
        {

            tIdx = _idx + 1;

            if (tIdx > track.Grid22.Length - 1)
                tIdx = 0;

            GP = track.Grid22[tIdx];

            NextType = _begin;

        }
        else
        {

            GP = GetTrack(_tipo, _begin, _end, _idx + 1);

        }


        GameObject _modulo;

        _modulo = (GameObject)Instantiate(GP, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
        _modulo.transform.SetParent(tContainer.transform);


        _modulo.transform.localPosition = section.transform.localPosition;
        _modulo.transform.localRotation = section.transform.localRotation;

        _modulo.name = _modulo.name.Replace("(Clone)", "");


        trackConfig.modulo[_num] = _modulo.gameObject;
        trackConfig._idx[_num] = tIdx;

        DestroyImmediate(section.gameObject);
        
        // Update Materials
        Material[] mats;
        GameObject gobj;
        Material atualMaterial;

        gobj = _modulo.transform.Find("Meshes/Road").gameObject;
        atualMaterial = mapMaterials.roadMaterial[trackConfig.indexMaterialRoad];
        mats = gobj.GetComponent<MeshRenderer>().sharedMaterials;
        mats[0] = atualMaterial;
        gobj.GetComponent<MeshRenderer>().sharedMaterials = mats;

        gobj = _modulo.transform.Find("Meshes/Grass").gameObject;
        atualMaterial = mapMaterials.grassMaterial[trackConfig.indexMaterialGrass];
        mats = gobj.GetComponent<MeshRenderer>().sharedMaterials;
        mats[0] = atualMaterial;
        gobj.GetComponent<MeshRenderer>().sharedMaterials = mats;

        gobj = _modulo.transform.Find("Meshes/Track").gameObject;
        atualMaterial = mapMaterials.fencesBaseMaterial[trackConfig.indexMaterialFences];
        mats = gobj.GetComponent<MeshRenderer>().sharedMaterials;
        mats[0] = atualMaterial;
        gobj.GetComponent<MeshRenderer>().sharedMaterials = mats;

        GameObject retorno = null;

        retorno = _modulo;


        if (nextSection)
        {


            trackConfig._end[_num] = NextType;

            _num++;
            if (_num >= trackConfig.modulo.Length)
                _num = 0;

            trackConfig._begin[_num] = NextType;

            ChangeSection2(trackConfig.modulo[_num].transform, false);

        }

        if (tContainer.transform.Find("W-P-C"))
            CreateWayPoints();

        return retorno;


    }





    public void CreateWayPoints()
    {
        if (!tContainer)
            SetContainer();

        if (!tContainer)
            return;



        Transform wpc;
        Transform wayPoints;

        wpc = tContainer.transform.Find("W-P-C");
        if (wpc) DestroyImmediate(wpc.gameObject);

        wpc = new GameObject("W-P-C").transform;
        wpc.transform.SetParent(tContainer.transform);
        wpc.gameObject.AddComponent<WaypointCircuit>();
        wpc.gameObject.GetComponent<WaypointCircuit>().GetWayPoints();


        GameObject startGrid = GameObject.Find("StartGrid");
        if (startGrid)
            wpc.GetComponent<WaypointCircuit>().AddWayPoint(startGrid.transform);

        mIdx = trackConfig.modulo.Length - 1;

        for (int i = 0; i <= mIdx; i++)
        {

            wayPoints = trackConfig.modulo[i].transform.Find("Waypoints");

            foreach (Transform childs in wayPoints)
            {
                wpc.GetComponent<WaypointCircuit>().AddWayPoint(childs);
            }


        }

        
        
        if (startGrid)
            wpc.GetComponent<WaypointCircuit>().AddWayPoint(startGrid.transform);
        

        /*
        float Kilometros = wpc.GetComponent<WaypointCircuit>().Kilometros();
        Debug.Log("Distance: " + Kilometros + " KM");
        */

    }





    private int GetIdx(int maximo, int definido)
    {
        if (definido == 0)
            return Random.Range(0, maximo);
        else
        {

            if (definido >= maximo)
                return 0;
            else
                return definido;

        }


    }

    GameObject GetTrack(int tipo, int tipoBegin, int tipoEnd, int idx)
    {


        int f;
        if (tipoEnd == 0)
            tipoEnd = Random.Range(1, 4);

        f = tipoEnd;

        if (tipoBegin == 0)
            tipoBegin = Random.Range(1, 4);

        GameObject r;


        if (tipo == 0)
        {

            if (tipoBegin == 1)
            {

                if (f == 1)
                {
                    tIdx = GetIdx(track.R11.Length, idx);
                    r = track.R11[tIdx];
                }
                else if (f == 2)
                {
                    tIdx = GetIdx(track.R12.Length, idx);
                    r = track.R12[tIdx];
                }
                else
                {
                    tIdx = GetIdx(track.R13.Length, idx);
                    r = track.R13[tIdx];
                }

            }
            else if (tipoBegin == 2)
            {

                if (f == 1)
                {
                    tIdx = GetIdx(track.R21.Length, idx);
                    r = track.R21[tIdx];
                }
                else if (f == 2)
                {
                    tIdx = GetIdx(track.R22.Length, idx);
                    r = track.R22[tIdx];
                }
                else
                {
                    tIdx = GetIdx(track.R23.Length, idx);
                    r = track.R23[tIdx];
                }



            }
            else
            { //if (tipoBegin == 3) {


                if (f == 1)
                {
                    tIdx = GetIdx(track.R31.Length, idx);
                    r = track.R31[tIdx];
                }
                else if (f == 2)
                {
                    tIdx = GetIdx(track.R32.Length, idx);
                    r = track.R32[tIdx];
                }
                else
                {
                    tIdx = GetIdx(track.R33.Length, idx);
                    r = track.R33[tIdx];
                }


            }

        }
        else if (tipo == 1)
        { // Curva a direita

            if (tipoBegin == 1)
            {


                if (f == 1)
                {
                    tIdx = GetIdx(track.C11.Length, idx);
                    r = track.C11[tIdx];
                }
                else if (f == 2)
                {
                    tIdx = GetIdx(track.C12.Length, idx);
                    r = track.C12[tIdx];
                }
                else
                {
                    tIdx = GetIdx(track.C13.Length, idx);
                    r = track.C13[tIdx];
                }

            }
            else if (tipoBegin == 2)
            {

                if (f == 1)
                {
                    tIdx = GetIdx(track.C21.Length, idx);
                    r = track.C21[tIdx];
                }
                else if (f == 2)
                {
                    tIdx = GetIdx(track.C22.Length, idx);
                    r = track.C22[tIdx];
                }
                else
                {
                    tIdx = GetIdx(track.C23.Length, idx);
                    r = track.C23[tIdx];
                }

            }
            else
            { //	if (tipoBegin == 3) {


                if (f == 1)
                {
                    tIdx = GetIdx(track.C31.Length, idx);
                    r = track.C31[tIdx];
                }
                else if (f == 2)
                {
                    tIdx = GetIdx(track.C32.Length, idx);
                    r = track.C32[tIdx];
                }
                else
                {
                    tIdx = GetIdx(track.C33.Length, idx);
                    r = track.C33[tIdx];
                }

            }

        }
        else
        { // if (tipo == 10 ) { // Curva Dupla a direita

            if (tipoBegin == 1)
            {


                if (f == 1)
                {
                    tIdx = GetIdx(track.DC11.Length, idx);
                    r = track.DC11[tIdx];
                }
                else if (f == 2)
                {
                    tIdx = GetIdx(track.DC12.Length, idx);
                    r = track.DC12[tIdx];
                }
                else
                {
                    tIdx = GetIdx(track.DC13.Length, idx);
                    r = track.DC13[tIdx];
                }

            }
            else if (tipoBegin == 2)
            {

                if (f == 1)
                {
                    tIdx = GetIdx(track.DC21.Length, idx);
                    r = track.DC21[tIdx];
                }
                else if (f == 2)
                {
                    tIdx = GetIdx(track.DC22.Length, idx);
                    r = track.DC22[tIdx];
                }
                else
                {
                    tIdx = GetIdx(track.DC23.Length, idx);
                    r = track.DC23[tIdx];
                }

            }
            else
            { //	if (tipoBegin == 3) {


                if (f == 1)
                {
                    tIdx = GetIdx(track.DC31.Length, idx);
                    r = track.DC31[tIdx];
                }
                else if (f == 2)
                {
                    tIdx = GetIdx(track.DC32.Length, idx);
                    r = track.DC32[tIdx];
                }
                else
                {
                    tIdx = GetIdx(track.DC33.Length, idx);
                    r = track.DC33[tIdx];
                }

            }



        }

        NextType = f;
        tTipo = tipo;


        return r;

    }



    private Vector3 GetSetPosition(int bloco)
    {
        if (bloco == 1)
            return new Vector3(-750, 0, 750);
        else if (bloco == 2)
            return new Vector3(-250, 0, 750);
        else if (bloco == 3)
            return new Vector3(250, 0, 750);
        else if (bloco == 4)
            return new Vector3(750, 0, 750);
        else if (bloco == 5)
            return new Vector3(-750, 0, 250);
        else if (bloco == 6)
            return new Vector3(-250, 0, 250);
        else if (bloco == 7)
            return new Vector3(250, 0, 250);
        else if (bloco == 8)
            return new Vector3(750, 0, 250);
        else if (bloco == 9)
            return new Vector3(-750, 0, -250);
        else if (bloco == 10)
            return new Vector3(-250, 0, -250);
        else if (bloco == 11)
            return new Vector3(250, 0, -250);
        else if (bloco == 12)
            return new Vector3(750, 0, -250);
        else if (bloco == 13)
            return new Vector3(-750, 0, -750);
        else if (bloco == 14)
            return new Vector3(-250, 0, -750);
        else if (bloco == 15)
            return new Vector3(250, 0, -750);
        else //if (bloco == 16) 
            return new Vector3(750, 0, -750);
    }











    public void AddSoundAudience(bool ativar)
    {

        GameObject sAud;
        GameObject[] objs = GameObject.FindObjectsOfType(typeof(GameObject)).Select(g => g as GameObject).Where(g => g.name == "Audience").ToArray();

        int n = objs.Length;

        for (int i = 0; i < n; i++)
        {

            if (ativar && objs[i].transform.childCount == 0)
            {

                sAud = (GameObject)Instantiate(AudienceSound, new Vector3(0, 5, 12), Quaternion.Euler(0, 0, 0));
                sAud.transform.SetParent(objs[i].transform);
                sAud.transform.localPosition = new Vector3(0, 4, 12);
                sAud.transform.localRotation = Quaternion.Euler(0, 0, 0);

                sAud.name = "Sound-Audience";

            }
            else
            {


                Component[] temp2 = objs[i].GetComponentsInChildren(typeof(AudioSource));
                foreach (AudioSource currentChild in temp2)
                {
                    DestroyImmediate(currentChild.gameObject);
                    //currentChild.gameObject.GetComponent<AudioSource>().enabled = ativar;
                }



            }


        }



    }


    public void AddAudience(bool ativar)
    {




        GameObject[] audienceObjs = GameObject.FindObjectsOfType(typeof(GameObject)).Select(g => g as GameObject).Where(g => g.name == "Audience").ToArray();


        int n = audienceObjs.Length;

        for (int i = 0; i < n; i++)
        {
            //audienceObjs[i].SetActive(ativar);
            audienceObjs[i].GetComponent<MeshRenderer>().enabled = ativar;
        }




    }


    void CenterBackGround(Transform bk)
    {

        //For Centralize Background 
        float minX = 0;
        float maxX = 0;
        float minZ = 0;
        float maxZ = 0;


            for (int r = 0; r < trackConfig.modulo.Length; r++)
            {


                if (trackConfig.modulo[r].transform.position.x > maxX)
                    maxX = trackConfig.modulo[r].transform.position.x;


                if (trackConfig.modulo[r].transform.position.x < minX)
                    minX = trackConfig.modulo[r].transform.position.x;


                if (trackConfig.modulo[r].transform.position.z > maxZ)
                    maxZ = trackConfig.modulo[r].transform.position.z;


                if (trackConfig.modulo[r].transform.position.z < minZ)
                    minZ = trackConfig.modulo[r].transform.position.z;

            }


            bk.position = new Vector3((minX * 0.5f) + (maxX * 0.5f), 0, (minZ * 0.5f) + (maxZ * 0.5f));

      

    }

    public void Destroytrack()
    {


        if (!SetContainer()) return;

        if (tContainer)
            DestroyImmediate(tContainer.gameObject);


    }


    public void ChangeTextureRoad(int qualMaterial, GameObject GmObj, int forceOld)
    {

        if (GmObj == null)
        {
            if (!SetContainer()) return;
            GmObj = tContainer;
        }

        if (GmObj == null) return;

        if (mapMaterials.roadMaterial.Length == 0)
            return;


        int idx_mr;

    
        if (qualMaterial > 0)
        {

            idx_mr = trackConfig.indexMaterialRoad + qualMaterial;

            if (idx_mr > mapMaterials.roadMaterial.Length - 1)
                idx_mr = 0;


        }
        else if (qualMaterial < 0)
        {

            idx_mr = trackConfig.indexMaterialRoad + qualMaterial;

            if (idx_mr < 0)
                idx_mr = mapMaterials.roadMaterial.Length - 1;

        }
        else
        {

            idx_mr = forceOld; 
        }



        trackConfig.indexMaterialRoad = idx_mr;

        GameObject[] myTracks = GameObject.FindObjectsOfType(typeof(GameObject)).Select(g => g as GameObject).Where(g => g.name == "Road" && g.transform.parent.parent.parent == GmObj.transform).ToArray();

        Material[] mats;

        for (int r = 0; r < myTracks.Length; r++)
        {



            mats = myTracks[r].GetComponent<MeshRenderer>().sharedMaterials;
            mats[0] = mapMaterials.roadMaterial[idx_mr];


            myTracks[r].GetComponent<MeshRenderer>().sharedMaterials = mats;

        }

    }




    public void ChangeTextureGrass(int qual, GameObject GmObj, int forceOld)
    {



        if (GmObj == null)
        {
            if (!SetContainer()) return;
            GmObj = tContainer;
        }

        if (GmObj == null) return;


        if (mapMaterials.grassMaterial.Length == 0)
            return;

        int idx_mg;


        if (qual > 0)
        {

            idx_mg = trackConfig.indexMaterialGrass + qual;

            if (idx_mg > mapMaterials.grassMaterial.Length - 1)
                idx_mg = 0;

        }
        else if (qual < 0)
        {

            idx_mg = trackConfig.indexMaterialGrass + qual;

            if (idx_mg < 0)
                idx_mg = mapMaterials.grassMaterial.Length - 1;

        }
        else
        {

            idx_mg = forceOld;

        }

        trackConfig.indexMaterialGrass = idx_mg;

        GameObject[] myTracks = GameObject.FindObjectsOfType(typeof(GameObject)).Select(g => g as GameObject).Where(g => g.name == "Grass" && g.transform.parent.parent.parent == GmObj.transform).ToArray();

        Material[] mats;

        for (int r = 0; r < myTracks.Length; r++)
        {

            mats = myTracks[r].GetComponent<MeshRenderer>().sharedMaterials;
            mats[0] = mapMaterials.grassMaterial[idx_mg];


            myTracks[r].GetComponent<MeshRenderer>().sharedMaterials = mats;

        }

    }

    



    public void ChangeTextureFenceBase(int qualMaterial, GameObject GmObj, int forceOld)
    {

        if (GmObj == null)
        {
            if (!SetContainer()) return;
            GmObj = tContainer;
        }

        if (GmObj == null) return;

        if (mapMaterials.fencesBaseMaterial.Length == 0)
            return;

        int idx_mf;

        if (qualMaterial > 0)
        {

            idx_mf = trackConfig.indexMaterialFences + qualMaterial;
            if (idx_mf > mapMaterials.fencesBaseMaterial.Length - 1)
                idx_mf = 0;


        }
        else if (qualMaterial < 0)
        {


            idx_mf = trackConfig.indexMaterialFences + qualMaterial;
            if (idx_mf < 0)
                idx_mf = mapMaterials.fencesBaseMaterial.Length - 1;

        }
        else
        {

            idx_mf = forceOld;

        }


        trackConfig.indexMaterialFences = idx_mf;

        GameObject[] myTracks = GameObject.FindObjectsOfType(typeof(GameObject)).Select(g => g as GameObject).Where(g => g.name == "Track" && g.transform.parent.parent.parent == GmObj.transform).ToArray();

        Material[] mats;

        for (int r = 0; r < myTracks.Length; r++)
        {

            mats = myTracks[r].GetComponent<MeshRenderer>().sharedMaterials;
            mats[0] = mapMaterials.fencesBaseMaterial[idx_mf];

            myTracks[r].GetComponent<MeshRenderer>().sharedMaterials = mats;

        }






    }

    


}

