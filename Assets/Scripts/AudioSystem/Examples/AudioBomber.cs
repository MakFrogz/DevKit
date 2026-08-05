using UnityEngine;

namespace AudioSystem.Examples
{
    public class AudioBomber : MonoBehaviour
    {
        [SerializeField]
        private AudioData _audioData;
        
        [SerializeField]
        private float _fireRate = 0.5f;
        
        private SoundBuilder _soundBuilder;
        private float _nextFire;
        
        private void Start()
        {
            _soundBuilder = AudioProvider.Instance.CreateSoundBuilder();
            _nextFire = Time.time;
        }

        private void Update()
        {
            if (_nextFire > Time.time)
            {
                return;
            }
            _soundBuilder.Play(_audioData);
            _nextFire = Time.time + 1 / _fireRate;
        }
    }
}