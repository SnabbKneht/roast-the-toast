using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class ToastStatus : MonoBehaviour
{
    [field: SerializeField] public int Health { get; private set; }
    [field: SerializeField] private Sprite Toast1 { get; set; }
    [field: SerializeField] private Sprite Toast2 { get; set; }
    [field: SerializeField] private Sprite Toast3 { get; set; }
    [field: SerializeField] private Sprite Toast4 { get; set; }
    [field: SerializeField] private Sprite FaceHappy { get; set; }
    [field: SerializeField] private Sprite FaceO { get; set; }
    [field: SerializeField] private Sprite FaceHurt { get; set; }
    [field: SerializeField] private Sprite FaceDead { get; set; }

    private Animator animator;
    private SpriteRenderer Renderer { get; set; }
    [SerializeField] private SpriteRenderer face;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        Renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Renderer.sprite = Health switch
        {
            3 => Toast1,
            2 => Toast2,
            1 => Toast3,
            0 => Toast4,
            _ => Renderer.sprite
        };
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Toaster"))
        {
            col.GetComponentInParent<Animator>().SetTrigger("Activate");
            StartCoroutine(GetComponent<ToastMovement>().Freeze());
            EventManager.Instance.RaiseOnToastHit();
            animator.SetTrigger("Toaster");
            Health--;
        }
        else if(col.gameObject.CompareTag("Car"))
        {
            StartCoroutine(GetComponent<ToastMovement>().Freeze());
            GetComponent<Rigidbody2D>().AddForce(col.GetComponentInParent<Car>().KnockbackVector);
        }
        
        if(Health <= 0)
        {
            EventManager.Instance.RaiseOnGameOver();
            face.sprite = FaceDead;
        }
    }

    // private IEnumerator SetHurtFace()
    // {
    //     face.sprite = FaceHurt;
    //     yield return new WaitForSeconds(2f);
    //     face.sprite = FaceHappy;
    // }
}
