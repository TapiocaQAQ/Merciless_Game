using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit_AI : MonoBehaviour
{
	public CharacterController characterController;
  public GameObject GFX;
  public Animator animator;
  public new SpriteRenderer renderer;
	public Vector3 fallSpeed;
  public float moveSpeed;
  public Quaternion rotation;
  public Vector3 facingDirection = new Vector3(1,0,1);
  public float relative_facing_angle;

  public enum AIstate{
    idle, moveTowardPlayer, moveRandomly
  }
  public AIstate action = AIstate.idle;
  public float movetime;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
      // disable any faraway rabbits
      GFX.SetActive(Physics.CheckSphere(transform.position, 30, LayerMask.GetMask("Player")));
      if(!GFX.activeInHierarchy){
        return;
      }

      // change the animation according to player pos
      fallSpeed.y += Config.gravity * Time.deltaTime;
      characterController.Move(fallSpeed * Time.deltaTime);
      rotation = transform.rotation;
      Vector3 towardPlayer = Config.player.transform.position - transform.position;
      float cam_angle = (float)((Mathf.Atan2(towardPlayer.x, towardPlayer.z) / Mathf.PI) * 180f);
      if(cam_angle < 0) cam_angle += 360f;
      float facing_angle = (float)((Mathf.Atan2(facingDirection.x, facingDirection.z) / Mathf.PI) * 180f);
      if(facing_angle < 0) facing_angle += 360f;
      relative_facing_angle = cam_angle - facing_angle;
      if(relative_facing_angle < 0) relative_facing_angle += 360f;

      animator.SetBool("Front_looking", relative_facing_angle < 90 || relative_facing_angle >= 270);
      renderer.flipX = (relative_facing_angle > 90 && relative_facing_angle <= 180)||(relative_facing_angle > 270 && relative_facing_angle <= 358);

      // move rabbit
      switch(action){
        case AIstate.idle:
          movetime -= Time.deltaTime;
          if(movetime <= 0){
            action = AIstate.moveRandomly;
            facingDirection = new Vector3(Random.Range(-1,1), 0, Random.Range(-1,1)).normalized;
            movetime = Random.Range(0.5f, 3);
          }
          break;
        case AIstate.moveRandomly:
          movetime -= Time.deltaTime;
          characterController.Move(facingDirection.normalized * moveSpeed * Time.deltaTime);
          if(movetime <= 0){
            action = AIstate.idle;
            movetime = Random.Range(0.5f, 5);
          }
        break;
        case AIstate.moveTowardPlayer:
          characterController.Move(facingDirection.normalized * moveSpeed * Time.deltaTime);
          facingDirection = new Vector3(towardPlayer.x, 0, towardPlayer.z).normalized;
        break;
      }

    }
}
