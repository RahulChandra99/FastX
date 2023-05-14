
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class RTG : EditorWindow {

	public GameObject trackGenerator;

    private bool soundAudience = true;
    private bool oldSoundAudience = true;

    private bool enableAudience = true;
    private bool oldEnableAudience = true;

    private Track_Config trackConfig;
    private GameObject tContainer;

    private bool save = true;
    private bool generateLightmapUVs = true;

    [MenuItem ("Window/Race Track Generator")]
	static void Init () {


        RTG window = (RTG)EditorWindow.GetWindow (typeof (RTG),false, "RTG 1.3.5", true);
		window.Show();

	}



    void GenerateNewTrack(int _size)
    {
        if (FindObjectOfType<Track_Config>())
        {
            GameObject atualTrack = FindObjectOfType<Track_Config>().gameObject;
            Selection.activeObject = atualTrack;
        }

        DestroyImmediate(GameObject.Find("RTG Track"));

        if (FindObjectOfType<Track_Config>())
        {

            if (EditorUtility.DisplayDialog("Warning", "There is already a RTG-Track on the scene\n\nOverwrite?", "Overwrite", "Do Not Overwrite"))
                DestroyImmediate(GameObject.FindObjectOfType<Track_Config>().gameObject);
            else
                return;

        }

        if (trackGenerator.GetComponent<Track_Generator>().CreateTrack(enableAudience, soundAudience, _size))
        {
     

        }

    }

    void OnGUI () {

  
        if (!trackGenerator) {
			trackGenerator = AssetDatabase.LoadAssetAtPath("Assets/RTG 1.3/TrackGenerator.prefab", (typeof(GameObject))) as GameObject;
		}


		GUILayout.Space (15);

        GUILayout.BeginHorizontal("box");

        GUILayout.Label(new GUIContent("GENERATE\nTRACK", "Make a new random race track"));

        GUILayout.Space(10);

        if (GUILayout.Button("Small", GUILayout.Width(80), GUILayout.Height(40)))
            GenerateNewTrack(1);

        GUILayout.Space(5);

        if (GUILayout.Button("Large", GUILayout.Width(80), GUILayout.Height(40)))
            GenerateNewTrack(2);

        GUILayout.Space(5);





        GUILayout.EndHorizontal();





        if (GUILayout.Button("Clear Track", GUILayout.Height(25))) {

                trackGenerator.GetComponent<Track_Generator>().Destroytrack();

        }

        GUILayout.Space(15);


        if (GUILayout.Button("Change Selected Section"))
        {

            if (!GameObject.FindObjectOfType<Track_Config>())
            {
                EditorUtility.DisplayDialog("Warning", "There is no race track on the scene", "OK");
                return;
            }

            GameObject selectedObject = Selection.activeGameObject;
            if (selectedObject) {

                GameObject nextSelected;
                nextSelected = trackGenerator.GetComponent<Track_Generator>().ChangeSection(selectedObject, false);
                if (nextSelected)
                    Selection.activeObject = nextSelected;
                
            }
            else
              EditorUtility.DisplayDialog("Warning", "First select the region of the race track you want to change", "OK"); 
          


        }

        if (GUILayout.Button("Change two Sections")) {

            if (!GameObject.FindObjectOfType<Track_Config>())
            {
                EditorUtility.DisplayDialog("Warning", "There is no race track on the scene", "OK");
                return;
            }

            GameObject selectedObject = Selection.activeGameObject;
            if (selectedObject) {

                GameObject nextSelected;
                nextSelected = trackGenerator.GetComponent<Track_Generator>().ChangeSection(selectedObject, true);
                if (nextSelected)
                    Selection.activeObject = nextSelected;

            } else
                EditorUtility.DisplayDialog("Warning", "First select the region of the race track you want to change", "OK");



        }

        GUILayout.Space(15);


        if (GUILayout.Button("Change Background"))
        {
            trackGenerator.GetComponent<Track_Generator>().ChangeBackGround(null);
        }


        GUILayout.Space(10);
  



        GUILayout.Space(10);


        EditorGUILayout.LabelField(new GUIContent("Change materials:", "change materials in the current race track"));


        GUILayout.BeginHorizontal("box");

        if (GUILayout.Button("<", GUILayout.Width(20), GUILayout.Height(40)))
            trackGenerator.GetComponent<Track_Generator>().ChangeTextureGrass(-1, null, -1);
        GUILayout.Label(new GUIContent("\nGrass"), GUILayout.Width(40), GUILayout.Height(40));
        if (GUILayout.Button(">", GUILayout.Width(20), GUILayout.Height(40)))
            trackGenerator.GetComponent<Track_Generator>().ChangeTextureGrass(1, null, -1);

        GUILayout.Space(20);

        if (GUILayout.Button("<", GUILayout.Width(20), GUILayout.Height(40)))
            trackGenerator.GetComponent<Track_Generator>().ChangeTextureRoad(-1, null, -1);
        GUILayout.Label("\nRoad", GUILayout.Width(40), GUILayout.Height(40));
        if (GUILayout.Button(">", GUILayout.Width(20), GUILayout.Height(40)))
            trackGenerator.GetComponent<Track_Generator>().ChangeTextureRoad(1, null, -1);

        GUILayout.Space(20);

        if (GUILayout.Button("<", GUILayout.Width(20), GUILayout.Height(40)))
            trackGenerator.GetComponent<Track_Generator>().ChangeTextureFenceBase(-1, null, -1);

        //GUILayout.Label(new GUIContent(trackGenerator.GetComponent<TrackGenerator>().texFence), GUILayout.Width(40), GUILayout.Height(40));
        GUILayout.Label("\nFence", GUILayout.Width(40), GUILayout.Height(40));
        if (GUILayout.Button(">", GUILayout.Width(20), GUILayout.Height(40)))
            trackGenerator.GetComponent<Track_Generator>().ChangeTextureFenceBase(1, null, -1);


        GUILayout.EndHorizontal();


        GUILayout.Space(15);

            EditorGUILayout.BeginHorizontal();

           
            enableAudience = GUILayout.Toggle(enableAudience, "Enable Audience in GrandStands", GUILayout.Width(220));

            if (oldEnableAudience != enableAudience)
            {
                oldEnableAudience = enableAudience;
                trackGenerator.GetComponent<Track_Generator>().AddAudience(enableAudience);

            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();

            if (enableAudience)
            {

             
                soundAudience = GUILayout.Toggle(soundAudience, "Audience Sound", GUILayout.Width(220));

                if (oldSoundAudience != soundAudience)
                {
                    oldSoundAudience = soundAudience;
                    trackGenerator.GetComponent<Track_Generator>().AddSoundAudience(soundAudience);
                }

            }
            else
            {

                //soundAudience = false;
                oldSoundAudience = !soundAudience;
                trackGenerator.GetComponent<Track_Generator>().AddSoundAudience(false);

            }

            EditorGUILayout.EndHorizontal();



        GameObject _objs = GameObject.Find("Objects");
        if (_objs)
        {
                       
            GUILayout.Space(10);
            
            GUILayout.BeginVertical("box");


            if (GUILayout.Button("Combine Meshes"))
            {

                if(!tContainer)
                SetContainer();

                string path = "Track";

                if (save)
                {
                    string guid;

                    if (!AssetDatabase.IsValidFolder("Assets/Mytracks"))
                        guid = AssetDatabase.CreateFolder("Assets", "Mytracks");

                    guid = AssetDatabase.CreateFolder("Assets/Mytracks", path);
                    path = AssetDatabase.GUIDToAssetPath(guid);

                    guid = AssetDatabase.CreateFolder(path, "Combined Meshes");
                }


                int t = trackConfig.GetModuleLength();  //modulo.Length;
                GameObject module;
                float tt;

                for (int i = 0; i < t; i++)
                {

                    tt = t; 
                    float f = i / tt;

                    EditorUtility.DisplayProgressBar("Combining meshes", "Please wait",  f);


                    module = trackConfig.GetModule(i);
                    Transform tobjs = module.transform.Find("Objects"); //modulo[i].transform.Find("Objects");

                    if (tobjs)
                    {


                        GameObject newObjects = new GameObject("Combined meshes");
                        newObjects.transform.parent = module.transform;
                        newObjects.transform.localPosition = Vector3.zero;
                        newObjects.transform.localRotation = Quaternion.identity;


                        CombineMeshes(tobjs.gameObject, newObjects, path, i, save);

                    }



                }

                EditorUtility.ClearProgressBar();

                if (save) { 
                    

                    GameObject trackSave = trackGenerator.GetComponent<Track_Generator>().GetContainer();

                    //Object prefab = PrefabUtility.CreatePrefab(path + "/RTG Track.prefab", trackSave);


#if UNITY_2017
                    PrefabUtility.CreatePrefab(path + "/RTG Track.prefab", trackSave);
#else
                    PrefabUtility.SaveAsPrefabAsset(trackSave, path + "/RTG Track.prefab");
#endif

                    Debug.Log("The Track as saved as " + path + "/RTG Track.prefab");
                    EditorUtility.DisplayDialog("Warning", "The Track as saved as \n" + path + "/RTG Track.prefab", "OK");

                } else {
                    EditorUtility.DisplayDialog("Warning", "Mesh Combine is done", "OK");
                }



            }


            save = GUILayout.Toggle(save, "Save prefab with Combined Meshes", GUILayout.Width(240));
            
   
            generateLightmapUVs = GUILayout.Toggle(generateLightmapUVs, "Generate Lightmap UVs", GUILayout.Width(240));

            GUILayout.EndVertical();

        }







    }



    void OnInspectorUpdate()
    {
        Repaint();
    }

    private List<GameObject> newObjects = new List<GameObject>();


    public void CombineMeshes(GameObject objs, GameObject _Objects, string path, int idx, bool save)
    {



        // Preserve Cloths
        Component[] temp = objs.GetComponentsInChildren(typeof(Cloth));
        foreach (Cloth currentChild in temp)
        {
            currentChild.gameObject.transform.parent = _Objects.transform;
            //currentChild.gameObject.isStatic = false;
        }

        // Preserve Audience AudioSource
        Component[] temp2 = objs.GetComponentsInChildren(typeof(AudioSource));
        foreach (AudioSource currentChild in temp2)
        {
            currentChild.gameObject.transform.parent = _Objects.transform;
        }

        //Preserve BoxCollider components
        temp = objs.GetComponentsInChildren(typeof(BoxCollider));
        foreach (BoxCollider currentChild in temp)
        {

            GameObject bc = new GameObject("BoxCollider");
            bc.transform.position = currentChild.transform.position;
            bc.transform.rotation = currentChild.transform.rotation;
            bc.transform.parent = _Objects.transform;

            UnityEditorInternal.ComponentUtility.CopyComponent(currentChild);
            UnityEditorInternal.ComponentUtility.PasteComponentAsNew(bc);

        }

        //Preserve MeshCollider components
        temp = objs.GetComponentsInChildren(typeof(MeshCollider));
        foreach (MeshCollider currentChild in temp)
        {

            GameObject bc = new GameObject("MeshCollider");
            bc.transform.position = currentChild.transform.position;
            bc.transform.rotation = currentChild.transform.rotation;
            bc.transform.parent = _Objects.transform;

            UnityEditorInternal.ComponentUtility.CopyComponent(currentChild);
            UnityEditorInternal.ComponentUtility.PasteComponentAsNew(bc);

        }



        newObjects.Clear();

        Combine2(objs, _Objects, path, idx, save);

    }



    private void Combine2(GameObject _objs, GameObject _Objects, string path, int idx, bool save)
    {

        

        GameObject oldGameObjects = _objs;

        Component[] filters = GetMeshFilters(_objs);

        Matrix4x4 myTransform = _objs.transform.worldToLocalMatrix;
        Hashtable materialToMesh = new Hashtable();

        for (int i = 0; i < filters.Length; i++)
        {

            MeshFilter filter = (MeshFilter)filters[i];
            Renderer curRenderer = filters[i].GetComponent<Renderer>();
            Mesh_CombineUtility.MeshInstance instance = new Mesh_CombineUtility.MeshInstance();
            instance.mesh = filter.sharedMesh;
            if (curRenderer != null && curRenderer.enabled && instance.mesh != null)
            {
                instance.transform = myTransform * filter.transform.localToWorldMatrix;

                Material[] materials = curRenderer.sharedMaterials;
                for (int m = 0; m < materials.Length; m++)
                {


                    instance.subMeshIndex = System.Math.Min(m, instance.mesh.subMeshCount - 1);

                    ArrayList objects = (ArrayList)materialToMesh[materials[m]];
                    if (objects != null)
                        objects.Add(instance);
                    else
                    {
                        objects = new ArrayList();
                        objects.Add(instance);
                        materialToMesh.Add(materials[m], objects);
                    }


                }
            }
        }


        UnityEditor.SerializedObject Sob = new UnityEditor.SerializedObject(GameObject.Find("Trees").GetComponent<Renderer>());
        UnityEditor.SerializedProperty Sprop = Sob.FindProperty("m_LightmapParameters");

        //string newName = "ObjectNew";

        foreach (DictionaryEntry mtm in materialToMesh)
        {
            ArrayList elements = (ArrayList)mtm.Value;
                        
            Mesh_CombineUtility.MeshInstance[] instances = (Mesh_CombineUtility.MeshInstance[])elements.ToArray(typeof(Mesh_CombineUtility.MeshInstance));

            Material mat = (Material)mtm.Key;

            GameObject go = new GameObject(mat.name);

            go.transform.localScale = Vector3.one;
            go.transform.localPosition = Vector3.zero;
            go.transform.position = Vector3.zero;

            go.AddComponent(typeof(MeshFilter));
            go.AddComponent<MeshRenderer>();
            go.GetComponent<Renderer>().material = (Material)mtm.Key;

            if (mat.name.Equals("Atlas-02") || mat.name.Equals("Audience"))
            {


                UnityEditor.SerializedObject Sob2 = new UnityEditor.SerializedObject(go.GetComponent<Renderer>());
                UnityEditor.SerializedProperty Sprop2 = Sob2.FindProperty("m_LightmapParameters");
                Sob2.FindProperty("m_ScaleInLightmap").floatValue = 0.5f;
                Sprop2.objectReferenceInstanceIDValue = Sprop.objectReferenceInstanceIDValue;
                Sob2.ApplyModifiedProperties();
            }

            MeshFilter filter = (MeshFilter)go.GetComponent(typeof(MeshFilter));
            filter.sharedMesh = Mesh_CombineUtility.Combine(instances, false);

            newObjects.Add(go);

        }

        if (newObjects.Count < 1)
        {
            return;
        }



        //GameObject objs = new GameObject("Objects");




        DestroyImmediate(oldGameObjects);


        if (newObjects.Count > 0)
        {
            for (int x = 0; x < newObjects.Count; x++)
            {
       

                newObjects[x].transform.parent = _Objects.transform;
                newObjects[x].transform.localPosition = Vector3.zero;
                newObjects[x].transform.localRotation = Quaternion.identity;

                //newObjects[x].isStatic = true;

                // Generate Lightmap UVs ?
                if (generateLightmapUVs)
                {
                   Unwrapping.GenerateSecondaryUVSet(newObjects[x].GetComponent<MeshFilter>().sharedMesh);
                }

                if (save)
                {
                
                    AssetDatabase.CreateAsset(newObjects[x].GetComponent<MeshFilter>().sharedMesh, path + "/Combined Meshes/newObjects-" + idx + "-" + x + ".asset");
                    AssetDatabase.SaveAssets();

                    GameObject meshToSave = newObjects[x];
                    meshToSave.GetComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath(path + "/Combined Meshes/newObjects-" + idx + "-" + x + ".asset", (typeof(Mesh))) as Mesh;
                }

            }
        }


        Transform aud = _Objects.transform.Find("Audience");
        if (aud)
        {
            Component[] temp2 = _Objects.GetComponentsInChildren(typeof(AudioSource));
            foreach (AudioSource currentChild in temp2)
            {
                currentChild.gameObject.transform.parent = aud;
            }
        }


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
            r = true;

        return r;
    }

    private Component[] GetMeshFilters(GameObject objs)
    {
        List<Component> filters = new List<Component>();
        Component[] temp = null;


        temp = objs.GetComponentsInChildren(typeof(MeshFilter));
        for (int y = 0; y < temp.Length; y++)
            filters.Add(temp[y]);




        return filters.ToArray();

    }





}