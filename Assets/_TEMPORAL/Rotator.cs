using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 3f;

    void Update()
    {
        transform.Rotate(transform.up * rotationSpeed * Time.deltaTime);    
    }
}
