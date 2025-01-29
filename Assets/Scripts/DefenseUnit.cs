using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseUnit : MonoBehaviour
{
    public float Health;

    public void CheckDead()
    {
        if (Health < 0)
        {
            //Trigger death
        }
    }
}
