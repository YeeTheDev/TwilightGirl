using UnityEngine;

namespace TG.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class Mover : MonoBehaviour
    {
        [SerializeField] float speed = 3f;
        [SerializeField] float jumpForce = 6f;

        Rigidbody rb;

        private void Awake() { rb = GetComponent<Rigidbody>(); }

        public bool IsRigidbodyEnabled() { return !rb.isKinematic; }
        public void Move(float xAxis, float zAxis) { rb.velocity = CalculateDirection(xAxis, zAxis); }

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

        public void Jump(bool halt) { rb.velocity = CalculateJumpForce(halt); }

        private Vector3 CalculateJumpForce(bool halt)
        {
            Vector3 jumpVelocity = rb.velocity;
            jumpVelocity.y = halt ? rb.velocity.y > 0 ? jumpVelocity.y * 0.5f : rb.velocity.y : jumpForce;

            return jumpVelocity;
        }
    }
}