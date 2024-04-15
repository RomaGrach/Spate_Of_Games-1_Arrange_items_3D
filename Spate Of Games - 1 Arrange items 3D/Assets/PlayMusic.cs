using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject surs;
    void Start()
    {
        if (Progress.Instance.PlayerInfo.Sound)
        {
            surs.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
