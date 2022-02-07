using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Human : MonoBehaviour
{
    public Rigidbody rigidbody;
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
    float maxHealth;
    float currentHealth = 100;
    float currentEndurance;
    float enduranceConsume = 0.1f;
    float maxEndurance = 600f;
    public int[] resources;
    public float speed = 1f;

    int currentActionIndex;
    public int[] missionsIndex;
    bool inOwnerTerritory;
    int occupationIndex;

    //battle
    bool inBattleStatus;
    public GameObject battleTarget;
    float battleCalmDownTime = 0f;
    float battleDst = 3;

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

        if(Input.GetKeyDown(KeyCode.K)){
            TakeDamage(null, 50);
        }
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
        //Battle
        if(inBattleStatus){
            //attack player
            if(battleTarget != null){
                Vector3 dir = battleTarget.transform.position - transform.position;
                
                if(Vector3.Magnitude(dir) >= battleDst){
                    rigidbody.MovePosition(transform.position + dir * speed * Time.deltaTime);
                }
            }
            
            //CD
            if(battleCalmDownTime <= 0){
                inBattleStatus = false;
                battleTarget = null;
            }else{
                battleCalmDownTime -= Time.deltaTime;
            }
            return;
        }
        currentHealth += Time.deltaTime;
        Mathf.Clamp(currentHealth, 0, maxHealth);

        //Territory
        if(!inOwnerTerritory){
            if(currentEndurance <= 0 || !LightingManager.instance.isWorkingTime){
                //back to territory when endurance not enough or is at night

            }else if(currentEndurance >= maxEndurance){
                //execute action
                Action();
            }else{
                currentEndurance -= enduranceConsume * Time.deltaTime;
            }
        }else{
            currentEndurance += enduranceConsume * Time.deltaTime;
        }
    }

    void Action()
    {
        if(currentActionIndex == 0){

        }else if(currentActionIndex == 1){
            //find food

        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("HumanTerritory")){
            inOwnerTerritory = true;

        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("HumanTerritory")){
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

    public void TakeDamage(GameObject target, int damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0){
            ResourceManager.instance.InstantiateResources(transform.position, resources);
            Destroy(gameObject);
        }else{
            battleCalmDownTime = 10f;
            battleTarget = target;
            inBattleStatus = true;
        }
    }
}