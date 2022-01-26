using UnityEngine;

namespace TG.Animations
{
    public class Animater : MonoBehaviour
    {
        [SerializeField] float timeToTriggerIdleSpecial = 5f;
        [SerializeField] Animator meshAnimator = null;
        [SerializeField] Animator shadowAnimator = null;

        [Header("Parameters' Name")]
        [SerializeField] string idleParameter = "Idle_1_Trigger";
        [SerializeField] string walkParameter = "Walking";
        [SerializeField] string jumpParameter = "Jump";
        [SerializeField] string yVelocityParameter = "Y_Velocity";
        [SerializeField] string swapParameter = "Swap";
        [SerializeField] string groundedParameter = "Grounded";

        float timer;

        public void PlaySpecialIdleAnimation()
        {
            timer += Time.deltaTime;
            if (timer > timeToTriggerIdleSpecial)
            {
                timer = 0;
                SetTrigger(idleParameter);
            }
        }

        public void PlayJumpAnimation() { SetTrigger(jumpParameter); }
        public void PlaySwapAnimation() { SetTrigger(swapParameter); }

        public void SetWalking(bool state) { SetBool(walkParameter, state); }
        public void SetGrounded(bool state) { SetBool(groundedParameter, state); }

        public void SetYVelocity(float yVelocity)
        {
            meshAnimator.SetFloat(yVelocityParameter, yVelocity);
            shadowAnimator.SetFloat(yVelocityParameter, yVelocity);
        }

        private void SetTrigger(string parameter)
        {
            meshAnimator.SetTrigger(parameter);
            shadowAnimator.SetTrigger(parameter);
        }

        private void SetBool(string parameter, bool state)
        {
            meshAnimator.SetBool(parameter, state);
            shadowAnimator.SetBool(parameter, state);
        }
    }
}