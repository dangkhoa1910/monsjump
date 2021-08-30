using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class Player : MonoBehaviour
{
    public GameObject deadEffect;
   
    public Vector2 jumpForce;
    public Vector2 jumpForceUp;
    public float minForceX;
    public float maxForceX;
    public float minForceY;
    public float maxForceY;
    float hue;
    [HideInInspector]
    public int lastPlatformId;
    bool didJump;
    bool powerSetted;

    Rigidbody2D myBody;
    Animator anim;
   

    void Start()
    {
        InitColor();
    }
    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        

    }
    private void Update()
    {
       
        SetPower();
        if(Input.GetMouseButtonDown (0) )
        {
            SetPower(true);

        }
        if (Input.GetMouseButtonUp (0) )
        {
            SetPower(false);
        }
    }
    void InitColor()
    {
        hue = Random.Range(0,1f);
        Camera.main.backgroundColor = Color.HSVToRGB(hue, 0.6f, 0.9f);   
    }
    void SetPower()
    {
        if(powerSetted && !didJump ) // nếu đang setpower và nhân vật chưa nhảy thì thực hiện tăng setpower lên( trong khi nhân vật đứng yên
        {
            jumpForce.x += jumpForceUp.x * Time.deltaTime;
            jumpForce.y += jumpForceUp.y * Time.deltaTime;
            jumpForce.x = Mathf.Clamp(jumpForce.x, minForceX, maxForceX);
            jumpForce.y = Mathf.Clamp(jumpForce.y, minForceY, maxForceY);
            
        }
    }
    public void SetPower(bool isHoldingMouse)
    {
        powerSetted = isHoldingMouse; // hai cái đều thực hiện là true
        if(!powerSetted && !didJump ) // và ngược lại nếu nhả chuột và chưa nhảy lên sẽ gọi phương thức Jump()
        {
            Jump();

        }  
    }
    void Jump()
    {
        if (!myBody || jumpForce.x <= 0 || jumpForce.y <= 0) return;// nếu thỏa điều kiện thì return xóa hết các lệnh ở dưới

        myBody.velocity = jumpForce;
        didJump = true;
        
    }
    private void OnTriggerEnter2D(Collider2D target)

    {
        if(target.tag=="ground")
        {
            Platform p = target.transform.root.GetComponent<Platform>();
            if(didJump )
            {
                didJump = false;

                if (myBody)
                    myBody.velocity = Vector2.zero;
                jumpForce = Vector2.zero;
            }
            if (p && p.id != lastPlatformId )
            {
                GameManager.Ins.CreatplatformandLerp(transform.position.x) ;
                lastPlatformId = p.id;

                GameObject.Find("GAMEMANAGER").GetComponent<GameManager>().Addscore(1);
            }
        }else if(target.tag =="obstacle")
        {
            Destroy(Instantiate(deadEffect, transform.position, Quaternion.identity),1f);
            
            GameObject.Find("GAMEMANAGER").GetComponent<GameManager>().Gameover();
            GameObject.Find("ads").GetComponent<deadcount >().countdead();



        }
    }
   
}
