using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CreateTerrain : MonoBehaviour
{
    public GameObject terrain;
    private GameObject main;
    private GameObject main1;
    private GameObject main2;
    private GameObject main3;

    private GameObject player;

    void Start()
    {
        // player = GameObject.FindWithTag("Player");
        // main = Instantiate(terrain);
        // main1 = Instantiate(terrain, new Vector3(terrain.transform.position.x, terrain.transform.position.y - 120, 0),
        //     Quaternion.identity);
        // main2 = Instantiate(terrain,
        //     new Vector3(terrain.transform.position.x - 120, terrain.transform.position.y - 120, 0),
        //     Quaternion.identity);
        // main3 = Instantiate(terrain, new Vector3(terrain.transform.position.x - 120, terrain.transform.position.y, 0),
        //     Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
