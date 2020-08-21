using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private InputHandler inputHandler;  //reference to input handler
    [SerializeField]
    private Interactable playerFocus;   //reference to interactables

    private Camera playerCamera;    //reference to player camera

    [SerializeField]
    private float playerSpeed;  //player speed
    [SerializeField]
    private float rotateSpeed;  //player rotate speed (frames per second you can rotate)
    [SerializeField]
    private bool RotateTowardMouse; //you can either move wsad and rotate that way or rotate towards mouse


    // Start is called before the first frame update
    void Awake()
    {
        inputHandler = GetComponent<InputHandler>();
        playerCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        var targetVector = new Vector3(inputHandler.InputVector.x, 0, inputHandler.InputVector.y).normalized;   //player direction
        var movementVector = MoveTowardTarget(targetVector);

        //move to darection aiming
        if (!RotateTowardMouse)
        {
            RotateTowardMovementVector(movementVector);  //good luck working on that l o l
        }
        //rotate to direction traveling

        if (RotateTowardMouse)
        {
            RotateFromMouseVector();
        }

        if (Input.GetMouseButtonDown(1)) //right click for interaction
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;


            if (Physics.Raycast(ray, out hit)) //, 100, movementMask))
            {
                Debug.Log("Hitted " + hit.collider.name);

                //check if hit an interactable object and set focus on it
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    SetFocus(interactable);
                }
            }
        }
    }

    private void RotateTowardMovementVector(Vector3 movementDirection)
    {
        if (movementDirection.magnitude == 0) { return; }
        var rotation = Quaternion.LookRotation(movementDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotateSpeed);
    }


    private void RotateFromMouseVector()
    {
        Ray ray = playerCamera.ScreenPointToRay(inputHandler.MousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance: 300f))
        {
            var target = hitInfo.point;
            target.y = transform.position.y;
            transform.LookAt(target);
        }
    }

    private Vector3 MoveTowardTarget(Vector3 targetVector)
    {
        var speed = playerSpeed * Time.deltaTime;
        targetVector = Quaternion.Euler(0, playerCamera.gameObject.transform.rotation.eulerAngles.y, 0) * targetVector; //forward for us is what is forward for the camera
        var targetPosition = transform.position + targetVector * speed;
        transform.position = targetPosition;
        return targetVector;
    }

    private void SetFocus(Interactable newFocus)    //sets focus on the object
    {
        if (newFocus != playerFocus)
        {
            if (playerFocus != null)
            {
                playerFocus.OnDefocused();
            }

            playerFocus = newFocus;
        }

        newFocus.OnFocused(transform);
    }

    private void RemoveFocus(Interactable newFocus) //removes object focus (useless for now, saved for later)
    {
        if (playerFocus != null)
        {
            playerFocus.OnDefocused();
        }

        playerFocus.OnDefocused();
        playerFocus = null;
    }
}
