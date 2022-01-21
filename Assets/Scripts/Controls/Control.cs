using UnityEngine;
using TG.Abilities;
using TG.Movement;

namespace TG.Controls
{
    [RequireComponent(typeof(Swapper))]
    public class Control : MonoBehaviour
    {
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

        private void FixedUpdate() { mover.Move(xAxis, zAxis); }
    }
}
