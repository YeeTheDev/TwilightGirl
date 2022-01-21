using System;
using UnityEngine;
using TG.Abilities;
using TG.Movement;

namespace TG.Controls
{
    [RequireComponent(typeof(Swapper))]
    public class Control : MonoBehaviour
    {
        [Header("2D Ground Check")]
        [SerializeField] Collider2D colliderChecker = null;

        [Header("3D Ground Check")]
        [SerializeField] float checkerRadius = 0;
        [SerializeField] Transform groundChecker = null;

        [Header("Ground Check Common Settings")]
        [SerializeField] LayerMask groundLayer = 0;

        bool lastPressedHBtn;
        float xAxis;
        float zAxis;
        Mover mover;
        Swapper swapper;

        private void Awake() { swapper = GetComponent<Swapper>(); }
        private void Start() { mover = swapper.GetActiveMover(); }

        void Update()
        {
            CheckLastButtonPressed();
            SetAxis();
            ReadSwapInput();
            ReadJumpInput();
        }

        private void FixedUpdate() { mover.Move(xAxis, zAxis); }

        private void CheckLastButtonPressed()
        {
            bool holdHBtn = Input.GetButton("Horizontal");
            bool holdVBtn = Input.GetButton("Vertical");

            if (holdHBtn && !holdVBtn || holdVBtn && Input.GetButtonDown("Horizontal")) { lastPressedHBtn = true; }
            if (!holdHBtn && holdVBtn || holdHBtn && Input.GetButtonDown("Vertical")) { lastPressedHBtn = false; }
        }

        private void SetAxis()
        {
            xAxis = lastPressedHBtn ? Input.GetAxisRaw("Horizontal") : 0;
            zAxis = !lastPressedHBtn ? Input.GetAxisRaw("Vertical") : 0;
        }

        //TODO Add fancy animation
        private void ReadSwapInput()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                swapper.Swap();
                mover = swapper.GetActiveMover();
            }
        }

        private void ReadJumpInput()
        {
            if (Input.GetButtonDown("Jump") && IsGrounded()) { mover.Jump(false); }
            else if (Input.GetButtonUp("Jump")) { mover.Jump(true); }
        }

        private bool IsGrounded()
        {
            bool using3DMover = mover.GetType().Equals(typeof(Mover3D));

            bool grounded;
            if (using3DMover) { grounded = Physics.CheckSphere(groundChecker.position, checkerRadius, groundLayer); }
            else { grounded = colliderChecker.IsTouchingLayers(groundLayer); }

            return grounded;
        }

        private void OnDrawGizmos()
        {
            if (groundChecker == null) { return; }

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(groundChecker.position, checkerRadius);
        }
    }
}
