using UnityEngine;
using TG.Core;

namespace TG.Audio
{
    public class SoundPlayer : MonoBehaviour
    {
        [SerializeField] AudioClip jumpClip = null;
        [SerializeField] AudioClip winClip = null;

        AudioSource audioSource;
        SceneEnder sceneEnder;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            sceneEnder = GameObject.FindGameObjectWithTag("Loader").GetComponent<SceneEnder>();

            sceneEnder.onSceneEnd += PlayWinClip;
        }

        public void PlayJumpClip() { audioSource.PlayOneShot(jumpClip); }
        public void PlayWinClip() { audioSource.PlayOneShot(winClip); }
    }
}