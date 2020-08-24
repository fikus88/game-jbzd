using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    public Vector3 offset;

    //Those are not needed for now i guess - Tails   
    //private float pitch = 2f;
    //private float currentZoom = 10f;

    private void Start()
    {
        //Creating offset, substracting self position and target position
        //this will be a vector we can use to make early scroll 
        //just multiply by 1.1 or 0.9 and clamp the values
        // ALSO! this will let us move camera anywhere in the editor
        // without any need to change this script not in playmode ofc
        // Tails
        offset = transform.position - target.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {   
        UpdateCameraPosition();
    }

    void UpdateCameraPosition()
    {
        // Setting the camera to character position + offset made at start to maitain camera position
        transform.position = target.transform.position + offset;
        
    }
}
