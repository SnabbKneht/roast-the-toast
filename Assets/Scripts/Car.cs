using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [field: SerializeField] private float Speed { get; set; }
    [field: SerializeField] private float GroundCheckDistance { get; set; }
    private Rigidbody2D Rb { get; set; }
    public bool IsGrounded
    {
        get
        {
            var hit = Physics2D.Raycast(transform.position,
                Vector2.down,
                GroundCheckDistance,
                LayerMask.GetMask("Ground"));
            return hit.collider != null;
        }
    }
    private bool Landed { get; set; }
    [field: SerializeField] public Vector2 KnockbackVector { get; set; }

    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(IsGrounded && !Landed)
        {
            Landed = true;
        }
        else if(Landed)
        {
            Rb.velocity = Vector2.left * Speed;
        }
    }
}
