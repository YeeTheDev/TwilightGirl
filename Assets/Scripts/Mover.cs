using UnityEngine;

namespace TG.Movement
{
    public abstract class Mover : MonoBehaviour
    {
        [SerializeField] float speed = 3f;

        public float Speed { get => speed; }

        public abstract bool IsRigidbodyEnabled();

        public abstract void Move(float xAxis, float zAxis);
        public abstract void SetBehaviorOnMovement(bool use3DMovement);
    }
}