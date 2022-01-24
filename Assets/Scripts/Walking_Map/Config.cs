using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config : MonoBehaviour
{
	public static float gravity = -9.8f;
    public static float playerSpawnX = 250f;
    public static float playerSpawnZ = 250f;
    public static GameObject player;
    public static GameObject tree01;
    public static GameObject rabbit;
    public GameObject _player;
    public GameObject tree01_prefab;
    public GameObject rabbit_prefab;

    void Start()
    {
        player = _player;
        tree01 = tree01_prefab;
        rabbit = rabbit_prefab;
    }
}
