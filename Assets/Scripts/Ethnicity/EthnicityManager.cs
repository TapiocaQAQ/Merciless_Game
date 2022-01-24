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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateEthnicity(float numOfEthnicities)
    {
        for (int i = 1; i <= numOfEthnicities; i++)
        {
            Instantiate(ethnicities[i].manager);
        }
    }

    public string GetEthnicityName(int index)
    {
        return ethnicities[index].ethnicityName;
    }
}
