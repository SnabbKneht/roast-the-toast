using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    // config parameters
    [field: SerializeField]
    [field: Tooltip("0 means the object will be static, 1 means the object will be bound to the camera.")]
    [field: Range(0f, 1f)]
    private float BoundForce { get; set; }

    // cached references
    private Camera Camera { get; set; }
    private float SpriteWidth => GetComponent<SpriteRenderer>().bounds.size.x;
    
    // properties
    private Vector3 PrevCamPos { get; set; }
    private Vector3 CurrentCamPos { get; set; }
    private Vector3 CameraMove => CurrentCamPos - PrevCamPos;
    private Vector3 StartPos { get; set; }
    private Vector3 CurrentOffset { get; set; }
    
    private void Awake()
    {
        Camera = Camera.main;
    }

    private void Start()
    {
        PrevCamPos = Camera.transform.position;
        StartPos = transform.position;
        CurrentOffset = Vector3.zero;
    }

    private void Update()
    {
        CurrentCamPos = Camera.transform.position;

        var move = CameraMove * BoundForce;
        transform.position += move;
        CurrentOffset += move - CameraMove;
        if(CurrentOffset.x < -SpriteWidth)
        {
            transform.position += Vector3.right * SpriteWidth;
            StartPos = transform.position;
            CurrentOffset = Vector3.zero;
        }
        else if(CurrentOffset.x > SpriteWidth)
        {
            transform.position += Vector3.left * SpriteWidth;
            StartPos = transform.position;
            CurrentOffset = Vector3.zero;
        }

        PrevCamPos = CurrentCamPos;
    }
}
