using UnityEngine;

namespace TG.Movement
{
    public abstract class Mover : MonoBehaviour
    {
        [SerializeField] float speed = 3f;
        [SerializeField] float jumpForce = 50f;

        public float Speed { get => speed; }
        public float JumpForce { get => jumpForce; }

        public abstract bool IsRigidbodyEnabled();

        public abstract void Move(float xAxis, float zAxis);
        public abstract void SetBehaviorOnMovement(bool use3DMovement);
        public abstract void Jump(bool halt);
    }
}