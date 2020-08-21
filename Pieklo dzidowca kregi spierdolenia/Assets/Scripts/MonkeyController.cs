using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Security.Cryptography;
using UnityEditor.Rendering;
using UnityEngine;

public class MonkeyController : MonoBehaviour
{
    [SerializeField]
    public Transform mainCharacterTransform;

    [SerializeField]
    private float movementSpeed = 0.14f;

    [SerializeField]
    private float aggroRadius = 20.0f;

    [SerializeField]
    private float attackRadius = 8.5f;

    [SerializeField]
    private float runRadius = 5.0f;

    [SerializeField]
    private float runSpeedDebuff = 0.7f;

    private Vector3 spawnPoint;
    private Transform thisEnemyTransform;
    private float runTimer = 0.0f;
   

    // Start is called before the first frame update
    void Start()
    {
        thisEnemyTransform = GetComponent<Transform>();
        spawnPoint = new Vector3(thisEnemyTransform.position.x, thisEnemyTransform.position.y, thisEnemyTransform.position.z);
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
                if (distanceToMainChar < runRadius)
                {
                    runTimer += Time.deltaTime;
                    if (runTimer < 3.0f)
                    {
                        //move enemy in opposite direction than main char and with speed debuff
                        UpdateEnemyPosition(new Vector3(thisEnemyTransform.position.x - mainCharacterTransform.position.x, 0, thisEnemyTransform.position.z - mainCharacterTransform.position.z));
                    }
                    else if(runTimer < 3.5f)
                    {

                    }
                    else
                    {
                        runTimer = 0.0f;
                    }
                }
                else
                {
                    //attacking code goes here
                }
            }
            else
            {
                //if main char is not in attack range then get closer
                UpdateEnemyPosition(new Vector3(mainCharacterTransform.position.x - thisEnemyTransform.position.x, 0, mainCharacterTransform.position.z - thisEnemyTransform.position.z));
            }
        }
        else if (Vector3.Distance(thisEnemyTransform.position, spawnPoint) > 2.0f) //if main char is not visible and this enemy is far form spawn point then go back to spawn
        {
            UpdateEnemyRotation(new Vector3(spawnPoint.x, 0, spawnPoint.z));
            UpdateEnemyPosition(new Vector3(spawnPoint.x - thisEnemyTransform.position.x, 0, spawnPoint.z - thisEnemyTransform.position.z));
        }
    }

    void UpdateEnemyPosition(Vector3 direction,float speedDebuff)
    {
        direction = Vector3.Normalize(direction);
        thisEnemyTransform.position += direction * movementSpeed * speedDebuff;
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