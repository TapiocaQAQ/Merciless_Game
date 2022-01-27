using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : MonoBehaviour
{
    public int resourceIndex;
    public int count;

    private void Start() {
        Destroy(gameObject, 30f);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player")){
            UIManager.instance.GetEquipement(resourceIndex, count);
            Destroy(gameObject);
        }
    }
}
