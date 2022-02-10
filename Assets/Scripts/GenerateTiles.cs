using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTiles : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject TilesPrefabs,DiamondPrefabs,StarPrefabs;
    bool isjumping = false;
    public float x,y,z;
    public float lastz,curz;
    public float minrange,maxrange,dif;
    public float SpawnTime,DiamondTime,StarTime;
    public List<GameObject> MyTiles,MyDiamond,MyStar;
    public List<Vector3> TilesPosition;
    private float PlayerSpeedUpy,PlayerSpeedDowny,PlayerSpeedz,currentz,need,PLayerSpeedx;
    private int cnt=0,triggercount=0;
    private float []differ;
    private float eps = 1e-4f,havey=0f;
    void Start()
    {
        //initializing players value
        PlayerSpeedUpy =0.15f;currentz=-2;PlayerSpeedz = 0.1f;PLayerSpeedx = 0.2f;
        need =-2f;PlayerSpeedDowny = -0.15f;

        //initializing for Objects
        x=0f;y=-0.3f;z=-2.5f;curz=-2f;dif=33.6f;
        minrange=-1.4f;maxrange=1.4f;
        SpawnTime = 1f;DiamondTime = 10f;StarTime = 8f;

        //initializing Lists
        MyTiles =  new List<GameObject>();
        MyDiamond = new List<GameObject>();
        MyStar =  new List<GameObject>();
        TilesPosition = new List<Vector3>();
        differ = new float[7];
        differ[0]=2.62f;
        differ[1]=3.06f;
        differ[2]=2.4f;
        differ[3]=3.1f;
        differ[4]=2f;
        differ[5]=2.5f;
        differ[6]=3f;

        //Creating Objects
        SpawnTilesInit();
        StartCoroutine(CreateTiles());
        StartCoroutine(CreateStar());
        StartCoroutine(CreateDiamond());
    }

    // Update is called once per frame

    private void Update() {

        //Player's Moving
        Vector3 MovePlayer = Vector3.zero;

        MovePlayer.x = Input.GetAxis("Horizontal")/4.5f;

        if(havey>need)isjumping=false;
        if(isjumping)
        {
            MovePlayer.y = PlayerSpeedUpy;
            havey+=PlayerSpeedUpy;
        }
        else 
        {
            MovePlayer.y = PlayerSpeedDowny;
            havey+=PlayerSpeedDowny;
        }

        if(transform.position.y>=2f && isjumping==true)MovePlayer.y = 0f;
        MovePlayer.z = PlayerSpeedz;
        transform.position += MovePlayer;
        if(transform.position.y < -1)
        {
            Application.Quit();
            Debug.Log("Game over");
        }

        //Destroying Tiles
        if(MyTiles.Count!=0)
        {
            Vector3 position = MyTiles[0].transform.position;

            if(position.z < 
            GameObject.Find("camera").GetComponent<Camera>().transform.position.z)
            {
                GameObject go = MyTiles[0];
                MyTiles.RemoveAt(0);
                if(cnt>6)
                {
                    Destroy(go);
                }
                else cnt++;
            }
        }

        //Destroying Diamonds
        if(MyDiamond.Count!=0)
        {
            GameObject diamond = MyDiamond[0];
            if(diamond==null)
            {
                MyDiamond.RemoveAt(0);
            }
            else
            {
                Vector3 DiamondPosition =  MyDiamond[0].transform.position;

                if(DiamondPosition.z < 
                GameObject.Find("camera").GetComponent<Camera>().transform.position.z)
                {
                    MyDiamond.RemoveAt(0);
                    Destroy(diamond);
                }
            }
        }

        //Destroying Stars
        if(MyStar.Count!=0)
        {
            GameObject star = MyStar[0];
            if(star==null)
            {
                MyStar.RemoveAt(0);
            }
            else
            {
                Vector3 StarPosition =  MyStar[0].transform.position;

                if(StarPosition.z < 
                GameObject.Find("camera").GetComponent<Camera>().transform.position.z)
                {
                    
                    MyStar.RemoveAt(0);
                    Destroy(star);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "platform")
        {
            if(triggercount%2==0 && triggercount!=0)
            {
                triggercount++;
                return;
            }
            triggercount++;
            float nextz = TilesPosition[0].z;
            float dist = nextz-currentz;
            if(Mathf.Abs(dist-differ[0])<=eps)
            {
                need = (dist+1.5f+dist-differ[0])/2;
            }
            else if(Mathf.Abs(dist-differ[1])<=eps)
            {
                need=(dist+1+dist-differ[1])/2;
            }
            else if(Mathf.Abs(dist-differ[2])<=eps)
            {
                need = (dist+1f+dist-differ[2])/2.2f;
            }
            else if(Mathf.Abs(dist-differ[3])<=eps)
            {
                need=(dist+1f+dist-differ[3])/2;
            }
            else if(Mathf.Abs(dist-differ[4])<=eps)
            {
                need=(dist+2.15f+dist-differ[4])/3;
            }
            else if(Mathf.Abs(dist-differ[5])<=eps)
            {
                need=(dist+0.6f+dist-differ[5])/2;
            }
            else if(Mathf.Abs(dist-differ[6])<=eps)
            {
                need=(dist+1+dist-differ[6])/2;
            }

            isjumping = true;
            currentz = nextz;
            TilesPosition.RemoveAt(0);
        }
    }
    void SpawnTilesInit()
    {
        GameObject []tiles = new GameObject[7];
        tiles[0] = GameObject.Find("Tiles1");
        tiles[1] = GameObject.Find("Tiles2");
        tiles[2] = GameObject.Find("Tiles3");
        tiles[3] = GameObject.Find("Tiles4");
        tiles[4] = GameObject.Find("Tiles5");
        tiles[5] = GameObject.Find("Tiles6");
        tiles[6] = GameObject.Find("Tiles7");

        for(int i=0;i<7;i++)
        {
            MyTiles.Add(tiles[i]);

            //if(tiles[i]==null)Debug.Log("Object is NULL at "+i);
            Vector3 position = tiles[i].transform.position;
            TilesPosition.Add(position);
        }
        lastz = tiles[6].transform.position.z;
    }

    private void SpawnTiles()
    {
        //Debug.Log("Lastz "+lastz);
        int index = (int)Random.Range(0,6);
        GameObject go = Instantiate(TilesPrefabs,new Vector3(Random.Range(minrange,maxrange),y,lastz+differ[index]),Quaternion.identity);
        MyTiles.Add(go);
        TilesPosition.Add(go.transform.position);
        lastz = go.transform.position.z;
        
    }

    IEnumerator CreateTiles()
    {
        while(true)
        {
            yield return new WaitForSeconds(SpawnTime);

            SpawnTiles();
        }
    }
    private void SpawnDiamond()
    {
        int num = (int)Random.Range(1f,5f);

        float diamondz = transform.position.z+7f;
        float low=-1.2f,high=1.2f;
        for(int i=1;i<=num;i++)
        {
            GameObject diamond = Instantiate(DiamondPrefabs,new Vector3(Random.Range(low,high),1f,Random.Range(diamondz,diamondz+2)),Quaternion.identity);

            diamondz = diamond.transform.position.z;
            MyDiamond.Add(diamond);
        }
        
    }

    IEnumerator CreateDiamond()
    {
        while(true)
        {
            yield return new WaitForSeconds(DiamondTime);

            SpawnDiamond();
        }
    }

    private void SpawnStar()
    {
        int num = (int)Random.Range(1f,5f);

        float Starz = transform.position.z+10f,low=-1.2f,high=1.2f;

        for(int i=1;i<=num;i++)
        {
            GameObject star = Instantiate(StarPrefabs,new Vector3(Random.Range(low,high),0.98f,Random.Range(Starz,Starz+2)),transform.rotation * Quaternion.Euler(60f,0f,-10f));

            Starz = star.transform.position.z;
            //Vector3 rotate = new Vector3(60f,0f,-10f);
            //star.transform.Rotate(rotate,star);
            MyStar.Add(star);
        }
        
    }

    IEnumerator CreateStar()
    {
        while(true)
        {
            yield return new WaitForSeconds(StarTime);

            SpawnStar();
        }
    }

}
