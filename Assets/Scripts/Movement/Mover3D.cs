using UnityEngine;

namespace TG.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class Mover3D : Mover
    {
        Rigidbody rb;

        private void Awake() { rb = GetComponent<Rigidbody>(); }

        public override bool IsRigidbodyEnabled() { return !rb.isKinematic; }
        public override void Move(float xAxis, float zAxis) { rb.velocity = CalculateDirection(xAxis, zAxis); }

        private Vector3 CalculateDirection(float xAxis, float zAxis)
        {
            Vector3 moveDirection = Vector3.one * rb.velocity.y;
            moveDirection.x = xAxis * Speed;
            moveDirection.z = zAxis * Speed;

            return moveDirection;
        }

        public override void SetBehaviorOnMovement(bool use3DMovement)
        {
            rb.isKinematic = !use3DMovement;
            rb.velocity = Vector3.zero;
        }

        public override void Jump(bool halt) { rb.velocity = CalculateJumpForce(halt); }

        private Vector3 CalculateJumpForce(bool halt)
        {
            Vector3 jumpVelocity = rb.velocity;
            jumpVelocity.y = halt ? rb.velocity.y > 0 ? jumpVelocity.y * 0.5f : rb.velocity.y : JumpForce;

            return jumpVelocity;
        }
    }
}