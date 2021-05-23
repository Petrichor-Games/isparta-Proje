using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Dusman : MonoBehaviour
{
    private float Health = 100f;
    public Animator anim;
    public GameObject MermiPrefab;
    public GameObject AtesEt;
    private float timeBtwShots;
    public float startTimeBtwShots;
    private bool oldumMQ =false;
    public GameObject Kendisi;

    private GameObject Target;
    
    // Start is called before the first frame update
    void Start()
    {
        anim.GetComponent<Animator>();
        Target = GameObject.Find("Player");
    }

    private void Update()
    {
        if (timeBtwShots <= 0 && oldumMQ!=true)
        {
            var tests = transform.position.z - Target.transform.position.z;
            
            if (tests < 20)
            {
                var mermi = Instantiate(MermiPrefab, AtesEt.transform.position, Quaternion.identity);
                var test = Target.transform.position;
                test.y += 2;
                mermi.transform.LookAt(test);
                mermi.GetComponent<Rigidbody>().AddForce(mermi.transform.forward * 1900f);
                anim.SetTrigger("shoot");
                Destroy(mermi, 5f);
                timeBtwShots = startTimeBtwShots;
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }


    public void TakeDamage(int damage)
    {
        if (oldumMQ)
        {
            return;
        }
        
        if (damage>=Health)
        {
            Death();
        }
        Health -= damage;
    }


    void Death()
    {
        
        if (!oldumMQ)
        {
            Debug.Log("Öldüm çık");
            oldumMQ = true;
        GameObject.Find("GameManager").GetComponent<GameManager>().score++;
        anim.SetTrigger("Death");
        Destroy(Kendisi, 2f); 
        }
    }
}
