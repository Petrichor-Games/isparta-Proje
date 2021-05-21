using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public enum STATE
{
    RUN,
    ATTACK,
}

public class GameManager : MonoBehaviour
{
    public STATE e_State = STATE.RUN;

    public void ChangeState(int state)
    {
        switch (state)
        {
            case 0:
                e_State = STATE.RUN;
                break;
            case 1:
                e_State = STATE.ATTACK;
                break;
            default:
                e_State = STATE.RUN;
                break;
        }
    }
    
    
    void Start()
    {
        
    }


    void Update()
    {
        
    }
}
