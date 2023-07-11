using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class MusicPlayer : MonoBehaviour
{
    [field: SerializeField] private List<AudioClip> Clips { get; set; }
    private AudioSource AudioSource { get; set; }

    private void Awake()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        var random = new Random();
        AudioSource.clip = Clips[random.Next(Clips.Count)];
        AudioSource.Play();
    }
}
