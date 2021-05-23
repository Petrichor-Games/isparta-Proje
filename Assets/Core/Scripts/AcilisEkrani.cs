using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AcilisEkrani : MonoBehaviour
{
    public float wait_time = 7f;
    //rütini başlat
    void Start()
    {
        StartCoroutine(WaitForinfo());
    }

    //vidyonun bitmesini ekle
    IEnumerator WaitForinfo()
    {
        yield return new WaitForSeconds(wait_time);
        SceneManager.LoadScene(1);
    }
}
