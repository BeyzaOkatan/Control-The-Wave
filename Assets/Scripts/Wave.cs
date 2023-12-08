using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public int numberOfPoints = 1000;
    public float waveFrequency = 1f;
    public float maxAmplitude = 3f;
    public float lerpSpeed = 1f;
    public int step = 50;
    public float currentAmplitude = 0f;
    private float targetAmplitude = 0f;
    GameObject closestObstacle;
    public List<GameObject> obstacles;
    private int closestPointIndex = -1;
    public List<GameObject> colliderPoints;
    public float colliderSize = 0.05f;
    //public float increment;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = numberOfPoints;
        InitializeLineRenderer();
        closestObstacle = obstacles[0];
    }
    void InitializeLineRenderer()
    {
        Vector3 startPosition = lineRenderer.GetPosition(0); 
        Vector3 endPosition = lineRenderer.GetPosition(1);

        for (int i = 0; i < numberOfPoints; i++)
        {
            float t = i / (float)(numberOfPoints - 1);
            Vector3 newPosition = Vector3.Lerp(startPosition, endPosition, t);
            lineRenderer.SetPosition(i, newPosition);

            GameObject colliderPoint = new GameObject("ColliderPoint" + i);
            colliderPoint.transform.parent = transform;
            colliderPoint.transform.position = newPosition;
            colliderPoint.gameObject.tag = "Player";
            BoxCollider boxCollider = colliderPoint.AddComponent<BoxCollider>();
            boxCollider.size = new Vector3(colliderSize, colliderSize, colliderSize);
            boxCollider.isTrigger= true;
            colliderPoints.Add(colliderPoint); 
        }
    }


    private void Update()
    {
        
        if (Input.GetKey(KeyCode.UpArrow))
        {
            targetAmplitude = maxAmplitude;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            targetAmplitude = -1 * maxAmplitude;
        }
        else
        {
            targetAmplitude = 0f;

            if (Mathf.Abs(currentAmplitude) < 0.05f)
            {
               currentAmplitude = 0f;
               FindClosestGameObject();

            }
            
        }
       // lerpSpeed += increment * Time.deltaTime;
        currentAmplitude = Mathf.Lerp(currentAmplitude, targetAmplitude, Time.deltaTime * lerpSpeed);

        GenerateWave();
        UpdateColliderPointsPositions();
    }
    
    void FindClosestGameObject()
    {
        foreach (GameObject obstacle in obstacles)
        {
            bool isMoving = obstacle.GetComponent<CollisionDetect>().isMoving;

            if (isMoving)
            {
                closestObstacle = obstacle;
            }
        }

    }
    void GenerateWave()
    {
        float closestDistance = 1000;
        if (currentAmplitude == 0)
        {
            for (int i = 0; i < numberOfPoints; i++)
            {
                Vector3 currentPosition = lineRenderer.GetPosition(i);
                
                    float distance = Vector3.Distance(currentPosition, closestObstacle.transform.position);

                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestPointIndex = i;
                    }
                                
            }
        }

        int middleIndex = closestPointIndex;

        for (int i = 0; i < numberOfPoints; i++)
        {
            if (i >= middleIndex - step && i <= middleIndex + step)
            {
                float t = Mathf.InverseLerp(middleIndex - step, middleIndex + step, i);
                Vector3 currentPosition = lineRenderer.GetPosition(i);
                currentPosition.y = Mathf.Sin(t * Mathf.PI * waveFrequency) * currentAmplitude;
                lineRenderer.SetPosition(i, currentPosition);
            }
            else  // ...
            {
                Vector3 currentPosition = lineRenderer.GetPosition(i);
                currentPosition.y = 0;
                lineRenderer.SetPosition(i, currentPosition);
            }
        }
        
    }

    void UpdateColliderPointsPositions()
    {
        for (int i = 0; i < numberOfPoints; i++)
        {
            Vector3 newPosition = lineRenderer.GetPosition(i);
            colliderPoints[i].transform.position = newPosition;
        }
    }


}