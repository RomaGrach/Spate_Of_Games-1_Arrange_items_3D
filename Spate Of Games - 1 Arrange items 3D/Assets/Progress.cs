using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

[System.Serializable]
public class PlayerInfo
{
    public int level = 1;
    public int Scen = 0;
    public bool Chenjd = false;
    public bool Sound = true;

}

public class Progress : MonoBehaviour
{

    

    public static Progress Instance;
    public PlayerInfo PlayerInfo;

    void StopGame()
    {
        Time.timeScale = 0f;
        AudioListener.volume = 0f;
    }

    void PlayGame()
    {
        Time.timeScale = 1f;
        AudioListener.volume = 1f;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
            Instance = this;
            //StartCoroutine(WaitTime());
        }
        else
        {
            Destroy(gameObject);
        }
        //Instance.PlayerInfo = YandexGame.savesData.PlayerInfo;
    }

    // Start is called before the first frame update
    void Start()
    {
        Progress.Instance.PlayerInfo = YandexGame.savesData.PlayerInfo;
        YandexGame.OpenFullAdEvent += StopGame;
        YandexGame.CloseFullAdEvent += PlayGame;
        YandexGame.OpenVideoEvent += StopGame;
        YandexGame.CloseVideoEvent += PlayGame;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetSave()
    {
        Progress.Instance.PlayerInfo = new PlayerInfo();
        YandexGame.savesData.PlayerInfo = Progress.Instance.PlayerInfo;
        YandexGame.SaveProgress();
        
    }


    public void NextScen()
    {

        int scenToLoad;
        if (Instance.PlayerInfo.level%5 == 1 && !Instance.PlayerInfo.Chenjd)
        {
            scenToLoad = 1;
            //SceneManager.LoadScene(1);
            Instance.PlayerInfo.Chenjd = true;
        }
        else 
        {
            if (Instance.PlayerInfo.level % 5 == 2)
            {
                Instance.PlayerInfo.Chenjd = false;
            }
            scenToLoad = 2 + Instance.PlayerInfo.Scen;
            //SceneManager.LoadScene(2 + Instance.PlayerInfo.Scen);
        }
        YandexGame.savesData.PlayerInfo = Instance.PlayerInfo;
        //YandexGame.SaveProgress();

        SceneManager.LoadScene(scenToLoad);
    }
}
