using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public playerController playerController;

    public static GameManager Instance;

    public bool isGameOver = false;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>();
        playerController.OnEntityDeath += CheckGameOver;
    }

    private void Update()
    {
        ReloadOnPlayerDeath();
    }

    public void CheckGameOver()
    {
        isGameOver = true;
        gameOverUI.SetActive(true);
    }

    public void ReloadOnPlayerDeath()
    {
        if(isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            isGameOver = false;
            gameOverUI.SetActive(false);
            SceneManager.LoadScene(0);
        }
        
    }


}
