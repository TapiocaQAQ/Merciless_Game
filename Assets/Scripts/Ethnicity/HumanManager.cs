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

    public int[] missionExcutingList;//check mission has how many character are excuting

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        GenerateCharactor(5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Initialize()
    {
        ethnicity = EthnicityManager.instance.ethnicities[1];
        ethnicity.population = 5;
    }

    void GenerateCharactor(int quantity)
    {
        EthnicityManager.instance.ethnicities[1].population += quantity;

        for (int i = 0; i < quantity; i++)
        {
            Instantiate(human, new Vector3(0, 5, i * 10), Quaternion.identity, transform);
        }
    }

    public byte GetCharacterActionIndex()
    {
        if(ethnicity.hunger <= 30 && missionExcutingList[1] < 3){
            missionExcutingList[1]++;
            return 1;
        }

        if(ethnicity.hunger <= 50 && missionExcutingList[1] < 1){
            missionExcutingList[1]++;
            return 1;
        }
        return 0;
    }
}
