using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampMovement : MonoBehaviour
{
    public float speed = 5.0f;

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
