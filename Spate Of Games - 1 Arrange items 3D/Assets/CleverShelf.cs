using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleverShelf : MonoBehaviour
{
    public int Size; // количество мест в полке
    public GameObject[] positions; // места в полке
    public List<ItemWave> IW; // набор волн предметов
    int MW = 0; // текущая волна
    public GameObject space; // заглушка
    public bool end = false;
    int Last;
    public GameObject efeect;
    public CleverSceneGameManager SGM; 

    [System.Serializable]
    public class ItemWave
    {
        public List<GameObject> Items = new List<GameObject> { }; // предметы
    }

    public void SendChouse(int place)
    {
        Debug.Log("++++++++");
        Debug.Log(place);
        if (IW[0].Items[place] != space && IW[0].Items[place] != null)
        {
            Debug.Log("11111111111111");
            SGM.StarMouve(IW[0].Items[place]);
            Last = place;
        }
        else
        {
            Debug.Log("2222222222222");
            SGM.MouveItem(gameObject, place);
        }
    }

    public void RemouveItem()
    {
        IW[0].Items[Last] = space;
        StartCoroutine(ChecForFull());
    }

    public void GetItem(GameObject item, int place)
    {
        Debug.Log("____-------");
        Debug.Log(place);
        IW[0].Items[place] = item;
        item.transform.position = positions[place].transform.position;
        item.transform.parent = gameObject.transform;
        StartCoroutine(ChecForFull());
    }

    private IEnumerator ChecForFull()
    {
        yield return new WaitForSeconds(0.1f);
        if (IW[0].Items.Count == Size && !end)
        {

            bool flag = true;
            foreach (GameObject Item in IW[0].Items)
            {
                if (Item != space && IW[0].Items[0] != space)
                {
                    if (Item.GetComponent<CleverItem>().index != IW[0].Items[0].GetComponent<CleverItem>().index)
                    {
                        flag = false;
                    }
                }
                else
                {
                    if (Item != IW[0].Items[0])
                    {
                        flag = false;
                    }
                }
            }

            if (flag)
            {
                

                if (IW[0].Items[0] != space)
                {
                    foreach (GameObject place in positions)
                    {
                        Instantiate(efeect, place.transform.position, place.transform.rotation);
                    }

                    SGM.score += 1;
                    SGM.WinNum -= 1;
                }

                foreach (GameObject Item in IW[0].Items)
                {
                    if(Item != space)
                    {
                        Destroy(Item);
                    }
                }

                if (IW.Count > 1)
                {
                    IW.RemoveAt(0);
                    PlaysItems();
                }
                else
                {
                    IW.RemoveAt(0);
                    IW.Add(new ItemWave());
                    for (int w = IW[IW.Count - 1].Items.Count; w < Size; w++)
                    {
                        //GameObject now = Instantiate(space, transform.position, transform.rotation);
                        IW[IW.Count - 1].Items.Add(space);
                    }
                    PlaysItems();
                }
                
            }

            
            




        }

    }

    public void NoItemsWaw()
    {
        if(IW[0].Items.Count == 0)
        {
            IW.RemoveAt(0);
        }

    }


    // Start is called before the first frame update
    void Start()
    {
        SGM = FindObjectOfType<CleverSceneGameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetWawe(List<GameObject> Items)
    {
        IW.Add(new ItemWave());
        IW[IW.Count - 1].Items = Items;
        for (int w = IW[IW.Count - 1].Items.Count; w < Size; w++)
        {
            //GameObject now = Instantiate(space, transform.position, transform.rotation);
            IW[IW.Count - 1].Items.Add(space);
        }
            
        foreach (GameObject Item in Items)
        {
            Item.transform.parent = gameObject.transform;
        }
    }

    public void PlaysItems()
    {
        for (int w = 0; w < IW.Count; w++)
        {
            for (int p = 0; p < IW[w].Items.Count; p++)
            {

                IW[w].Items[p].transform.position = positions[p].transform.position + transform.forward * w;

            }
        }
    }
}
