using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPlayerController : MonoBehaviour
{
    [Header("Movement")]
    public CharacterController characterController;
    public Transform cam;
    private float turnSmoothTime = .1f;
    private float turnSmoothVelocity;
    public float speed;
    public float buffSpeed;//norAttack
    public float debuffSpeed;
    public float norSpeed = 50f;
    public float jump;
    public float jumpHeight = 20f;
    public float gravity = -80f;
    public Transform GroundCheck;
    public float GrounCheckDistance = 0.1f;
    public LayerMask groundMask;
    public bool isGrounded;
    Vector3 velocity;
    public bool moveLimit;
    public bool jumpLimit;

    [Header("Intaeration")]
    public Transform interactionCam;
    public Transform interactionCamHolder;
    public float interactionDst = 2f;
    public LayerMask interactiveLayer;
    GameObject currentInteractionTarget;
    [HideInInspector]public bool isInteracting;
    
    void Start() 
    {
        jump = jumpHeight;
    }

    void Update() 
    {
        Movement();
        CharacterInteraction();
    }

    void Movement()
    {
        speed = norSpeed + buffSpeed - debuffSpeed;

        if(buffSpeed >= 0){
            buffSpeed -= 20 * Time.deltaTime;
        }
        if(debuffSpeed >= 0){
            debuffSpeed -= 20 * Time.deltaTime;
        }
        if(jump != jumpHeight){
            jump = jumpHeight;
        }
        
        isGrounded = Physics.CheckSphere(GroundCheck.position , GrounCheckDistance, groundMask);

        if(isGrounded && velocity.y < 0){
            velocity.y = -2f;
        }

        if(!moveLimit){
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = new Vector3(x, 0f, z).normalized;

            if(move.magnitude >= 0.1f){

                float targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                characterController.Move(moveDir.normalized * speed * Time.deltaTime);
            }

        }
        
        if(!jumpLimit){
            if(Input.GetButtonDown("Jump") && isGrounded){
                velocity.y = Mathf.Sqrt(jump * -2f * gravity);
                isGrounded = false;
            }
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    void CharacterInteraction()
    {
        if(isInteracting){
            return;
        }

        //set interactionRay dir
        Vector3 interRayDir = transform.position - cam.position;
        float yRotation = cam.transform.eulerAngles.y;
        interactionCamHolder.rotation = Quaternion.Euler(0, yRotation, 0);
        interactionCam.rotation = Quaternion.Euler(0, yRotation, 0);

        //use raycast to detact target
        RaycastHit hit;
        if(Physics.Raycast(interactionCam.position, interactionCam.forward, out hit, interactionDst, interactiveLayer)){
            if(hit.transform.gameObject != currentInteractionTarget){
                if(currentInteractionTarget != null){
                    currentInteractionTarget.GetComponentInParent<Interaction>().DisplayInteractionUI(false);
                }
                currentInteractionTarget = hit.transform.gameObject;
                currentInteractionTarget.GetComponentInParent<Interaction>().DisplayInteractionUI(true);
            }
        }else{
            if(currentInteractionTarget != null){
                currentInteractionTarget.GetComponentInParent<Interaction>().DisplayInteractionUI(false);
                currentInteractionTarget = null;
            }
        }

        if(currentInteractionTarget == null || isInteracting){
            return;
        }

        if(Input.GetButtonDown("Interaction")){
            currentInteractionTarget.GetComponentInParent<Interaction>().InteractionFunc();
            isInteracting = true;
        }
    }
}
