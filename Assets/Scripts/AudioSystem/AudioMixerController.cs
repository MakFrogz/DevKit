using UnityEngine;
using UnityEngine.Audio;

namespace AudioSystem
{
    public class AudioMixerController
    {
        private const float MinDecibels = -80f;
        private const string MasterVolumeKey = "MasterVolume";
        private const string SoundVolumeKey = "SoundVolume";
        private const string MusicVolumeKey = "MusicVolume";
 
        private readonly AudioMixer _mixer;
        private readonly string _masterVolumeParam;
        private readonly string _soundVolumeParam;
        private readonly string _musicVolumeParam;
 
        public AudioMixerController(AudioMixer mixer, string masterVolumeParam, string soundVolumeParam, string musicVolumeParam)
        {
            _mixer = mixer;
            _masterVolumeParam = masterVolumeParam;
            _soundVolumeParam = soundVolumeParam;
            _musicVolumeParam = musicVolumeParam;
        }
 
        public void SetMasterVolume(float normalizedVolume) => SetVolume(_masterVolumeParam, normalizedVolume);
        public void SetSoundVolume(float normalizedVolume) => SetVolume(_soundVolumeParam, normalizedVolume);
        public void SetMusicVolume(float normalizedVolume) => SetVolume(_musicVolumeParam, normalizedVolume);
 
        public float GetMasterVolume() => PlayerPrefs.GetFloat(MasterVolumeKey, 1f);
        public float GetSoundVolume() => PlayerPrefs.GetFloat(SoundVolumeKey, 1f);
        public float GetMusicVolume() => PlayerPrefs.GetFloat(MusicVolumeKey, 1f);

        public void Revert()
        {
            SetMasterVolume(GetMasterVolume());
            SetSoundVolume(GetSoundVolume());
            SetMusicVolume(GetMusicVolume());
        }
 
        public void Save()
        {
            PlayerPrefs.SetFloat(MasterVolumeKey, GetVolume(_masterVolumeParam));
            PlayerPrefs.SetFloat(MusicVolumeKey, GetVolume(_musicVolumeParam));
            PlayerPrefs.SetFloat(SoundVolumeKey, GetVolume(_soundVolumeParam));
            PlayerPrefs.Save();
        }

        private void SetVolume(string exposedParam, float normalizedVolume)
        {
            normalizedVolume = Mathf.Clamp01(normalizedVolume);
            var decibels = normalizedVolume > 0.0001f ? Mathf.Log10(normalizedVolume) * 20f : MinDecibels;

            if (_mixer.SetFloat(exposedParam, decibels))
            {
                return;
            }
            Debug.LogError($"AudioMixer parameter '{exposedParam}' not found. Did you expose it via right-click -> Expose in the mixer?");
        }
        
        private float GetVolume(string exposedParam)
        {
            if (_mixer.GetFloat(exposedParam, out var decibels))
            {
                return decibels <= MinDecibels ? 0f : Mathf.Pow(10f, decibels / 20f);
            }
            Debug.LogError($"AudioMixer parameter '{exposedParam}' not found. Did you expose it via right-click -> Expose in the mixer?");
            return 1f;
        }
    }
}