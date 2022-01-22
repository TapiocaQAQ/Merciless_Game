using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit_AI : MonoBehaviour
{
	public Vector3 fallSpeed;
	public CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		fallSpeed.y += Config.gravity * Time.deltaTime;
		characterController.Move(fallSpeed * Time.deltaTime);
    }
}
