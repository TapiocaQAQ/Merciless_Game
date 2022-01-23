using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dwarf : MonoBehaviour
{
    [Header("GFX Optimization")]
    bool isGFXActive;
    public GameObject GFX;
    public float checkPlayerDst = 100;
    public LayerMask playerLayer;

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
        isGFXActive = Physics.CheckSphere(transform.position, checkPlayerDst, playerLayer);
        GFX.SetActive(isGFXActive);
    }
}
