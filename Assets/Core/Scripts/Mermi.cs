using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mermi : MonoBehaviour
{
    //TA TA TA DUSMANIN KAFASINA MERMI
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.GetComponent<Dusman>()!=null)
        {
            other.collider.GetComponent<Dusman>().TakeDamage(20);
        }
    }
}
