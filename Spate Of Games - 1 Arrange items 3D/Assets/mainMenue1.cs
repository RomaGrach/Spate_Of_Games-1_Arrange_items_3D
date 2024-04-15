using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class mainMenue1 : MonoBehaviour
{
    public GameObject MusOn;
    public GameObject MusOf;
    public GameObject surs;
    public GameObject[] CanvasPoints;
    [SerializeField] TextMeshProUGUI ScoreText;
    // Start is called before the first frame update

    

    void Start()
    {
        YandexGame.RewardVideoEvent += Reward;
        if (!Progress.Instance.PlayerInfo.Sound)
        {
            
            MusOf.SetActive(true);
            MusOn.SetActive(false);
            
        }
        else
        {
            
            MusOf.SetActive(false);
            MusOn.SetActive(true);
            
        }


        int av;
        ScoreText.text = Progress.Instance.PlayerInfo.level.ToString();
        //Progress.Instance.ResetSave();
        if (Progress.Instance.PlayerInfo.level % 5 == 0)
        {
            av = 5;
        }
        else
        {
            av = Progress.Instance.PlayerInfo.level % 5;
        }


        for (int k = 0; k < av; k++)
        {
            CanvasPoints[k].SetActive(true);
        }
    }
    /*
    private void OnDestroy()
    {
        YandexGame.RewardVideoEvent -= Reward;
    }
    */
    void Reward(int id)
    {
        if (id == 1453235)
        {
            ReRotateAD();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!Progress.Instance.PlayerInfo.Sound)
        {

            MusOf.SetActive(true);
            MusOn.SetActive(false);

        }
        else
        {

            MusOf.SetActive(false);
            MusOn.SetActive(true);

        }


        int av;
        ScoreText.text = Progress.Instance.PlayerInfo.level.ToString();
        //Progress.Instance.ResetSave();
        if (Progress.Instance.PlayerInfo.level % 5 == 0)
        {
            av = 5;
        }
        else
        {
            av = Progress.Instance.PlayerInfo.level % 5;
        }


        for (int k = 0; k < av; k++)
        {
            CanvasPoints[k].SetActive(true);
        }
    }

    public void OpenScen()
    {
        Progress.Instance.NextScen();
    }

    public void MusOnOf()
    {
        if (Progress.Instance.PlayerInfo.Sound)
        {
            surs.SetActive(false);
            MusOf.SetActive(true);
            MusOn.SetActive(false);
            Progress.Instance.PlayerInfo.Sound = false;
        }
        else
        {
            surs.SetActive(true);
            MusOf.SetActive(false);
            MusOn.SetActive(true);
            Progress.Instance.PlayerInfo.Sound = true;
        }
        
    }

    public void ReRotate()
    {
        YandexGame.RewVideoShow(1453235);

    }

    public void ReRotateAD()
    {

        SceneManager.LoadScene(1);
    }


}
