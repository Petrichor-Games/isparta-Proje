using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum LANE
{
    Left,
    Mid,
    Right
}

public class PlayerMovement : MonoBehaviour
{
    private LANE e_Lane = LANE.Mid;
    private float Xcordinate = 0f;
    public float Health = 100f;

    private bool SwipeLeft;
    public float Speed = 7f;
    private bool SwipeRight;
    private Animator animator;
    public float xValue;
    private CharacterController cc;
    private GameManager GM;
    public Transform Player;
    private Vector3 desiredPosition;
    private Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        cc = GetComponent<CharacterController>();

        
    }

    public void SwipeitLeft()
    {
        if (e_Lane == LANE.Mid)
        {
            Xcordinate = -xValue;
            e_Lane = LANE.Left;
        }
        else if (e_Lane == LANE.Right)
        {
            Xcordinate = 0;
            e_Lane = LANE.Mid;
        }
        var x = Xcordinate - transform.position.x;
        var vector = new Vector3(x, 0, 0);

        cc.Move(vector);
    }

    public void SwipeitRight()
    {
        if (e_Lane == LANE.Mid)
        {
            Xcordinate = xValue;
            e_Lane = LANE.Right;
        }
        else if (e_Lane == LANE.Left)
        {
            Xcordinate = 0;
            e_Lane = LANE.Mid;
        }
        var x = Xcordinate - transform.position.x;
        var vector = new Vector3(x, 0, 0);

        cc.Move(vector);
    }

    public void Jump()
    {
        animator.SetTrigger("Jump");
        // rb.AddForce(0,10,0);
         cc.Move(Vector3.up * 10f); 
        
        Debug.Log("ZIPLA");
    }

    // Update is called once per frame
    void Update()
    {
        if (GM.e_State == STATE.RUN)
        {
            var vector = new Vector3(0, 0, Time.deltaTime * Speed);

            cc.Move(vector);
        }
    }

    void Death()
    {
        Destroy(GameObject.Find("Player"));
    }


    private void FixedUpdate()
    {
        if (Health <= 0)
        {
            Death();
        }
    }


    private void OnCollisionEnter(Collision other)
    {
        
        if (other.collider.GetComponent<EngelTag>()!=null)
        {
            Debug.Log("CARPTIK");
            Destroy(other.collider.gameObject);
            Health -= 20;
        }
    }
}