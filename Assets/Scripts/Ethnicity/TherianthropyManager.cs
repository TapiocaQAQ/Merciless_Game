using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TherianthropyManager : MonoBehaviour
{
    #region Singleton

    public static TherianthropyManager instance;

    private void Awake() {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(this);
        }else{
            TherianthropyManager.instance.Destroy();
            instance = this;
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    #endregion

    public GameObject therianthropy;
    public Ethnicity ethnicity;

    // Start is called before the first frame update
    void Start()
    {
        GenerateCharactor(5);
        ethnicity = EthnicityManager.instance.ethnicities[0];
        ethnicity.population = 5;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateCharactor(int quantity)
    {
        EthnicityManager.instance.ethnicities[1].population += quantity;

        for (int i = 0; i < quantity; i++)
        {
            Instantiate(therianthropy, new Vector3(2, 5, i * 10), Quaternion.identity, transform);
        }
    }
}