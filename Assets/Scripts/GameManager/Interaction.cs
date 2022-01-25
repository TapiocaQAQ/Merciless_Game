using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public int characterIndex;
    bool interactionUI_isActive;
    public GameObject interactionUI;

    bool interacting;
    
    int[] missions;

    // Update is called once per frame
    void Update()
    {
        if(interactionUI_isActive)
        {
            interactionUI.transform.LookAt(transform.position + GameManager.instance.localPlayerCam.forward);
        }
    }

    public void DisplayInteractionUI(bool isDisplay)
    {
        interactionUI_isActive = isDisplay;
        interactionUI.SetActive(isDisplay);
    }

    public void InteractionFunc()
    {
        if(interacting){
            return;
        }

        interacting = true;
        interactionUI.SetActive(false);

        if(characterIndex == 0){

        }else if(characterIndex == 1){
            missions = GetComponent<Human>().GetMissions();
            UIManager.instance.DisplayInteractionPanel(this, gameObject, characterIndex, missions);
        }else if(characterIndex == 2){
            missions = GetComponent<Orc>().GetMissions();
            UIManager.instance.DisplayInteractionPanel(this, gameObject, characterIndex, missions);
        }else if(characterIndex == 3){
            missions = GetComponent<Dwarf>().GetMissions();
            UIManager.instance.DisplayInteractionPanel(this, gameObject, characterIndex, missions);
        }
    }

    public void CancelInteraction()
    {
        interactionUI.SetActive(true);
        interacting = false;
    }
}
