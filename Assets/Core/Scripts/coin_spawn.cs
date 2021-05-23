using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin_spawn : MonoBehaviour
{
    public float turnSpeed = 90f;
    public AudioSource audioSource;
    public AudioClip clip;

    
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    //Check that the object we collided with is the player
    private void OnTriggerEnter(Collider other)
    {
        //
        if (other.gameObject.GetComponent<GameManager>() != null)
        {
            
            Destroy(gameObject);
            
            return;
        }
        
        //check that the object we collided with is the player
        if(other.gameObject.name != "Player")
        {
            return;
        }

        //Add to the player's score
        //GameManager.inst.score++;
        Game.Instance.Coins++;
        audioSource.PlayOneShot(clip, 1f);
        //Destroy this coin object
        Destroy(gameObject);
    }

    private void Update()
    {
        transform.Rotate(0, 0, turnSpeed * Time.deltaTime);
    }

}
