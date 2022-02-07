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
    Ethnicity ethnicity;
    public GameObject territory;

    int[] actionExcutingList;//check action has how many character are excuting

    int[] occupationsPopulation;//store how many people in this occupations 
    int[] remainOccupationsPopulation;//store how many people can apply for this occupations 

    string[] characterName = {
        "Steve",
        "Diana",
    };

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        GenerateTerritory(new Vector3(0,0,0), 30);
        GenerateCharactor(5);
    }

    void Initialize()
    {
        ethnicity = EthnicityManager.instance.ethnicities[1];
        ethnicity.population = 5;
    }

    void Update()
    {
        
    }

    void GenerateCharactor(int quantity)
    {
        EthnicityManager.instance.ethnicities[1].population += quantity;

        for (int i = 0; i < quantity; i++)
        {
            GameObject g = (GameObject)Instantiate(human, new Vector3(0, 5, i * 10), Quaternion.identity, transform);
            g.GetComponent<Human>().Initialize(SetCharacterName);
        }
    }

    void GenerateTerritory(Vector3 pos, float radius)
    {
        GameObject g = Instantiate(territory, pos, Quaternion.identity, transform);
        g.transform.localScale = Vector3.one * radius;
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

    public void ExecuteMission(int missionindex)
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

    public int GetOccupation()
    {
        for (int i = 1; i < remainOccupationsPopulation.Length; i++)
        {
            if(remainOccupationsPopulation[i] > 0){
                remainOccupationsPopulation[i]--;
                return i;
            }
        }
        return 0;
    }

    public string SetCharacterName
    {
        get{
            int nameIndex = Random.Range(0, characterName.Length);
            return characterName[nameIndex];
        }
    }

}
