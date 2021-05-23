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
