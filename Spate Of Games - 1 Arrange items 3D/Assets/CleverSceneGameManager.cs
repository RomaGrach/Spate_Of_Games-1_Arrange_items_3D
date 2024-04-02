
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class CleverSceneGameManager : MonoBehaviour
{
    public GameObject NowItem = null;
    public PointShelfsArrays[] PSA;  // наборы позиций для объектов
    public List<ShelfsItemsArray> SIA;  // набор полка и волны для нее
    public List<GameObject> ItemsBase;
    public List<GameObject> ShelfsBase;
    public int score = 0;
    public int NOPL; // оставшееся количество пар
    public int WinNum;
    public List<GameObject> AI = new List<GameObject> { };
    



    [System.Serializable]
    public class ShelfsItemsArray
    {
        public GameObject S; // полка
        public List<ItemWave> IW = new List<ItemWave> { }; // наборы предметов
    }

    [System.Serializable]
    public class ItemWave
    {
        public List<GameObject> Items = new List<GameObject> { }; // предметы
    }

    [System.Serializable]
    public class PointShelfsArrays
    {
        public Transform[] PointArray;
    }


    public GameObject CanvasWin;
    public GameObject CanvasLose;
    bool start = false;
    float timer1;

    // Start is called before the first frame update
    void Start()
    {
        GenerateGame(10, 0, 0);
        start = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
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
                    hit.collider.gameObject.GetComponent<CleverShelfSpaceManager>().SendNowItem();
                    Debug.Log("spavn");

                    //nowCube = ObjectPooler.ME.RequestObject("1", new Vector3(hit.point.x, 10f, 0), new Quaternion(0, 0, 0, 0));

                }

            }
        }

        if(CanvasWin != null && WinNum == 0 && start)
        {
            CanvasWin.SetActive(true);
        }

        timer1 += Time.deltaTime;

        if (CanvasLose != null && WinNum > 0 && start)
        {
            bool ful = true;
            foreach (ShelfsItemsArray waw in SIA)
            {
                int size = 0;
                foreach (GameObject obj in waw.S.GetComponent<CleverShelf>().IW[0].Items)
                {
                    if(obj != waw.S.GetComponent<CleverShelf>().space)
                    {
                        size += 1;
                    }
                }
                if (size != waw.S.GetComponent<CleverShelf>().Size)
                {
                    ful = false;
                }
            }
            if (ful)
            {
                CanvasLose.SetActive(true);
            }
           
        }


    }

    public void DemoResetGame()
    {
        SceneManager.LoadScene(0);
    }

    public void GetItemBack()
    {
        if (NowItem != null)
        {
            NowItem.transform.position = NowItem.transform.position + NowItem.transform.parent.transform.forward;
            NowItem = null;
        }
    }

    public void StarMouve(GameObject item)
    {
        
        if (NowItem == item)
        {
            Debug.Log("____________1");
            GetItemBack();
        }else
        {
            Debug.Log("____________2");
            GetItemBack();
            NowItem = item;
            NowItem.transform.position = NowItem.transform.position + item.transform.parent.transform.forward * -1;
        }
        
    }

    public void MouveItem(GameObject shelf, int place)
    {

        if (NowItem != null)
        {
            NowItem.transform.parent.GetComponent<CleverShelf>().RemouveItem();
            shelf.GetComponent<CleverShelf>().GetItem(NowItem, place);
            NowItem = null;
        }
    }


    public void GenerateGame(int NOP, int SLN, int SO) // количество пар,  вариант расстановки полок, вариант полки
    {
        Debug.Log("_______0________");
        WinNum = NOP;
        for (int k = 0; k < PSA[SLN].PointArray.Length; k++)
        {
            int RS = Random.Range(0, PSA.Length);
            GameObject now = Instantiate(ShelfsBase[SO], PSA[RS].PointArray[k].position, PSA[RS].PointArray[k].rotation);
            SIA.Add(new ShelfsItemsArray());
            SIA[k].S = now;
        }
        
        Debug.Log("_______1________");
        
        int NOS = PSA[SLN].PointArray.Length;

        int SZ = ShelfsBase[SO].GetComponent<CleverShelf>().Size;

        int NOI = SZ * NOP; // общее колличество предметов

        AI = new List<GameObject> { }; // массив  всех предметов

        Debug.Log("_______2________");

        Debug.Log("ItemsBase.Count-1");
        Debug.Log(ItemsBase.Count - 1);
        Debug.Log("NOP");
        Debug.Log(NOP);
        Debug.Log("Random.Range(0, ItemsBase.Count-1)");
        Debug.Log(Random.Range(0, ItemsBase.Count - 1));
        Debug.Log("ItemsBase.Count");
        Debug.Log(ItemsBase.Count);
        Debug.Log("SZ");
        Debug.Log(SZ);


        /*
        int randomItemAIndex = Random.Range(0, ItemsBase.Count);

        GameObject now2 = Instantiate(ItemsBase[randomItemAIndex], transform.position, transform.rotation);

        AI.Add(now2);

        */
        /*
        for (int k = 0; k < NOP; k++)
        {
            Debug.Log("______________k");
            Debug.Log(k);
            int randomItemAIndex = Random.Range(0, ItemsBase.Count - 1);
            Debug.Log("randomItemAIndex");
            Debug.Log(randomItemAIndex);
            
            GameObject now2 = Instantiate(ItemsBase[randomItemAIndex], transform.position, transform.rotation);
                
            //GameObject now = Instantiate(ItemsBase[randomItemAIndex], transform.position, transform.rotation);
                
            AI.Add(now2);
            AI.Add(now2);
            AI.Add(now2);
            //AI.Add(now);

        }
        */
        
        for (int k = 0; k < NOP; k++)
        {
            
            int randomItemAIndex = Random.Range(0, ItemsBase.Count);
            
            for (int k1 = 0; k1 < SZ; k1++)
            {
                
                GameObject now2 = Instantiate(ItemsBase[randomItemAIndex], transform.position, transform.rotation);
                
                //GameObject now = Instantiate(ItemsBase[randomItemAIndex], transform.position, transform.rotation);
                
                AI.Add(now2);
                
            }
        }
        
        Debug.Log("_______3________");

        
        // Перемешиваем

        // Проходим по списку в обратном порядке
        for (int i = AI.Count - 1; i >= 1; i--)
        {
            // Генерируем случайный индекс от 0 до i
            int j = Random.Range(0, i + 1);

            // Обменяем значения между индексами j и i
            GameObject temp = AI[j];
            AI[j] = AI[i];
            AI[i] = temp;
        }

        Debug.Log("_______4________");
        

        


        while (AI.Count >0)
        {
            for (int p = 0; p < SIA.Count && AI.Count > 0; p++)
            {
                SIA[p].IW.Add(new ItemWave());

                int nW = SIA[p].IW.Count - 1;
                Debug.Log(nW);

                for (int m = 0; m < Random.Range(1, SZ+1) && AI.Count > 0; m++)
                {
                    //SIA[p].IW[nW].Items.Add(AI[0]);
                    //AI.RemoveAt(0);
                    
                    if(SIA[p].IW[nW].Items.Count == SZ - 1)
                    {
                        bool flag = true;
                        for (int f = 0; f < SIA.Count && AI.Count > 0 && flag; f++)
                        {
                            if (SIA[p].IW[nW].Items[0] != AI[f])
                            {
                                SIA[p].IW[nW].Items.Add(AI[f]);
                                AI.RemoveAt(f);
                                flag = false;
                            }
                        }
                    }
                    else
                    {
                        SIA[p].IW[nW].Items.Add(AI[0]);
                        AI.RemoveAt(0);
                    }
                    

                }
            }
        }
        for (int s = 0; s < SIA.Count; s++)
        {
            for (int w = 0; w < SIA[s].IW.Count; w++)
            {
                SIA[s].S.GetComponent<CleverShelf>().SetWawe(SIA[s].IW[w].Items);
            }
            SIA[s].S.GetComponent<CleverShelf>().PlaysItems();
        }
        
        /*
        int k11 = 0;
        while (k11 < NOI)
        {
            for (int k = 0; k < NOS; k++)
            {
                for (int k1 = 0; k1 < Random.Range(0, SZ); k1++)
                {
                    if (SIA[k].IW == null)
                    {
                        SIA[k].IW.Add(new ItemWave());
                        SIA[k].IW[0].Items.Add(AI[k11]);
                    }
                    else
                    {
                        int a = SIA[k].IW.Count;
                        SIA[k].IW.Add(new ItemWave());
                        SIA[k].IW[a].Items.Add(AI[k11]);
                    }
                    /*
                    int a = SIA[k].IW.Count;
                    SIA[k].IW.Add(new ItemWave());
                    SIA[k].IW[a].Items.Add(AI[k11]);
                    //
                    k11++;
                    if (k11 >= NOI)
                    {
                        k1 = 999999;
                    }
                }
                if (k11 >= NOI)
                {
                    k = NOS;
                }
            }
            
        }
*/
        Debug.Log("_______5________");
        
    }
}
