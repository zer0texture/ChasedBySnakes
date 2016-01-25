using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TunnelSpawner : MonoBehaviour {

    public const int Max_Tunnel = 100;
    const int NUMBER_OF_TUNNELS = 7;
    List<GameObject> tunnel;
    Quaternion nullQuart;
    GameObject tempOBJ;

	// Use this for initialization
	void Start () {
        tunnel = new List<GameObject>();

        print("Started tunnel 0");
        tempOBJ = (Resources.Load("TunnelSections/" + Random.Range(1, NUMBER_OF_TUNNELS)) as GameObject);
        tempOBJ.transform.position = transform.position;
        tunnel.Add(Instantiate(tempOBJ));
        //tunnel[0].transform.position.Set()
        print("created tunnel 0");

        for (int i = 1; i < Max_Tunnel; i++)
        {
            print("Started tunnel " + i);
            tempOBJ = (Resources.Load("TunnelSections/" + Random.Range(1, NUMBER_OF_TUNNELS)) as GameObject);
            tunnel.Add(Instantiate(tempOBJ));
            tunnel[i].name = ("tunnel " + i);
            print("created tunnel" + i);
            tunnel[i].transform.position = tunnel[i - 1].transform.position;
            tunnel[i].gameObject.transform.Translate((tunnel[i].gameObject.GetComponent<Renderer>().bounds.extents.x + tunnel[i - 1].gameObject.GetComponent<Renderer>().bounds.extents.x), 0, 0);
            
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
