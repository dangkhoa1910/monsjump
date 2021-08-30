using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDestroy : MonoBehaviour
{
    // Start is called before the first frame update
   void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "destroy")
        {
            Destroy(gameObject); 
        }
    }
}
