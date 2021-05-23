using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Touch;
using UnityEngine;
using UnityEngine.UI;

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
    public GameObject OyunSonuSkor;

    private bool SwipeLeft;
    public float Speed = 14f;
    private bool SwipeRight;
    private Animator animator;
    public float xValue;
    private CharacterController cc;
    private GameManager GM;
    public GameObject CamLoc;
    private Vector3 desiredPosition;
    private Rigidbody rb;
    private Vector3 movement;
    public Camera camera;
    public GameObject MermiLoc;
    public GameObject MermiPrefab;
    public GameObject CanSlider; 
    public GameObject menu;
    public GameObject PARA;
    public AudioSource audioSource;
    public AudioClip krilmaSesi;
    
    float timeElapsed;
    float lerpDuration = 3;

    float startValue=0;
    float endValue=10;
    float valueToLerp;
    private bool death = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        cc = GetComponent<CharacterController>();
        menu.SetActive(false);
        CanSlider.GetComponent<Slider>().value = Health;
        PARA.GetComponent<Text>().text = Game.Instance.Coins.ToString();
    }

    public void SwipeitLeft()
    {
        
        if (e_Lane == LANE.Mid)
        {
            Xcordinate = -xValue;
            e_Lane = LANE.Left;
            animator.SetTrigger("Roll");
        }
        else if (e_Lane == LANE.Right)
        {
            Xcordinate = 0;
            e_Lane = LANE.Mid;
            animator.SetTrigger("Roll");
        }
        var x = Xcordinate - transform.position.x;
        var vector = new Vector3(x, 0, 0);

        cc.Move(vector);
    }

    public void Shoot(LeanFinger LF)
    {
        if (GM.GetState()!=STATE.ATTACK)
            return;
        var mermi = Instantiate(MermiPrefab, MermiLoc.transform.position, MermiLoc.transform.rotation);
        mermi.transform.LookAt(LF.GetWorldPosition(10));
        mermi.GetComponent<Rigidbody>().AddForce(mermi.transform.forward * 1900f);
        animator.SetTrigger("shoot");
        Destroy(mermi, 5f);
    }
    

    public void SwipeitRight()
    {
        
        if (e_Lane == LANE.Mid)
        {
            Xcordinate = xValue;
            e_Lane = LANE.Right;
            animator.SetTrigger("Roll");
        }
        else if (e_Lane == LANE.Left)
        {
            Xcordinate = 0;
            e_Lane = LANE.Mid;
            animator.SetTrigger("Roll");
        }
        var x = Xcordinate - transform.position.x;
        var vector = new Vector3(x, 0, 0);

        cc.Move(vector);
    }

    public void Jump()
    {
        if (GM.GetState()!=STATE.RUN)
        return;
        if (cc.isGrounded==false)
        {
            return;
        }
        
        animator.SetTrigger("Jump");
        movement.y = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        switch (GM.GetState())
        {
            case STATE.RUN :

                if (Speed < 40)
                {
                    Speed += Time.deltaTime * 0.5f;
                }
                
                
                if (death)
                {
                    rb.velocity = Vector3.zero;
                    return;
                }
            
                // Applying Gravity
                if (cc.isGrounded == false)
                {
 
                    movement.y += Physics.gravity.y * 0.08f;
 
                }
                var vector = new Vector3(0, movement.y * Time.deltaTime, Time.deltaTime * Speed);
                // Applying Movement
                cc.Move(vector);
                break;
            case STATE.DEAD :
                rb.velocity = Vector3.zero;
                break;
            case STATE.ATTACK :
                camera.transform.position = Vector3.Lerp(camera.transform.position, CamLoc.transform.position, Time.deltaTime);
                camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, 98, Time.deltaTime);
                break;
            default:
                break;
            
        }
    }

    void Death()
    {
        death = true;
        GM.ChangeState(2);
        animator.SetTrigger("Death");
        var Metre = (transform.position.z + 26) / 10;

        OyunSonuSkor.GetComponent<Text>().text = Metre.ToString("0.##") + " Metre Koştun. \n " + GM.score + " Düşman öldürdün.";
        //Destroy(GameObject.Find("Player") , 5f);
        menu.SetActive(true);
    }


    private void FixedUpdate()
    {
        if (Health <= 0)
        {
            Death();
        }
        PARA.GetComponent<Text>().text = Game.Instance.Coins.ToString();
    }


    private void OnCollisionEnter(Collision other)
    {
        
        if (other.collider.GetComponent<EngelTag>()!=null)
        {
            Destroy(other.collider.gameObject);
            Health -= 20;
            CanSlider.GetComponent<Slider>().value = Health;
            if (Speed> 21f)
            {
                Speed -= 7f;
            }
            audioSource.PlayOneShot(krilmaSesi, 1f);
        }

       
        
        if (other.gameObject.tag == "mermi")
        {
            Destroy(other.collider.gameObject);
            Health -= 20;
            CanSlider.GetComponent<Slider>().value = Health;
        }
    }

    public void CanEkle()
    {
        if (Health>0&&Health<199)
        {
            Health=+2;
            CanSlider.GetComponent<Slider>().value = Health;
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