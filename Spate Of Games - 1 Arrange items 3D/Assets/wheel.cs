using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class wheel : MonoBehaviour
{
    public List<ItemWave> ItemsBase;

    public GameObject[] pose;

    public List<int> NowItems;

    public GameObject Arrow;

    public GameObject Effect;

    public GameObject point;

    public GameObject CanBef;
    public GameObject CanAfter;

    bool spin = false;


    float speed;
    public float Startspeed;
    public float speedStop;



    [System.Serializable]
    public class ItemWave
    {
        public List<GameObject> Items; // предметы
    }


    public void arrange()
    {
        CanBef.SetActive(false);
        for (int k = 0; k < pose.Length; k++)
        {

            int a = Random.Range(0, ItemsBase.Count);
            while (NowItems.IndexOf(a) != -1)
            {
                a = Random.Range(0, ItemsBase.Count);
            }
            NowItems.Add(a);
            Instantiate(ItemsBase[a].Items[Random.Range(0, ItemsBase[a].Items.Count)], pose[k].transform.position, pose[k].transform.rotation);

        }

        spin = true;

        speed = Random.Range(0, Startspeed);
        /*
        // Движение по горизонтали и вертикали
        
        Vector3 moveDirection = new Vector3(1,0,0);
        Vector3 moveVelocity = moveDirection * speed;

        // Применение силы для движения
        Arrow.GetComponent<Rigidbody>().AddForce(moveVelocity);
        */
    }

    // Start is called before the first frame update
    void Start()
    {
        CanBef.SetActive(true);
        CanAfter.SetActive(false);
        
    }

    public void OpenScen()
    {
        Progress.Instance.NextScen();
    }





    // Update is called once per frame
    void Update()
    {
        
    }

    public void generateFin()
    {
        float dist = 99999998f;
        int posMin = 0;
        for (int k = 0; k < pose.Length; k++)
        {
            float a = System.Math.Abs((point.transform.position - pose[k].transform.position).magnitude);
            if (dist > a)
            {
                dist = a;
                posMin = k;
            }

        }
        Instantiate(Effect, pose[posMin].transform.position, pose[posMin].transform.rotation);
        CanBef.SetActive(false);
        CanAfter.SetActive(true);
        Progress.Instance.PlayerInfo.Scen = NowItems[posMin];
    }

    private void FixedUpdate()
    {
        
        if (spin) {
            Arrow.transform.Rotate(Vector3.right * speed); // Space.World
            speed -= speedStop * speed/Startspeed;
        }
        if (speed <=1 && spin)
        {
            spin = false;
            generateFin();
        }
        
    }
}
