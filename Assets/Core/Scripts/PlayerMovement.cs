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
    private bool SwipeLeft;
    public float Speed = 7f;
    private bool SwipeRight;
    public float xValue;
    private CharacterController cc;
    
    
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        SwipeLeft = Input.GetKeyDown(KeyCode.A);
        SwipeRight = Input.GetKeyDown(KeyCode.D);

        if (SwipeLeft)
        {
            if (e_Lane == LANE.Mid)
            {
                Xcordinate = -xValue;
                e_Lane = LANE.Left;
            }
            else if(e_Lane == LANE.Right)
            {
                Xcordinate = 0;
                e_Lane = LANE.Mid;
            }
        }
        else if (SwipeRight)
        {
            if (e_Lane == LANE.Mid)
            {
                Xcordinate = xValue;
                e_Lane = LANE.Right;
            }
            else if(e_Lane == LANE.Left)
            {
                Xcordinate = 0;
                e_Lane = LANE.Mid;
            }
        }

        var x = Xcordinate - transform.position.x;
        var vector = new Vector3(x, 0, Time.deltaTime * Speed);

        cc.Move(vector);
    }
}
