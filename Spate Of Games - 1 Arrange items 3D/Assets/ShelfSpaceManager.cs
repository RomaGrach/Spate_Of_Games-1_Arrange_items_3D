using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfSpaceManager : MonoBehaviour
{
    public ShelfManager SM;
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
        SM.SendChouse(place);
    }
    
}
