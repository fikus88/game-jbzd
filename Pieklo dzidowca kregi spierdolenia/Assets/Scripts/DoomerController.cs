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

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = new Vector3(this.gameObject.transform.position.x,this.gameObject.transform.position.y,this.gameObject.transform.position.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distanceToMainChar = Vector3.Distance(this.gameObject.transform.position, mainCharacterTransform.position);
        if (distanceToMainChar < aggroRadius)// checking if main char is visible for enemy
        {
            UpdateEnemyRotation(new Vector3(mainCharacterTransform.position.x, 0, mainCharacterTransform.position.z));
            if (distanceToMainChar < attackRadius) //checking if main char is in attack range
            {
                //attacking code goeas here
            }
            else
            {
                UpdateEnemyPosition(new Vector3(mainCharacterTransform.position.x - this.gameObject.transform.position.x, 0, mainCharacterTransform.position.z - this.gameObject.transform.position.z));
            }
        } else if (Vector3.Distance(this.gameObject.transform.position, spawnPoint) > 2.0f)
        {
            UpdateEnemyRotation(new Vector3(spawnPoint.x,0,spawnPoint.z));
            UpdateEnemyPosition(new Vector3(spawnPoint.x-this.gameObject.transform.position.x,0,spawnPoint.z-this.gameObject.transform.position.z));
        }
    }

    void UpdateEnemyPosition(Vector3 direction)
    {
        direction = Vector3.Normalize(direction);
        this.gameObject.transform.position += direction * movementSpeed;

    }
    
    void UpdateEnemyRotation(Vector3 direction)
    {
        this.gameObject.transform.LookAt(direction);
    }
}