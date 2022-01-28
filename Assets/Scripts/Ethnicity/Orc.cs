using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Orc : MonoBehaviour
{
    public TextMeshProUGUI nameText;

    [Header("GFX Optimization")]
    public GameObject GFX;
    public GameObject nameTextUI;
    public float GFXcheckPlayerDst = 100;
    public float nameUICheckDst = 40;
    public LayerMask playerLayer;
    
    [Header("Attributes")]
    public string characterName;
    public string ethnicityColor;
    public string characterOccupation;
    public string occupationColor;
    float health;
    float currentEndurance;
    float enduranceConsume = 0.1f;
    float maxEndurance = 600f;
    public int[] resources;

    int currentActionIndex;
    public int[] missionsIndex;
    bool inOwnerTerritory;
    int occupationIndex;

    public void Initialize(string _characterName)
    {
        resources = new int[ResourceManager.instance.GetResourcesLength];
        characterName = _characterName;
        nameText.text = $"<color=#{ethnicityColor}>{characterName}</color>\n <color=#505050>無職業</color>";
    }

    void Update()
    {
        CheckShowGFX();
        nameTextUI.transform.LookAt(transform.position + GameManager.instance.localPlayerCam.forward);
        Activity();
    }

    void CheckShowGFX()
    {
        bool isGFXActive = Physics.CheckSphere(transform.position, GFXcheckPlayerDst, playerLayer);
        GFX.SetActive(isGFXActive);
        bool isNameUIActive = Physics.CheckSphere(transform.position, nameUICheckDst, playerLayer);
        nameTextUI.SetActive(isNameUIActive);
    }

    void Activity()
    {
        if(!inOwnerTerritory){
            if(currentEndurance <= 0){
                //back to territory

            }else if(currentEndurance >= maxEndurance){
                //execute action
                
            }else{
                currentEndurance -= enduranceConsume * Time.deltaTime;
            }
        }else{
            currentEndurance += enduranceConsume * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("OrcTerritory")){
            inOwnerTerritory = true;

        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("OrcTerritory")){
            inOwnerTerritory = false;
        }
    }

    public void SetOccupation(string _characterOccupation, string _occupationColor)
    {
        characterOccupation = _characterOccupation;
        occupationColor = _occupationColor;
        nameText.text = $"<color=#{ethnicityColor}>{characterName}</color>\n <color=#{_occupationColor}>{characterOccupation}</color>";
    }

    public int[] GetMissions()
    {
        //stop action

        return missionsIndex;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0){
            ResourceManager.instance.InstantiateResources(transform.position, resources);
            Destroy(gameObject);
        }
    }
}
