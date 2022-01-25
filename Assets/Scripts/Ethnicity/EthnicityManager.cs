using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EthnicityManager : MonoBehaviour
{
    #region Singleton

    public static EthnicityManager instance;

    private void Awake() {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(this);
        }else{
            EthnicityManager.instance.Destroy();
            instance = this;
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    #endregion

    /// <Summary>0:player, 1:Human, 2:</Summary>
    public Ethnicity[] ethnicities;
    public GameObject[] allEthnicitiesManagers;
    int countOfAllEthnicities;

    void Start()
    {
        countOfAllEthnicities = ethnicities.Length;
        allEthnicitiesManagers = new GameObject[countOfAllEthnicities];
    }

    void Update()
    {
        
    }

    public void GenerateEthnicity(float numOfEthnicities)
    {
        for (int i = 1; i <= numOfEthnicities; i++)
        {
            GameObject g = Instantiate(ethnicities[i].manager);
            allEthnicitiesManagers[i] = g;
        }
    }
    
    public string GetMissionBtnName(int characterIndex, int missionindex)
    {
        if(characterIndex == 0){
            
        }else if(characterIndex == 1){
            return allEthnicitiesManagers[1].GetComponent<HumanManager>().GetMissionBtnName(missionindex);
        }else if(characterIndex == 2){
            return allEthnicitiesManagers[2].GetComponent<OrcManager>().GetMissionBtnName(missionindex);
        }else if(characterIndex == 3){
            return allEthnicitiesManagers[3].GetComponent<DwarfManager>().GetMissionBtnName(missionindex);
        }

        Debug.Log("There have some problem");
        return null;
    }
}
