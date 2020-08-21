using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Security.Cryptography;
using UnityEngine;

public class DoomerController : MonoBehaviour
{
    [SerializeField]
    public Transform mainCharacterTransform;

    [SerializeField]
    private float movementSpeed=0.1f;

    [SerializeField]
    private float aggroRadius=10.0f;

    [SerializeField]
    private float attackRadius=1.5f;

    private Vector3 spawnPoint;

    private Transform thisEnemyTransform;

    // Start is called before the first frame update
    void Start()
    {
        thisEnemyTransform = GetComponent<Transform>();
        spawnPoint = new Vector3(thisEnemyTransform.position.x,thisEnemyTransform.position.y,thisEnemyTransform.position.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distanceToMainChar = Vector3.Distance(thisEnemyTransform.position, mainCharacterTransform.position);
        if (distanceToMainChar < aggroRadius)// checking if main char is visible for enemy
        {
            UpdateEnemyRotation(new Vector3(mainCharacterTransform.position.x, 0, mainCharacterTransform.position.z));
            if (distanceToMainChar < attackRadius) //checking if main char is in attack range
            {
                //attacking code goeas here
            }
            else
            {
                UpdateEnemyPosition(new Vector3(mainCharacterTransform.position.x - thisEnemyTransform.position.x, 0, mainCharacterTransform.position.z - thisEnemyTransform.position.z));
            }
        } else if (Vector3.Distance(thisEnemyTransform.position, spawnPoint) > 2.0f)
        {
            UpdateEnemyRotation(new Vector3(spawnPoint.x,0,spawnPoint.z));
            UpdateEnemyPosition(new Vector3(spawnPoint.x-thisEnemyTransform.position.x,0,spawnPoint.z-thisEnemyTransform.position.z));
        }
    }

    void UpdateEnemyPosition(Vector3 direction)
    {
        direction = Vector3.Normalize(direction);
        thisEnemyTransform.position += direction * movementSpeed;

    }
    
    void UpdateEnemyRotation(Vector3 direction)
    {
        thisEnemyTransform.LookAt(direction);
    }
}