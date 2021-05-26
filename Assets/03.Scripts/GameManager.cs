using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public new Camera camera;
    private GameObject player; 

    public Image gameOverImage;

    public Text gameOverTimeText;
    public Text gameOverGemText;

    public BackGroundAudio bgAM;

    public static bool isPlay = true;

    private int currentTime = 0;
    public static int gem = 0;

    public AudioClip stage1;
    public AudioClip stage2;
    public AudioClip stage3;

    public bool startAudio;
    public static bool isGameScene;
    int currentBgmNum = 1;
    float startTime = 0;

    bool timeCount;

    public GameObject minimapCamera;
    public GameObject[] minimapBox;

    public Transform stoneSpawner;
    public Transform stoneSpawner2;
    public GameObject stone2;
    public GameObject stone;
    private bool isSpawn = true;

    public static bool isGameClear;
    private bool showMimimap = false;

    public Text gameClearText;
    public GameObject endingCredit;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        gem = 0;
        startTime = Time.time;
        timeCount = true;
        gameClearText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ChangGeBackGgoundBGM();
        MoveCamera();
        GameOverUI();
        GameClear();
        ShowMinimap();
        Cheet();
        if(isSpawn)
            StartCoroutine("StoneSpawn");
    }
    IEnumerator StoneSpawn()
    {
        
        if (NextScene.currentScene == 6)
        {
            isSpawn = false;
            for (int i = 0; ; i++)
            {
                Instantiate(stone, stoneSpawner.position, Quaternion.identity);
                yield return new WaitForSeconds(3.0f);
                Instantiate(stone2, stoneSpawner2.position, Quaternion.identity);
                yield return new WaitForSeconds(0.5f);
                Instantiate(stone, stoneSpawner.position, Quaternion.identity);
                yield return new WaitForSeconds(1.0f);
            }
        }
        else
            yield return null;

    }

    private void ShowMinimap(){
        if(Input.GetKeyDown(KeyCode.M) && !showMimimap)
        {
            showMimimap = true;
            minimapCamera.SetActive(false);
            for(int i = 0; i<minimapBox.Length; i++)
                minimapBox[i].SetActive(false);
                
        }
        else if(Input.GetKeyDown(KeyCode.M) && showMimimap)
        {
            showMimimap = false;
            minimapCamera.gameObject.SetActive(true);
            for(int i = 0; i<minimapBox.Length; i++)
                minimapBox[i].SetActive(true);
        }
    }
    private void GameClear(){
        if(isGameClear)
        {
            gameClearText.gameObject.SetActive(false);
            for (int i = 0; i < minimapBox.Length; i++)
                minimapBox[i].SetActive(false);
            minimapCamera.SetActive(false);
            endingCredit.SetActive(true);
        }
    }
    private void GameOverUI()
    {
        if(timeCount)
            currentTime = (int)(Time.time - startTime);
        if (isPlay)
        {
            gameOverImage.gameObject.SetActive(false);
        }
        if (!isPlay)
        {
            timeCount = false;
            int maxTime = currentTime;
            int minuteTime = 0;
            while(maxTime >= 60)
            {
                minuteTime++;
                maxTime -= 60;
            }
 

            gameOverGemText.text = "젬 :     " + gem + " 개";
            gameOverTimeText.text = ": " + minuteTime + "분 " +maxTime+"초";
            gameOverImage.gameObject.SetActive(true);
        }
    }
    private void MoveCamera()
    {
        if (NextScene.currentScene == 0)
            camera.transform.position = new Vector3(73f, 0, -10);
        else if (NextScene.currentScene == 1)
            camera.transform.position = new Vector3(73, 211.2f, -10);
        else if (NextScene.currentScene == 2)
            camera.transform.position = new Vector3(73, 413.5f, -10);
        else if (NextScene.currentScene == 3)
            camera.transform.position = new Vector3(73, 617.1f, -10);
        else if (NextScene.currentScene == 4)
        {
            camera.transform.position = new Vector3(73, 817.2f, -10);
            if (NextScene.currentEventScene == 2)
                camera.transform.position = new Vector3(428.6f, 817.2f, -10);
            else
                camera.transform.position = new Vector3(73, 817.2f, -10);
        }
        else if (NextScene.currentScene == 5)
        {
            camera.transform.position = new Vector3(73, 1017f, -10);
        }
        else if (NextScene.currentScene == 6)
        {
            camera.transform.position = new Vector3(73, 1217f, -10);
        }
        else if (NextScene.currentScene == 7)
        {
            camera.transform.position = new Vector3(73, 1415f, -10);
        }
        else if (NextScene.currentScene == 8)
        {
            camera.transform.position = new Vector3(73, 1617f, -10);
            gameClearText.gameObject.SetActive(true);
        }
        if(NextScene.currentScene!=8)
            gameClearText.gameObject.SetActive(false);

    }
    private void ChangGeBackGgoundBGM()
    {
        switch (NextScene.currentScene)
        { 
            case 0:
            case 1:
            case 2:
                if (!startAudio)
                {
                    if (currentBgmNum == 1)
                        return;
                    else
                    {
                        currentBgmNum = 1;
                        startAudio = true;
                        bgAM.PlayAudio(stage1, 0.15f);
                    }
                }
                break;
            case 3:
            case 4:
            case 5:
                if (!startAudio)
                {
                    if (currentBgmNum == 2)
                        return;
                    else
                    {
                        currentBgmNum = 2;
                        startAudio = true;
                        bgAM.PlayAudio(stage2,0.6f);
                    }
                }
                break;
            case 6:
            case 7:
            case 8:
                if (!startAudio)
                {
                    if (currentBgmNum == 3)
                        return;
                    else
                    {
                        currentBgmNum = 3;
                        startAudio = true;
                        bgAM.PlayAudio(stage3,0.3f);
                    }
                }
                break;

            default: break;
        }
    }

    private void Cheet()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.F))
            PlayerControllor.playerHp = 999;
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.G))
            PlayerControllor.playerHp = 6;


        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Q))
        {
            startAudio = false;
            NextScene.currentScene = 0;
            player.transform.position = new Vector3(88,-67,0);
        }
        else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.W))
        {
            startAudio = false;
            NextScene.currentScene = 1;
            player.transform.position = new Vector3(64, 133, 0);

        }
        else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.E))
        {
            startAudio = false;
            NextScene.currentScene = 2;
            player.transform.position = new Vector3(130, 354, 0);

        }
        else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.R))
        {
            startAudio = false;
            NextScene.currentScene = 3;
            player.transform.position = new Vector3(78, 550, 0);

        }
        else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.T))
        {
            startAudio = false;
            NextScene.currentScene = 4;
            player.transform.position = new Vector3(215, 740, 0);

        }
        else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Y))
        {
            startAudio = false;
            NextScene.currentScene = 5;
            player.transform.position = new Vector3(-42, 945, 0);

        }
        else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.U))
        {
            startAudio = false;
            NextScene.currentScene = 6;
            player.transform.position = new Vector3(27, 1197, 0);

        }
        else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.I))
        {
            startAudio = false;
            NextScene.currentScene = 7;
            player.transform.position = new Vector3(0, 1342, 0);


        }
        else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.O))
        {
            startAudio = false;
            NextScene.currentScene = 8;
            player.transform.position = new Vector3(224, 1541, 0);

        }
    }
}
