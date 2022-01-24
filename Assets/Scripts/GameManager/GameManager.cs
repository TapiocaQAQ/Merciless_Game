using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager instance;

    private void Awake() {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(this);
        }else{
            GameManager.instance.Destroy();
            instance = this;
        }
    }

    #endregion

    [Header("LocalPlayer")]
    public Transform localPlayerCam;
    public ThirdPlayerController localPlayerController;

    public int numOfEthnicities;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G)){
            StartWorld();
        }
    }

    public void StartWorld()
    {
        //Initialize
        EthnicityManager.instance.GenerateEthnicity(numOfEthnicities);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
