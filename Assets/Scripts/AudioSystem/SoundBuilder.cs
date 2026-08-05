using UnityEngine;

namespace AudioSystem
{
    public class SoundBuilder
    {
        private readonly AudioManager _audioManager;
        private Vector3 _position = Vector3.zero;
        private bool _randomPitch = false;
        
        public  SoundBuilder(AudioManager audioManager)
        {
            _audioManager = audioManager;
        }

        public SoundBuilder SetPosition(Vector3 position)
        {
            _position = position;
            return this;
        }
        
        public SoundBuilder WithRandomPitch() {
            _randomPitch = true;
            return this;
        }

        public void Play(AudioData audioData)
        {
            if (audioData == null)
            {
                Debug.LogError("Audio data is null");
                return;
            }

            if (!_audioManager.CanPlaySound(audioData))
            {
                return;
            }
            
            var soundEmitter = _audioManager.Get();
            soundEmitter.Initialize(audioData);
            soundEmitter.transform.position = _position;
            soundEmitter.transform.parent = _audioManager.SoundContainer;
            
            if (_randomPitch) {
                soundEmitter.WithRandomPitch();
            }

            if (audioData.FrequentSound)
            {
                _audioManager.AddFrequentSound(soundEmitter);
            }
            
            soundEmitter.Play();
        }
    }
}