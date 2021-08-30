using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>

{
  
    public TextMeshProUGUI yourScore;
    public TextMeshProUGUI bestscoreText;
    public GameObject gameOverPanel;
    public TextMeshProUGUI currentScoreText;
    int currentScore = 0;
    public Player playerPrefabt;
    public Platform platformPrefabt;
    public float minSpawnX;
    public float maxSpawnX;
    public float minSpawnY;
    public float maxSpawnY;

    
    

    public CamController mainCam;
    Player player;
    int m_score;
    
    public override void Awake()
    {
      
        MakeSingleton(false);
    }
    public override void Start()
    {
        
        base.Start();
        GetBestScore();
        StartCoroutine(PlatformInit());
    }
    IEnumerator PlatformInit()
    {
        Platform platformClone = null;
        if(platformPrefabt )
        {
            platformClone = Instantiate(platformPrefabt, new Vector2(0, Random.Range(minSpawnY, maxSpawnY)), Quaternion.identity);
            platformClone.id = platformClone.gameObject.GetInstanceID();
        }

        yield return new WaitForSeconds(0.1f);

        if(playerPrefabt)
        {
            player = Instantiate(playerPrefabt, Vector3.zero, Quaternion.identity);
            player.lastPlatformId = platformClone.id; 
        }
        if(platformPrefabt ) // tạo thêm platform phía trước
        {
            float spawnX = player.transform.position.x + minSpawnX;
            float spawnY = Random.Range(minSpawnY, maxSpawnY);
            Platform platformClone02 = Instantiate(platformPrefabt, new Vector2(spawnX, spawnY), Quaternion.identity);
            platformClone02.id = platformClone02.gameObject.GetInstanceID();   
        }
    }
    public void CreatPlatform()
    {
        if (!platformPrefabt || !player) return;

        float spawnX = Random.Range(player.transform.position.x + minSpawnX, player.transform.position.x + maxSpawnX);
        float spawnY = Random.Range(minSpawnY, maxSpawnY);

        Platform platformClone = Instantiate(platformPrefabt, new Vector2(spawnX, spawnY), Quaternion.identity);
        platformClone.id = platformClone.gameObject.GetInstanceID();   
    }
    public void CreatplatformandLerp(float playerXpos)
    {
        if (mainCam)
            mainCam.LerpTrigger(playerXpos + minSpawnX );
        CreatPlatform();
    }
    public void Addscore(int score)
    {
        currentScore += score;
        currentScoreText.text = currentScore.ToString();
        yourScore.text = currentScore.ToString();
        if (currentScore > PlayerPrefs.GetInt("BESTSCORE00", 0)) 
        {
            bestscoreText.text = currentScore.ToString();
            PlayerPrefs.SetInt("BESTSCORE00", currentScore);  
        }

    }
    void GetBestScore()
    {
        bestscoreText.text = PlayerPrefs.GetInt("BESTSCORE00", 0).ToString(); 
    }

    internal void countdead()
    {
        throw new System.NotImplementedException();
    }

    public void Gameover()
    {
        StartCoroutine(GameOverPanel());
    }
    IEnumerator GameOverPanel()
    {
        yield return new WaitForSeconds(0.1f);
        /*if (playAd == false)
        {
            GameObject.Find("GameObject").GetComponent<deadcount >().countdead(1) ;
          
        }
        playAd = true;*/


        gameOverPanel.SetActive(true); 
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Quit()
    {
        Application.Quit();
    }
}

