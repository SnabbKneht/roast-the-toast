using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Gameplay : MonoBehaviour
{
    [SerializeField] private GameObject endScreen;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject toast;

    private void OnEnable()
    {
        EventManager.OnGameOver += ShowEndScreen;
    }

    private void OnDisable()
    {
        EventManager.OnGameOver -= ShowEndScreen;
    }

    public void ShowEndScreen()
    {
        endScreen.SetActive(true);
        scoreText.text = $"Distance: {(int)toast.transform.position.x}m";
        StartCoroutine(WaitAndReturnToMenu());
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    private IEnumerator WaitAndReturnToMenu()
    {
        yield return new WaitForSeconds(5f);
        LoadMenu();
    }
}
