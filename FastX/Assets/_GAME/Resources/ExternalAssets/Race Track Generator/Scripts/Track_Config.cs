using UnityEngine;
using System.Collections;
using System.Linq;


public class Track_Config : MonoBehaviour
{


    //[HideInInspector]
    public GameObject[] modulo;
    [HideInInspector]
    public int[] _tipo;
    [HideInInspector]
    public int[] _begin;
    [HideInInspector]
    public int[] _end;
    [HideInInspector]
    public int[] _idx;          // Index in inspector


    [HideInInspector]
    public int indexMaterialGrass = 0;

    [HideInInspector]
    public int indexMaterialRoad = 0;

    [HideInInspector]
    public int indexMaterialFences = 0;

    
    public void Newlist(int i)
    {
        modulo = new GameObject[i];
        _tipo = new int[i];
        _begin = new int[i];
        _end = new int[i];
        _idx = new int[i];
    }



    private bool mobile;

    public void AddConfig(int index, int tipo, int begin, int end, int idx)
    {

        _tipo[index] = tipo;
        _begin[index] = begin;
        _end[index] = end;
        _idx[index] = idx;

    }

    void Awake()
    {


        // ------------------------------------------------------------------------------------------------------------------
        // Objects that, for optimization, should not exist in mobile devices.
        // Put these objects in a new GameObjectEmpty named "DestroyIfMobile"
        // ------------------------------------------------------------------------------------------------------------------

        mobile = ((Application.platform == RuntimePlatform.IPhonePlayer || (Application.platform == RuntimePlatform.Android)));

        if (mobile)
        {

            GameObject[] disabledOnMobile = GameObject.FindObjectsOfType(typeof(GameObject)).Select(g => g as GameObject).Where(g => g.name.Equals("AutoDisabledOnMobile")).ToArray();
            int n = disabledOnMobile.Length;
            if (n > 0)
                for (int i = 0; i < n; i++)
                    Destroy(disabledOnMobile[i]);

            disabledOnMobile = null;
        }


    }


    public GameObject GetModule(int i)
    {
        return modulo[i];
    }
    public int GetModuleLength()
    {
        return modulo.Length;
    }

   




}
