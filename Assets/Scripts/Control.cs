using UnityEngine;
using TG.Abilities;
using TG.Movement;

namespace TG.Controls
{
    [RequireComponent(typeof(PlaneSwapper))]
    public class Control : MonoBehaviour
    {
        [SerializeField] float checkerRadius = 0;
        [SerializeField] Transform ground3DChecker = null;
        [SerializeField] Transform shadowGroundChecker = null;
        [SerializeField] LayerMask groundLayer = 0;

        bool lastPressedHBtn;
        float xAxis;
        float zAxis;
        Transform groundChecker;

        Mover mover;
        PlaneSwapper planeSwapper;

        private void Awake()
        {
            groundChecker = ground3DChecker;

            mover = GetComponent<Mover>();
            planeSwapper = GetComponent<PlaneSwapper>();
        }

        void Update()
        {
            CheckLastButtonPressed();
            SetAxis();
            ReadJumpInput();
            ReadSwapInput();
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
            zAxis = !lastPressedHBtn && !planeSwapper.InShadowRealm ? Input.GetAxisRaw("Vertical") : 0;
        }

        private void ReadJumpInput()
        {
            if (Input.GetButtonDown("Jump") && IsGrounded()) { mover.Jump(false); }
            else if (Input.GetButtonUp("Jump")) { mover.Jump(true); }
        }

        private bool IsGrounded() { return Physics.CheckSphere(groundChecker.position, checkerRadius, groundLayer); }

        private void ReadSwapInput()
        {
            if (Input.GetKeyDown(KeyCode.K) && IsGrounded())
            {
                planeSwapper.SwapPlane();
                groundChecker = planeSwapper.InShadowRealm ? shadowGroundChecker : ground3DChecker;
            }
        }

        private void OnDrawGizmos()
        {
            if (ground3DChecker == null || shadowGroundChecker == null) { return; }

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(ground3DChecker.position, checkerRadius);
            Gizmos.DrawWireSphere(shadowGroundChecker.position, checkerRadius);
        }
    }
}
