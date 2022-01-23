using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanManager : MonoBehaviour
{
    public GameObject human;

    // Start is called before the first frame update
    void Start()
    {
        GenerateCharactor(5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateCharactor(int quantity)
    {
        EthnicityManager.instance.ethnicities[0].population += quantity;

        for (int i = 0; i < quantity; i++)
        {
            Instantiate(human, new Vector3(0, 10, i * 10), Quaternion.identity);
        }
    }

}
