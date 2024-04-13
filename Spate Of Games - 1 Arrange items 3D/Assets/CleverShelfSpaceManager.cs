using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleverShelfSpaceManager : MonoBehaviour
{
    public CleverShelf SM;
    public int place;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SendNowItem()
    {
        Debug.Log(place);
        SM.SendChouse(place);
    }
}
