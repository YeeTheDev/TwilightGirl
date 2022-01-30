using UnityEngine;

namespace TG.Audio
{
    public class MusicPlayer : MonoBehaviour
    {
        public static MusicPlayer singleton;

        private void Awake() { SetUpSingleton(); }

        private void SetUpSingleton()
        {
            if (singleton == null) { singleton = this; }
            else { Destroy(gameObject); }

            DontDestroyOnLoad(gameObject);
        }
    }
}