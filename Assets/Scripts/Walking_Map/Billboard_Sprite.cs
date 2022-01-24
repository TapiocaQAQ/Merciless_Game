using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard_Sprite : MonoBehaviour
{
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = Config.player;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0f, player.transform.rotation.eulerAngles.y, 0f);
    }
}
