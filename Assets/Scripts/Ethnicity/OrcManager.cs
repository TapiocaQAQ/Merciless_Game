using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcManager : MonoBehaviour
{
    #region Singleton

    public static OrcManager instance;

    private void Awake() {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(this);
        }else{
            OrcManager.instance.Destroy();
            instance = this;
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    #endregion

    public GameObject orc;
    public Ethnicity ethnicity;

    // Start is called before the first frame update
    void Start()
    {
        GenerateCharactor(5);
        ethnicity = EthnicityManager.instance.ethnicities[2];
        ethnicity.population = 5;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateCharactor(int quantity)
    {
        EthnicityManager.instance.ethnicities[2].population += quantity;

        for (int i = 0; i < quantity; i++)
        {
            Instantiate(orc, new Vector3(4, 5, i * 10), Quaternion.identity, transform);
        }
    }
}