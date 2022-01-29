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
        SceneLoader sceneLoader;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            sceneLoader = FindObjectOfType<SceneLoader>();
        }

        public void FadeAndLoad(int sceneToLoad)
        {
            animator.SetTrigger(fadeParameter);
            sceneLoader.StartLoading(fadeAnimationClip.length, sceneToLoad);
        }
    }
}