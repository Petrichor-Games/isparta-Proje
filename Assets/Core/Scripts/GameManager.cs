using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public enum STATE
{
    RUN,
    ATTACK,
    DEAD
}

public class GameManager : MonoBehaviour
{
    private STATE e_State = STATE.RUN;

    public int score;
    public static GameManager inst;
    public GameObject Player;
    
    
    private void Update()
    {
        if (GameObject.FindGameObjectsWithTag("enemy").Length==0)
        {
            ChangeState(0);
            Player.GetComponent<Animator>().SetBool("Kos",true);
        }
        else
        {
            Player.GetComponent<Animator>().SetBool("Kos",false);
        }
    }


    public STATE GetState() => e_State;


    private void Awake()
    {
        inst = this;
    }


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
            case 2 :
                e_State = STATE.DEAD;
                break;
            default:
                e_State = STATE.RUN;
                break;
        }
    }

}
