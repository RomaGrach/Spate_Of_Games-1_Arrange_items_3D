using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerInfo
{
    public int level = 1;
    public int Scen = 0;
    public bool Chenjd = false;

}

public class Progress : MonoBehaviour
{

    

    public static Progress Instance;
    public PlayerInfo PlayerInfo;

    public int nowScen = Instance.PlayerInfo.Scen;
    public bool Chenjd = false;

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
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextScen()
    {
        if (Instance.PlayerInfo.level%5 == 1 && !Instance.PlayerInfo.Chenjd)
        {
            SceneManager.LoadScene(1);
            Instance.PlayerInfo.Chenjd = true;
        }
        else 
        {
            if (Instance.PlayerInfo.level % 5 == 2)
            {
                Instance.PlayerInfo.Chenjd = false;
            }
            
            SceneManager.LoadScene(2 + Instance.PlayerInfo.level % 5);
        }
    }
}
