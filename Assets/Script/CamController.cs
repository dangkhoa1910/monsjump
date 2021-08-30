using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public float lerbTime;
    public float xOffset;
    bool canLerp;
    float lerpXDist;

    private void Update()
    {
        if (canLerp)
            MoveLerp();
    }
    void MoveLerp()
    {
        float xPos = transform.position.x;
        xPos = Mathf.Lerp(xPos, lerpXDist, lerbTime * Time.deltaTime);

        transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
        if(transform.position.x >= (lerpXDist -xOffset ) )
        {
            canLerp = false;
        }
    }
    public void LerpTrigger(float Dist)
    {
        canLerp = true;
        lerpXDist = Dist;
    }
}
