using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public int characterIndex;
    bool interactionUI_isActive;
    public GameObject interactionUI;

    bool interacting;
    
    int[] actions;

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

        if(characterIndex == 0){

        }else if(characterIndex == 1){
            actions = GetComponent<Human>().GetActions();
        }

        UIManager.instance.DisplayInteractionPanel(this, GetComponent<Human>().characterName, characterIndex, actions);
    }
}
