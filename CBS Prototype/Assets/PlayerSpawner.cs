﻿using UnityEngine;
using System.Collections;

public class PlayerSpawner : MonoBehaviour {
    public static GameObject playerInst = null;
    private GameObject cameraInst = null;

    static bool playerSpawned = false;
    public bool pSpawnVis = playerSpawned;
    public GameObject playerPrefab;
    public GameObject cameraPrefab;
    // Use this for initialization

    private float respawnTimer = 0.0f;

    public bool deathDebug = false;


	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        pSpawnVis = playerSpawned;
        if (!playerSpawned)
        {
            playerSpawned = true;
            playerInst = Instantiate(playerPrefab, transform.position, transform.rotation) as GameObject;
            playerInst.transform.rotation.Set(0,gameObject.transform.rotation.y,0,0);   //Makes sure we're facing the right direction when we spawn
            playerInst.name = "loadedPlayer";


            if (cameraInst)
                Destroy(cameraInst);

            Vector3 cameraPos = new Vector3(0,2.0f,-4f);
            cameraPos += transform.position;
            cameraInst = Instantiate(cameraPrefab, cameraPos, transform.rotation) as GameObject;
            cameraV3 myCam = cameraInst.transform.FindChild("Player Camera").GetComponent<cameraV3>();
            myCam.targetPos = playerInst.transform.FindChild("cameraPosition").transform;


            myCam.player = playerInst.transform.FindChild("cameraTarget").transform;

            playerInst.GetComponent<PlayerController>().Screen = cameraInst;            
        }

        if(playerInst == null && playerSpawned)
        {
            respawnTimer += Time.deltaTime;

            if (respawnTimer > 5)
            {
                playerSpawned = false;
                respawnTimer = 0.0f;
            }
        }
	}

    public static bool playerDied()
    {
        return !playerInst && playerSpawned;
    }
}
