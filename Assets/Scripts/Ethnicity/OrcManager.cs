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

    public int[] actionExcutingList;//check action has how many character are excuting

    int[] occupationsPopulation;//store how many people in this occupations 

    string[] characterName = {
        "Job",
        "Thresh",
    };

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        GenerateCharactor(5);
    }

    void Initialize()
    {
        ethnicity = EthnicityManager.instance.ethnicities[2];
        ethnicity.population = 5;
    }

    void Update()
    {
        
    }

    void GenerateCharactor(int quantity)
    {
        EthnicityManager.instance.ethnicities[2].population += quantity;

        for (int i = 0; i < quantity; i++)
        {
            GameObject g = (GameObject)Instantiate(orc, new Vector3(4, 5, i * 10), Quaternion.identity, transform);
            g.GetComponent<Orc>().Initialize(SetCharacterName);
        }
    }

    #region Mission

    public string GetMissionBtnName(int missionindex)
    {
        if(missionindex == 0){
            return "任務1";
        }else if(missionindex == 1){
            return "任務2";
        }else if(missionindex == 2){
            return "任務3";
        }else if(missionindex == 3){
            return "任務4";
        }

        Debug.Log("There have some problem");
        return null;
    }

    public void ExecuterMission(int missionindex)
    {
        //check player whether arrive mission requirement
        if(missionindex == 0){
            
        }else if(missionindex == 1){
            
        }else if(missionindex == 2){
            
        }else if(missionindex == 3){
            
        }
    }

    #endregion

    #region Action

    // <summary>distribute NPC action</summary>
    public byte GetCharacterActionIndex()
    {
        if(ethnicity.hunger <= 30 && actionExcutingList[1] < 3){
            actionExcutingList[1]++;
            return 1;
        }

        if(ethnicity.hunger <= 50 && actionExcutingList[1] < 1){
            actionExcutingList[1]++;
            return 1;
        }
        return 0;
    }

    #endregion

    public string SetCharacterName
    {
        get{
            int nameIndex = Random.Range(0, characterName.Length);
            return characterName[nameIndex];
        }
    }
    
}