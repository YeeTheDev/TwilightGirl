using UnityEngine;
using TG.Abilities;
using TG.Movement;
using TG.Animations;
using TG.Physic;

namespace TG.Controls
{
    [RequireComponent(typeof(PlaneSwapper))]
    public class Control : MonoBehaviour
    {
        bool lastPressedHBtn;
        float xAxis;
        float zAxis;

        Mover mover;
        PlaneSwapper planeSwapper;
        Animater animater;
        Collisioner collisioner;

        private void Awake()
        {
            mover = GetComponent<Mover>();
            planeSwapper = GetComponent<PlaneSwapper>();
            animater = GetComponent<Animater>();
            collisioner = GetComponent<Collisioner>();
        }

        void Update()
        {
            CheckLastButtonPressed();
            SetAxis();
            ReadJumpInput();
            ReadSwapInput();
            mover.SetStartFallDistance(collisioner.IsGrounded());
        }

        private void FixedUpdate() { MoveCharacter(); }

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
            if (Input.GetButtonDown("Jump") && collisioner.IsGrounded()) { mover.Jump(false); }
            else if (Input.GetButtonUp("Jump")) { mover.Jump(true); }
        }

        private void ReadSwapInput()
        {
            if (Input.GetKeyDown(KeyCode.K) && collisioner.IsGrounded())
            {
                planeSwapper.SwapPlane();
                animater.PlaySwapAnimation();
            }
        }

        private void MoveCharacter()
        {
            mover.Move(xAxis, zAxis);
            animater.SetYVelocity(mover.GetYVelocity());
            animater.SetGrounded(collisioner.IsGrounded());
            animater.RotateCharacter(xAxis, zAxis);

            if (Mathf.Approximately(xAxis, 0) && Mathf.Approximately(zAxis, 0)) { animater.PlaySpecialIdleAnimation(); }
        }
    }
}
