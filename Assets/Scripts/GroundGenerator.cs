using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerator : MonoBehaviour
{
    // config parameters
    [field: SerializeField] private GameObject RoadTilePrefab { get; set; }
    [field: SerializeField] private int NumberOfTiles { get; set; }
    
    // cached references
    private GameObject Toast { get; set; }
    
    // properties
    private float TileWidth => RoadTilePrefab.GetComponent<SpriteRenderer>().bounds.size.x;
    private int TilesGenerated { get; set; }
    private float GroundMiddle { get; set; }

    private void Awake()
    {
        Toast = GameObject.FindGameObjectWithTag("Toast");
    }

    private void Start()
    {
        for(TilesGenerated = 0; TilesGenerated < NumberOfTiles; TilesGenerated++)
        {
            Instantiate(RoadTilePrefab, transform.position + Vector3.right * TileWidth * TilesGenerated, Quaternion.identity, transform);
        }
        GroundMiddle = transform.position.x + TileWidth * (NumberOfTiles / 2);
    }

    private void Update()
    {
        if(Toast.transform.position.x > GroundMiddle)
        {
            Instantiate(RoadTilePrefab, transform.position + Vector3.right * TileWidth * TilesGenerated, Quaternion.identity, transform);
            TilesGenerated++;
            GroundMiddle += TileWidth;
        }
    }
}
