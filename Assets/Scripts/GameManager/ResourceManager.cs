using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{

    #region Singleton

    public static ResourceManager instance;

    private void Awake() {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(this);
        }else{
            ResourceManager.instance.Destroy();
            instance = this;
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    #endregion

    public GameObject[] resourcesObject;

    public void InstantiateResources(Vector3 pos, int[] resources)
    {
        for (int i = 0; i < GetResourcesLength; i++)
        {
            if(i > 0){
                GameObject g = (GameObject)Instantiate(resourcesObject[i], pos, Quaternion.identity, transform);
                g.GetComponent<PickableObject>().count = resources[i];
            }
        }
    }

    public int GetResourcesLength
    {
        get{
            return resourcesObject.Length;
        }
    }
}
