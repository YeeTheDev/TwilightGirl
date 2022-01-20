using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : MonoBehaviour
{
    [SerializeField] float speed = 3f;

    public float Speed { get => speed; }

    public abstract bool IsRigidbodyEnabled();

    public abstract void Move(float xAxis, float zAxis);
    public abstract void SetBehaviorOnMovement(bool use3DMovement);
}
