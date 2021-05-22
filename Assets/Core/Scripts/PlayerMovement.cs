using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Touch;
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
    public GameObject CamLoc;
    private Vector3 desiredPosition;
    private Rigidbody rb;
    private Vector3 movement;
    public Camera camera;
    public GameObject MermiLoc;
    public GameObject MermiPrefab;
    
    float timeElapsed;
    float lerpDuration = 3;

    float startValue=0;
    float endValue=10;
    float valueToLerp;


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
        if (GM.GetState()==STATE.ATTACK)
            return;
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

    public void Shoot(LeanFinger LF)
    {
        Ray hit = LF.GetStartRay(camera);
        var mermi = Instantiate(MermiPrefab, MermiLoc.transform.position, MermiLoc.transform.rotation);
        mermi.GetComponent<Rigidbody>().AddForce(LF.GetWorldPosition(500) * 10f);
        Destroy(mermi, 5f);
    }
    

    public void SwipeitRight()
    {
        if (GM.GetState()==STATE.ATTACK)
            return;
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
        if (GM.GetState()==STATE.ATTACK)
        return;
        
        animator.SetTrigger("Jump");
        movement.y = 10f;
        Debug.Log("ZIPLA");
    }

    // Update is called once per frame
    void Update()
    {
        if (GM.GetState() == STATE.RUN)
        {
            // Applying Gravity
            if (cc.isGrounded == false)
            {
 
                movement.y += Physics.gravity.y * 0.08f;
 
            }
            var vector = new Vector3(0, movement.y * Time.deltaTime, Time.deltaTime * Speed);
            // Applying Movement
            cc.Move(vector);
        }
        else
        {
            camera.transform.position = Vector3.Lerp(camera.transform.position, CamLoc.transform.position, Time.deltaTime);
            camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, 98, Time.deltaTime);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<OyunSonuTag>()!=null)
        {
            GM.ChangeState(1);
            animator.SetTrigger("Idle");
            rb.velocity = Vector3.zero;
            
            
        }
    }
}