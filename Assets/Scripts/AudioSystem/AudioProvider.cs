using Singletons;
using UnityEngine;

namespace AudioSystem
{
    public class AudioProvider : PersistentSingleton<AudioProvider>
    {
        [SerializeField]
        private AudioSettings _audioSettings;
        
        private AudioManager _audioManager;
        private AudioMixerController _mixerController;

        protected override void Awake()
        {
            base.Awake();
            _audioManager = new AudioManager(_audioSettings, transform);
            _mixerController = new AudioMixerController(
                _audioSettings.Mixer,
                _audioSettings.MasterVolumeParameter,
                _audioSettings.SoundVolumeParameter,
                _audioSettings.MusicVolumeParameter);
        }

        public SoundBuilder CreateSoundBuilder()
        {
            return _audioManager.CreateSoundBuilder();
        }
        
        public void SetMasterVolume(float normalizedVolume) => _mixerController.SetMasterVolume(normalizedVolume);
        public void SetSoundVolume(float normalizedVolume) => _mixerController.SetSoundVolume(normalizedVolume);
        public void SetMusicVolume(float normalizedVolume) => _mixerController.SetMusicVolume(normalizedVolume);
        public float GetMasterVolume() => _mixerController.GetMasterVolume();
        public float GetSoundVolume() => _mixerController.GetSoundVolume();
        public float GetMusicVolume() => _mixerController.GetMusicVolume();
 
        public void SaveVolumeSettings() => _mixerController.Save();
    }
}