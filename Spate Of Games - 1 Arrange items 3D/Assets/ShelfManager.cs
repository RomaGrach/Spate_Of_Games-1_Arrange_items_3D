using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ShelfManager : MonoBehaviour
{
    public GameObject[] positions;
    public GameObject[] Items;
    public List<ItemsSet> IS;
    public int[] ListIndex;
    public GameObject efeect;
    //public SceneGameManager SGM; // поменял
    public SceneGameManagerNoWait SGM;
    //public SceneGameManagerNoWait SGMNW;
    // Start is called before the first frame update

    public class ItemsSet
    {
        public List<GameObject> items = new List<GameObject> { };
        public List<int> Inds = new List<int> { };
    }
    void Start()
    {
        SGM = FindObjectOfType<SceneGameManagerNoWait>();
        efeect = SGM.efeects;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SendChouse(int place)
    {
        if(Items[place] != null)
        {
            SGM.StartMouveChouse(Items[place], gameObject, ListIndex[place], place);
            Items[place] = null;
            ListIndex[place] = 0;
        }
        else
        {
            SGM.MouveChouse(gameObject, place);
        }
    }

    public void ResetChouse(GameObject chous, int index, int place)
    {
        ListIndex[place] = index;
        Items[place] = chous;
        Debug.Log(place);
        Debug.Log("++++++++++++++++++++");
        chous.transform.parent = positions[place].transform;
        //index[place] = 0;
        Chek();
    }

    public void PlaceChouse(GameObject chous, int index, int place)
    {
        chous.transform.position = positions[place].transform.position;
        ListIndex[place] = index;
        Items[place] = chous;
        Debug.Log(place);
        Debug.Log("++++++++++++++++++++");
        chous.transform.parent = positions[place].transform;
        //index[place] = 0;
        
    }

    public void Chek()
    {
        int rightInd = 0;
        bool ful = true;
        bool fulRight = true;
        foreach (GameObject Item in Items)
        {
            if (Item == null)
            {
                ful = false;
            }
        }
        if (ful)
        {
            rightInd = ListIndex[0];
            foreach (int ind in ListIndex)
            {
                if (ind != rightInd)
                {
                    fulRight = false;
                }
            }

            if (fulRight)
            {
                SGM.FulRight(rightInd);
                StartCoroutine(FulRightDest());
            }
        }

        


    }
    private IEnumerator FulRightDest()
    {
        foreach (GameObject pos in positions)
        {
            pos.GetComponent<BoxCollider>().enabled = false;
        }

        yield return new WaitForSeconds(0.5f);

        for (int k = 0; k < positions.Length; k++)
        {
            Instantiate(efeect, Items[k].transform.position, Items[k].transform.rotation);
            Destroy(Items[k]);

            Items[k] = null;
            ListIndex[k] = 0;
        }

        foreach (GameObject pos in positions)
        {
            pos.GetComponent<BoxCollider>().enabled = true;
        }
    }

    
}
