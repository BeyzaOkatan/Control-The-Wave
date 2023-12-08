using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class CollisionDetect : MonoBehaviour
{
    public bool isMoving = false;
    int same = 0;
    int count = 0;
    HealthSystem healthSystem;
    public static int score = 0;
    // int colliderLength;
    // int a = 0;
    TextMeshProUGUI scoreText;

    private void Start()
    {
        healthSystem = GameObject.Find("GameManager").GetComponent<HealthSystem>();
        transform.position = new Vector3(-20, 50, 0);
        scoreText = GameObject.Find("GameManager").GetComponent<ButtonManager>().scoreText;
      //  colliderLength = transform.GetComponents<Collider>().Length;
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "screen")
        {
           // a = 0;
            count = 1;
        }

        if (other.gameObject.tag == "Player")
        {
            if(same==0)
            {      
                same = 1;
                count = 0;
                healthSystem.heart--;
                if (healthSystem.heart >= 0)
                {
                    healthSystem.hearts[healthSystem.heart].SetActive(false);
                }
                if (healthSystem.heart == 0)
                {
                    healthSystem.endPanel.SetActive(true);
                    healthSystem.pauseButton.gameObject.SetActive(false);
                }
               
            }

        }    

    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "screen")
        {
            score += count;
            scoreText.text = score.ToString();
                
        }
    }

    private void Update()
    {
        if (transform.position.y>-10 && transform.position.y < 10)
        {
            isMoving = true;
            
        }
        else
        {
            isMoving = false;
            same = 0;
        }

    }
}
