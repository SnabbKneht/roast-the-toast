using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D))]
public class ToastMovement : MonoBehaviour
{
    // config parameters
    [field: SerializeField] public float MovementSpeed { get; private set; }
    [field: SerializeField] public Vector2 KnockbackVector { get; private set; }
    [field: SerializeField] public float KnockbackStrength { get; private set; }
    [field: SerializeField] private float GroundCheckDistance { get; set; }
    [field: SerializeField] private float JumpForce { get; set; }
    [field: SerializeField] private Sprite FaceHappy { get; set; }
    [field: SerializeField] private Sprite FaceO { get; set; }
    [field: SerializeField] private Sprite FaceHurt { get; set; }
    [field: SerializeField] private Sprite FaceDead { get; set; }
    
    // cached references
    private Rigidbody2D Rb { get; set; }
    private Animator Animator { get; set; }
    [SerializeField] private SpriteRenderer face;

    private Vector2 MoveVector => Vector2.right * MovementSpeed;

    // properties
    public bool IsRunning { get; private set; }

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
    public bool IsStunned { get; private set; }

    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }

    private void Start()
    {
        StartRunning();
    }

    private void Update()
    {
        if(IsGrounded && !IsRunning)
        {
            StartRunning();
        }
    }

    private void FixedUpdate()
    {
        if(Rb.velocity == MoveVector) return;
        if(!IsGrounded) return;
        if(IsStunned) return;
        
        Rb.velocity = MoveVector;
    }

    private void OnEnable()
    {
        EventManager.OnToastHit += ApplyKnockback;
        EventManager.OnGameOver += DisableMovement;
    }

    private void OnDisable()
    {
        EventManager.OnToastHit -= ApplyKnockback;
        EventManager.OnGameOver -= DisableMovement;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Animator.SetTrigger("Jump");
        if(col.gameObject.CompareTag("DangerAhead"))
        {
            if(Random.value > 0.5f)
            {
                Jump();
                Debug.Log("Trap avoided");
            }
            else
            {
                Debug.Log("Trap not avoided");
            }
        }
    }

    private void ApplyKnockback()
    {
        StopRunning();
        StartCoroutine(Stun());
        Rb.AddForce(KnockbackVector * KnockbackStrength);
    }

    private void Jump()
    {
        Rb.AddForce(Vector2.up * JumpForce);
        StartCoroutine(Stun());
    }

    private void StartRunning()
    {
        Debug.Log("Started running");
        if(IsRunning) return;
        Rb.velocity += MoveVector;
        IsRunning = true;
    }

    private void StopRunning()
    {
        Debug.Log("Stopped running");
        if(!IsRunning) return;
        Rb.velocity -= MoveVector;
        IsRunning = false;
    }

    public IEnumerator Stun()
    {
        IsStunned = true;
        face.sprite = FaceO;
        yield return new WaitForSeconds(1f);
        IsStunned = false;
        face.sprite = FaceHappy;
    }

    public IEnumerator Freeze()
    {
        StartCoroutine(Stun());
        Rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(1f);
        Rb.velocity = MoveVector;
    }

    private void DisableMovement()
    {
        GetComponent<Rigidbody2D>().simulated = false;
        this.enabled = false;
    }
}
