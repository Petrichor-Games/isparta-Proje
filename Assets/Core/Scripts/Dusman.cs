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

    private GameObject Target;
    
    // Start is called before the first frame update
    void Start()
    {
        anim.GetComponent<Animator>();
        Target = GameObject.Find("Player");
    }

    private void Update()
    {
        if (timeBtwShots <= 0)
        {
            if ((Target.transform.position.x - transform.position.x) < 50)
            {
                var mermi = Instantiate(MermiPrefab, AtesEt.transform.position, Quaternion.identity);
                mermi.GetComponent<Rigidbody>().AddForce(-Target.transform.position * 10f);
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
        
        if (damage>=Health)
        {
            Death();
        }
        Health -= damage;
    }


    void Death()
    {
        Debug.Log("Öldüm çık");
        GameObject.Find("GameManager").GetComponent<GameManager>().score++;
        anim.SetTrigger("Death");
        Destroy(this, 5f);
    }
}
