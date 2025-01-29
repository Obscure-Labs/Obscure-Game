using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotMovementController : MonoBehaviour
{
    public GameObject DefenceUnit;
    public GameObject Player;

    void Update()
    {
        transform.rotation = new Quaternion(0, 0, 0, 0);
        if (Vector2.Distance(transform.position, DefenceUnit.transform.position) >
            Vector2.Distance(transform.position, Player.transform.position))
        {
            GetComponent<NavMeshAgent>().SetDestination(Player.transform.position);
        }
        else
        {
            GetComponent<NavMeshAgent>().SetDestination(DefenceUnit.transform.position); 
        }
    }
}
