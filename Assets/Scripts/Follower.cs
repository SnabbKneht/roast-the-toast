using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Follower : MonoBehaviour
{
    [field: SerializeField] private Transform ObjectToFollow { get; set; }
    [field: SerializeField] private Vector2 Offset { get; set; }

    private void FixedUpdate()
    {
        transform.position = new Vector3(ObjectToFollow.position.x,
            transform.position.y,
            transform.position.z)
            + (Vector3)Offset;
    }
}
