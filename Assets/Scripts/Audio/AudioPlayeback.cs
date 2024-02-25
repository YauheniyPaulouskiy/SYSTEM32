using UnityEngine;

namespace Audio
{
    public class AudioPlayeback : MonoBehaviour
    {
        private AudioSource _audioSource;

        #region [Initialization]
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }
        #endregion

        public void PlayAudio()
        {
            _audioSource.Play();
        }
    }
}