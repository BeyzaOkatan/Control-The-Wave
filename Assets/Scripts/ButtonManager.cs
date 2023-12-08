using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ButtonManager : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject gamePlayScreen;
    public GameObject prefab;
    public int length;
    public TextMeshProUGUI scoreText;
    public GameObject pauseScreen;

    private void Start()
    {
        Time.timeScale = 0.0f;
    }
    public void StartGame()
    {
        startPanel.SetActive(false);
        Time.timeScale = 1.0f;
        gamePlayScreen.SetActive(true);

        for (int i = 0; i < length; i++)
        {
            int rand = Random.Range(-5, 6);
            int rand2 = Random.Range(0, 20);
            Instantiate(prefab, new Vector3(rand2, rand, 5), Quaternion.Euler(-90, 0, 0));
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseScreen.SetActive(true);
    }

    public void continueGame()
    {
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
    }

    public void restartGame()
    {
        CollisionDetect.score = 0;
        SceneManager.LoadScene(0);
    }


}
