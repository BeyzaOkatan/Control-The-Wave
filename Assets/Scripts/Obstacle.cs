using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public List<GameObject> obstacles;
    public GameObject wave;
    GameObject movingObject;
    Wave waveScript;
    bool isClose = false;
    bool isGettingFar = false;
    int[] distance = { 1, 2, 3, -1, -2, -3 };
    Vector3 initialPos;
    public float speed;
    public float increment;
    public Material[] materials;
    void Start()
    {
        waveScript = wave.GetComponent<Wave>();
    }

    void Update()
    {
        if (waveScript.currentAmplitude==0 && !isClose)
        {
            int[] arr = { 10, -10 };
            int rand = Random.Range(0, obstacles.Count);
            movingObject = obstacles[rand];
            int rand2 = Random.Range(3, 18);
            int rand3 = Random.Range(0, 2);
            initialPos = new Vector3(rand2, arr[rand3], 0);
            movingObject.transform.position = initialPos;
            do
            {
                rand = Random.Range(0, materials.Length);
                movingObject.GetComponent<Renderer>().material = materials[rand];
            } while (movingObject.name == "cube" && materials[rand].name == "white");

            if (movingObject.gameObject.tag == "sharp")
            {
                if (initialPos.y > 0)
                {
                    movingObject.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
                }
                else
                {
                    movingObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);

                }
            }
            else if (movingObject.gameObject.name == "cube")
            {
                if (initialPos.y > 0)
                {
                    movingObject.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
                }
                else
                {
                    movingObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);

                }
            }
            isClose = true;
            isGettingFar = false;
        }

        if (isClose)
        {
            Vector3 targetPosition;

            if (!isGettingFar)
            {
                targetPosition = new Vector3(movingObject.transform.position.x, wave.transform.position.y, movingObject.transform.position.z);
            }
            else
            {
                targetPosition = initialPos;
            }

            float step = speed * Time.deltaTime;

            movingObject.transform.position = Vector3.MoveTowards(movingObject.transform.position, targetPosition, step);

            if (Mathf.Approximately(movingObject.transform.position.y, targetPosition.y))
            {

                if (targetPosition != initialPos)
                {
                    isGettingFar = true;
                }
                else
                {
                    isGettingFar = false;
                    isClose = false;
                }
            }
            speed += increment * Time.deltaTime;
        }
    }
}