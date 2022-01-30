using UnityEngine;
using TG.Abilities;
using TG.Movement;
using TG.Animations;
using TG.Physic;
using TG.Core;
using TG.SceneControl;

namespace TG.Controls
{
    [RequireComponent(typeof(PlaneSwapper))]
    public class Control : MonoBehaviour
    {
        bool isControlEnabled = true;
        bool lastPressedHBtn;
        float xAxis;
        float zAxis;

        Mover mover;
        PlaneSwapper planeSwapper;
        Animater animater;
        Collisioner collisioner;
        SceneEnder sceneEnder;
        Respawner respawner;
        SceneReseter sceneReseter;

        private void EnableMovement() { isControlEnabled = true; }

        private void Awake()
        {
            mover = GetComponent<Mover>();
            planeSwapper = GetComponent<PlaneSwapper>();
            animater = GetComponent<Animater>();
            collisioner = GetComponent<Collisioner>();
            respawner = GetComponent<Respawner>();
            sceneEnder = GameObject.FindGameObjectWithTag("Loader").GetComponent<SceneEnder>();
            sceneReseter = sceneEnder.GetComponent<SceneReseter>();

            sceneEnder.onSceneEnd += StopMovement;
        }

        void Update()
        {
            if (!isControlEnabled) { return; }

            CheckLastButtonPressed();
            SetAxis();
            ReadJumpInput();
            ReadSwapInput();
            ReadRestartInput();
            mover.SetStartFallDistance(collisioner.IsGrounded());
            respawner.CheckFall();
        }

        private void FixedUpdate()
        {
            if (!isControlEnabled) { return; }

            MoveCharacter();
        }

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
                if(planeSwapper.SwapPlane())
                {
                    animater.RotateCharacter(0, 1);
                    animater.PlaySwapAnimation();
                    StopMovement();
                    Invoke("EnableMovement", 0.5f);
                }
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

        private void StopMovement()
        {
            isControlEnabled = false;
            mover.Move(0, 0);
        }

        private void ReadRestartInput() { if (Input.GetKeyDown(KeyCode.R)) { sceneReseter.ResetScene(); } }
    }
}
