using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Human : MonoBehaviour
{
    public TextMeshProUGUI nameText;

    [Header("GFX Optimization")]
    public GameObject GFX;
    public GameObject nameTextUI;
    public float GFXcheckPlayerDst = 100;
    public float nameUICheckDst = 40;
    public LayerMask playerLayer;

    [Header("Action")]
    int currentActionIndex;
    public int[] missionsIndex;

    [Header("Attributes")]
    public string characterName;
    public string ethnicityColor;
    public string characterOccupation;
    public string occupationColor;
    float health;
    float endurance;

    public void Initialize(string _characterName)
    {
        characterName = _characterName;
        nameText.text = $"<color=#{ethnicityColor}>{characterName}</color>\n <color=#505050>無職業</color>";
    }

    void Update()
    {
        CheckShowGFX();
        nameTextUI.transform.LookAt(transform.position + GameManager.instance.localPlayerCam.forward);
    }

    void CheckShowGFX()
    {
        bool isGFXActive = Physics.CheckSphere(transform.position, GFXcheckPlayerDst, playerLayer);
        GFX.SetActive(isGFXActive);
        bool isNameUIActive = Physics.CheckSphere(transform.position, nameUICheckDst, playerLayer);
        nameTextUI.SetActive(isNameUIActive);
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
}