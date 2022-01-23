using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanManager : MonoBehaviour
{
    #region Singleton

    public static HumanManager instance;

    private void Awake() {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(this);
        }else{
            HumanManager.instance.Destroy();
            instance = this;
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    #endregion

    public GameObject human;
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
        EthnicityManager.instance.ethnicities[0].population += quantity;

        for (int i = 0; i < quantity; i++)
        {
            Instantiate(human, new Vector3(0, 5, i * 10), Quaternion.identity, transform);
        }
    }
}
