//using System;

using System.Collections;
using System.Collections.Generic;

using UnityEditor;
using UnityEngine;


public class SceneGameManagerNoWait : MonoBehaviour
{
    public GameObject NowItem = null;
    public GameObject NowShelf = null;
    public List<matrixPlace> MP;
    public List<GameObject> matrixShelf;

    public ItemBase[] IB;
    public GameObject[] ShelfBase;

    public PointShelfsArrays[] PSA;

    public int NowIndex;
    public int NowPlace;
    bool go = false;
    public Vector3 PlaceToMouve;
    //float time = 0;
    public int score = 0;

    public GameObject efeects;

    [System.Serializable]
    public class matrixPlace
    {
        public List<int> Places = new List<int> { };
    }

    [System.Serializable]
    public class PointShelfsArrays
    {
        public Transform[] PointArray;
    }
    [System.Serializable]
    public class ItemBase
    {
        public GameObject item;
        public int Ind;
    }

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        GameStart();
    }

    public void GameStart()
    {
        int NOI = Random.Range(3, 18); // число предметов
        int NOS = NOI / 3 + 1;// число полок
        if (NOS > PSA[0].PointArray.Length)
        {
            NOS = PSA[0].PointArray.Length;
        }

        List<int> usePose=new List<int> { };
        for (int k = 0; k < NOS; k++)
        {
            int a = Random.Range(0, PSA[0].PointArray.Length);
            if (usePose != null)
            {
                while (usePose.Contains(a))
                {
                    a = Random.Range(0, PSA[0].PointArray.Length);
                }
            }
            
            usePose.Add(a);
            Debug.Log(usePose);
            GameObject now = Instantiate(ShelfBase[0], PSA[0].PointArray[a].position, PSA[0].PointArray[a].rotation);

            matrixShelf.Add(now);
            MP.Add(new matrixPlace());
            MP[k].Places.Add(0);
            MP[k].Places.Add(0);
            MP[k].Places.Add(0);

        }

        for (int k = 0; k < NOI; k++)
        {
            int a = Random.Range(0, MP.Count);
            int b = Random.Range(0, MP[a].Places.Count);
            bool flag = true;
            while (flag)
            {
                a = Random.Range(0, MP.Count);
                b = Random.Range(0, MP[a].Places.Count);
                if (MP[a].Places[b] == 0)
                {
                    flag = false;
                }
            }

            int c = Random.Range(0, IB.Length);
            flag = true;
            
            while (flag)
            {
                c = Random.Range(0, IB.Length);

                for (int k1 = 0; k1 < MP[a].Places.Count; k1++)
                {
                    if (c != MP[a].Places[k1] && b != k1)
                    {
                        flag = false;
                    }
                }
                    
            }
            /*
            while (flag)
            {
                if()
                c = Random.Range(0, IB.Length);
            }
            */

            MP[a].Places[b] = IB[c].Ind;

            

            
            GameObject now = Instantiate(IB[c].item, PSA[0].PointArray[a].position, PSA[0].PointArray[a].rotation);

            matrixShelf[a].GetComponent<ShelfManager>().PlaceChouse(now, MP[a].Places[b], b);

        }



    }

    private void Shuffle<T>(T[] array)
    {
        int n = array.Length;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = array[k];
            array[k] = array[n];
            array[n] = value;
        }
    }

    public void FulRight(int ind)
    {

        score += ind;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (time > 0)
        {
            time -= Time.deltaTime;
        }
        */
        if (Input.GetKeyDown(KeyCode.Mouse0)  && !go)
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
        //StartCoroutine(PullOutItem(NowItem));
        chous.transform.position = chous.transform.position + new Vector3(0, 0, -1f);

    }

    public void MouveChouse(GameObject shelf, int place)
    {
        Debug.Log(place);
        Debug.Log("_+__________");
        if (shelf == NowShelf && place == NowPlace)
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
            //NowShelf.GetComponent<ShelfManager>().ResetChouse(NowItem, NowIndex, NowPlace);
            Vector3 direction = (PlaceToMouve - NowItem.transform.position).normalized * 0.1f;

            //Debug.Log(Point.transform.position);
            NowItem.transform.position = NowItem.transform.position + direction;
            if ((PlaceToMouve - NowItem.transform.position).magnitude <= 0.1f)
            {
                NowItem.transform.position = PlaceToMouve;
                go = false;
                Debug.Log(NowItem.transform.position);
                GoBackChouse();
            }
        }
    }

    /*
    private IEnumerator PullOutItem(GameObject chous)
    {
        time = 1.1f;
        for (int k = 0; k < 50; k++)
        {
            yield return new WaitForSeconds(0.02f);
            chous.transform.position = chous.transform.position + new Vector3(0, 0, -0.02f);

        }

    }
    */

    /*
    private IEnumerator PullBackItem(GameObject chous, GameObject shelf, int index, int place)
    {
        for (int k = 0; k < 50; k++)
        {
            yield return new WaitForSeconds(0.02f);
            chous.transform.position = chous.transform.position + new Vector3(0, 0, 0.02f);

        }
        shelf.GetComponent<ShelfManager>().ResetChouse(chous, index, place);
    }
    */


    public void GoBackChouse()
    {
        //time = 1.1f;
        //StartCoroutine(PullBackItem(NowItem, NowShelf, NowIndex, NowPlace));
        NowItem.transform.position = NowItem.transform.position + new Vector3(0, 0, 1);
        NowShelf.GetComponent<ShelfManager>().ResetChouse(NowItem, NowIndex, NowPlace);


        NowItem = null;
    }
}
