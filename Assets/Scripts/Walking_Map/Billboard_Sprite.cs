using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard_Sprite : MonoBehaviour
{
    private Camera playerCam;

    // Start is called before the first frame update
    void Start()
    {
        playerCam = Config.playerCam;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0f, playerCam.transform.rotation.eulerAngles.y, 0f);
    }
}
