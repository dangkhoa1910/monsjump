using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deadcount : MonoBehaviour
{
    static  int counter = 0;

   

    public void countdead()
    {
        counter++;
        if (counter == 4)
        {
            counter = 0;
            Debug.Log("show ad");
        }
        DontDestroyOnLoad(this.gameObject); 

    }
}