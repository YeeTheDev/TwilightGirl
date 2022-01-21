using UnityEngine;
using TG.Abilities;
using TG.Movement;

namespace TG.Controls
{
    [RequireComponent(typeof(Swapper))]
    public class Control : MonoBehaviour
    {
        [SerializeField] Collider2D colliderChecker = null;
        [SerializeField] LayerMask groundLayer = 0;

        float xAxis;
        float zAxis;
        bool holdHBtn;
        bool holdVBtn;
        bool lastPressedHBtn;

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

        private void CheckLastButtonPressed()
        {
            holdHBtn = Input.GetButton("Horizontal");
            holdVBtn = Input.GetButton("Vertical");

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
            if (Input.GetButtonDown("Jump") && CanJump()) { mover.Jump(false); }
            else if (Input.GetButtonUp("Jump")) { mover.Jump(true); }
        }

        private bool CanJump()
        {
            return colliderChecker.IsTouchingLayers(groundLayer);
        }

        private void FixedUpdate() { mover.Move(xAxis, zAxis); }
    }
}
