using UnityEngine;
using UnityEngine.UI;
using TG.Core;

namespace TG.UserInterface
{
    [RequireComponent(typeof(Animator))] 
    public class Fader : MonoBehaviour
    {
        [SerializeField] Image fadeImage = null;
        [SerializeField] string fadeParameter = "Fade";
        [SerializeField] AnimationClip fadeAnimationClip = null;

        Animator animator;
        SceneEnder sceneEnder;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            sceneEnder = GameObject.FindGameObjectWithTag("Loader").GetComponent<SceneEnder>();

            if (sceneEnder != null) { sceneEnder.onSceneEnd += DelayedFade; }
        }

        public void DelayedFade() { Invoke("Fade", 1f); }

        public void Fade() { animator.SetTrigger(fadeParameter); }
    }
}