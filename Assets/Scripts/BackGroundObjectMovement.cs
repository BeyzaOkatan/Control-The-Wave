using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundObjectMovement : MonoBehaviour
{
    public float speed = 2f;
    public float increment = 0.1f;
    void Update()
    {
        speed += increment * Time.deltaTime;
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        if (transform.position.x < 0)
        {
            float rand = Random.Range(-5, 6);
            transform.position = new Vector3(20, rand, 5);
        }
    }
}
