using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config : MonoBehaviour
{
	public static float gravity = -9.8f;
    public static float playerSpawnX = 250f;
    public static float playerSpawnZ = 250f;
    public static Camera playerCam;
    public Camera _playerCam;

    void Start()
    {
        playerCam = _playerCam;
    }
}
