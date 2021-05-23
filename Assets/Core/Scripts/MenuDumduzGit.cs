using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuDumduzGit : MonoBehaviour
{
    void Update()
    {
        //DÜMDÜZ YARDIR
        transform.position = new Vector3(0,0, transform.position.z + .1f);
    }
}
