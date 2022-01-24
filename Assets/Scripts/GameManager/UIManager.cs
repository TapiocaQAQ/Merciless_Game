using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Singleton

    public static UIManager instance;

    private void Awake() {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(this);
        }else{
            UIManager.instance.Destroy();
            instance = this;
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    #endregion

    [Header("Interaction")]
    public GameObject interactionPanel;
    public Text nameText;
    public Text dialogText;
    Interaction currentInteractionManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayInteractionPanel(Interaction interaction, string characterName, int characterIndex, int[] missions)
    {
        interactionPanel.SetActive(true);
        currentInteractionManager = interaction;
        
        //UI Settings
        string ethnicityName = EthnicityManager.instance.GetEthnicityName(characterIndex);
        nameText.text = $"{characterName} <size=40>({ethnicityName})</size>";
        

        //btns
        for (int i = 0; i < missions.Length; i++)
        {
            
        }
    }

    public void InteractionBtn(int btnIndex)
    {

    }

    public void CancelInteractionBtn()
    {
        interactionPanel.SetActive(false);
        GameManager.instance.localPlayerController.isInteracting = false;
        currentInteractionManager = null;
    }
}
