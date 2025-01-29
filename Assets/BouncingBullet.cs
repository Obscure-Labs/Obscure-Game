using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class BouncingBullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Walls"))
        {
            GetComponent<Rigidbody2D>().velocity = Vector3.Reflect(GetComponent<Rigidbody2D>().velocity, other.transform.rotation.eulerAngles);
        }
    }
}
