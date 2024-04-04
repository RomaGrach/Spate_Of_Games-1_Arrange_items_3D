using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class SceneGameManager : MonoBehaviour
{
    public GameObject NowItem = null;
    public GameObject NowShelf = null;
    public int NowIndex;
    public int NowPlace;
    bool go = false;
    public Vector3 PlaceToMouve;
    float time = 0;
    public int score = 0;

    

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    public void FulRight(int ind)
    {

        score += ind;
    }

    // Update is called once per frame
    void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && time <= 0 && !go)
        {
            Debug.Log("press");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //Instantiate(cub, hit.point, new Quaternion(0, 0, 0, 0));
                Debug.Log(hit.collider.tag);
                if (hit.collider.tag == "Place")
                {
                    hit.collider.gameObject.GetComponent<ShelfSpaceManager>().SendNowItem();
                    Debug.Log("spavn");
                    
                    //nowCube = ObjectPooler.ME.RequestObject("1", new Vector3(hit.point.x, 10f, 0), new Quaternion(0, 0, 0, 0));

                }

            }
        }
    }

    public void StartMouveChouse(GameObject chous, GameObject shelf, int index, int place)
    {
        if (NowItem != null)
        {
            GoBackChouse();
        }
        Debug.Log(chous);
        Debug.Log(shelf);
        Debug.Log(index);
        Debug.Log(place);
        chous.transform.parent = null;
        NowItem = chous;
        NowShelf = shelf;
        NowIndex = index;
        NowPlace = place;
        StartCoroutine(PullOutItem(NowItem));

    }

    public void MouveChouse(GameObject shelf, int place)
    {
        Debug.Log(place);
        Debug.Log("_+__________");
        if(shelf == NowShelf && place == NowPlace)
        {
            GoBackChouse();
        }
        else if (NowItem != null)
        {
            go = true;
            NowShelf = shelf;
            NowPlace = place;
            PlaceToMouve = shelf.GetComponent<ShelfManager>().positions[place].transform.position;
            Debug.Log(PlaceToMouve);
            PlaceToMouve = PlaceToMouve + new Vector3(0, 0, -1);
        }

    }

    private void FixedUpdate()
    {

        if (go)
        {
            Vector3 direction = (PlaceToMouve - NowItem.transform.position).normalized * 0.1f;

            //Debug.Log(Point.transform.position);
            NowItem.transform.position = NowItem.transform.position + direction;
            if((PlaceToMouve - NowItem.transform.position).magnitude <= 0.1f)
            {
                NowItem.transform.position = PlaceToMouve;
                go = false;
                Debug.Log(NowItem.transform.position);
                GoBackChouse();
            }
        }
    }
    private IEnumerator PullOutItem(GameObject chous)
    {
        time = 1.1f;
        for (int k = 0; k < 50; k++)
        {
            yield return new WaitForSeconds(0.02f);
            chous.transform.position = chous.transform.position + new Vector3(0,0,-0.02f);

        }
        
    }

    private IEnumerator PullBackItem(GameObject chous, GameObject shelf, int index, int place)
    {
        for (int k = 0; k < 50; k++)
        {
            yield return new WaitForSeconds(0.02f);
            chous.transform.position = chous.transform.position + new Vector3(0, 0, 0.02f);

        }
        shelf.GetComponent<ShelfManager>().ResetChouse(chous, index, place);
    }


    public void GoBackChouse()
    {
        time = 1.1f;
        StartCoroutine(PullBackItem(NowItem, NowShelf, NowIndex, NowPlace));
        NowItem = null;
    }

}
