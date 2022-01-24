using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    [Header("GFX Optimization")]
    bool isGFXActive;
    public GameObject GFX;
    public float GFXcheckPlayerDst = 100;
    public LayerMask playerLayer;

    [Header("Action")]
    int actionIndex;
    int[] actionsIndex;

    [Header("Attributes")]
    public string characterName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckShowGFX();

        
    }

    void CheckShowGFX()
    {
        isGFXActive = Physics.CheckSphere(transform.position, GFXcheckPlayerDst, playerLayer);
        GFX.SetActive(isGFXActive);
    }

    public int[] GetActions()
    {
        //stop action

        return actionsIndex;
    }
}