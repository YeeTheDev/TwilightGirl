using UnityEngine;
using TG.Animations;

namespace TG.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Animater))]
    public class Mover : MonoBehaviour
    {
        [SerializeField] float speed = 3f;
        [SerializeField] float jumpForce = 6f;

        Animater animater;
        Rigidbody rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            animater = GetComponent<Animater>();
        }

        public bool IsRigidbodyEnabled() { return !rb.isKinematic; }
        public float GetYVelocity() { return rb.velocity.y; }

        public void Move(float xAxis, float zAxis)
        {
            animater.SetWalking(xAxis != 0 || zAxis != 0);
            rb.velocity = CalculateDirection(xAxis, zAxis);
        }

        private Vector3 CalculateDirection(float xAxis, float zAxis)
        {
            Vector3 moveDirection = Vector3.one * rb.velocity.y;
            moveDirection.x = xAxis * speed;
            moveDirection.z = zAxis * speed;

            return moveDirection;
        }

        public void SetBehaviorOnMovement(bool use3DMovement)
        {
            rb.isKinematic = !use3DMovement;
            rb.velocity = Vector3.zero;
        }

        public void Jump(bool halt)
        {
            if (!halt) { animater.PlayJumpAnimation(); }
            rb.velocity = CalculateJumpForce(halt);
        }

        private Vector3 CalculateJumpForce(bool halt)
        {
            Vector3 jumpVelocity = rb.velocity;
            jumpVelocity.y = halt ? rb.velocity.y > 0 ? jumpVelocity.y * 0.5f : rb.velocity.y : jumpForce;

            return jumpVelocity;
        }
    }
}