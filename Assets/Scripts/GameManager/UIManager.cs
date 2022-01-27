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
    public GameObject[] btns;
    public Text[] btnsText;

    [Header("Resource")]
    public GameObject playerAllObjectPanel;
    public GameObject[] resourcesImage;
    public int[] countOfResources;
    public Text[] resourcesCountText;
    public GameObject[] resourcesDescriptionPanel;

    // Start is called before the first frame update
    void Start()
    {
        countOfResources = new int[resourcesImage.Length];
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab)){
            playerAllObjectPanel.SetActive(true);
            Cursor.visible = true;
        }
        if(Input.GetKeyUp(KeyCode.Tab)){
            playerAllObjectPanel.SetActive(false);
            Cursor.visible = false;

            for (int i = 0; i < resourcesDescriptionPanel.Length; i++)
            {
                resourcesDescriptionPanel[i].SetActive(false);
            }
        }
    }

    public void DisplayInteractionPanel(Interaction interaction, GameObject characterObject, int characterIndex, int[] missions)
    {
        interactionPanel.SetActive(true);
        currentInteractionManager = interaction;
        
        //UI Settings
        if(characterIndex == 0){
            
        }else if(characterIndex == 1){
            Human human = characterObject.GetComponent<Human>();
            nameText.text = $"<color=#{human.ethnicityColor}>{human.characterName}<size=40> (人類)</size></color> - <color=#{human.occupationColor}>{human.characterOccupation}</color>";
        }else if(characterIndex == 2){
            Orc orc = characterObject.GetComponent<Orc>();
            nameText.text = $"<color=#{orc.ethnicityColor}>{orc.characterName}<size=40> (獸人)</size></color> - <color=#{orc.occupationColor}>{orc.characterOccupation}</color>";
        }else if(characterIndex == 3){
            Dwarf dwarf = characterObject.GetComponent<Dwarf>();
            nameText.text = $"<color=#{dwarf.ethnicityColor}>{dwarf.characterName}<size=40> (矮人)</size></color> - <color=#{dwarf.occupationColor}>{dwarf.characterOccupation}</color>";
        }

        //btns
        int missionsLength = missions.Length;
        for (int i = 0; i < btns.Length; i++)
        {
            if(i < missionsLength){
                btns[i].SetActive(true);
                btnsText[i].text = EthnicityManager.instance.GetMissionBtnName(characterIndex, missions[i]);
            }else{
                btns[i].SetActive(false);
            }
        }
    }

    public void InteractionBtn(int btnIndex)
    {
        
    }

    public void CancelInteractionBtn()
    {
        interactionPanel.SetActive(false);
        GameManager.instance.localPlayerController.CancelInteraction();
        currentInteractionManager = null;
    }

    public void GetEquipement(int equipementIndex, int count)
    {
        countOfResources[equipementIndex] += count;
        resourcesCountText[equipementIndex].text = $"x{countOfResources[equipementIndex]}";
        resourcesImage[equipementIndex].SetActive(true);
    }

    public void UseEquipement(int equipementIndex, int count)
    {
        countOfResources[equipementIndex] -= count;
        resourcesCountText[equipementIndex].text = $"x{countOfResources[equipementIndex]}";
        if(countOfResources[equipementIndex] <= 0){
            resourcesImage[equipementIndex].SetActive(false);
        }
    }

    public void DisplayEquipementDescriptionPanel(int index, bool isShow)
    {
        resourcesDescriptionPanel[index].SetActive(isShow);
    }
}
