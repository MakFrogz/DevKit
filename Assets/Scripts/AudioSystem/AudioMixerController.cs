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
 
            SetMasterVolume(PlayerPrefs.GetFloat(MasterVolumeKey, 1f));
            SetSoundVolume(PlayerPrefs.GetFloat(SoundVolumeKey, 1f));
            SetMusicVolume(PlayerPrefs.GetFloat(MusicVolumeKey, 1f));
        }
 
        public void SetMasterVolume(float normalizedVolume) => SetVolume(_masterVolumeParam, MasterVolumeKey, normalizedVolume);
        public void SetSoundVolume(float normalizedVolume) => SetVolume(_soundVolumeParam, SoundVolumeKey, normalizedVolume);
        public void SetMusicVolume(float normalizedVolume) => SetVolume(_musicVolumeParam, MusicVolumeKey, normalizedVolume);
 
        public float GetMasterVolume() => PlayerPrefs.GetFloat(MasterVolumeKey, 1f);
        public float GetSoundVolume() => PlayerPrefs.GetFloat(SoundVolumeKey, 1f);
        public float GetMusicVolume() => PlayerPrefs.GetFloat(MusicVolumeKey, 1f);
 
        public void Save() => PlayerPrefs.Save();
 
        private void SetVolume(string exposedParam, string prefsKey, float normalizedVolume)
        {
            normalizedVolume = Mathf.Clamp01(normalizedVolume);
            var decibels = normalizedVolume > 0.0001f ? Mathf.Log10(normalizedVolume) * 20f : MinDecibels;
 
            if (!_mixer.SetFloat(exposedParam, decibels))
            {
                Debug.LogError($"AudioMixer parameter '{exposedParam}' not found. Did you expose it via right-click -> Expose in the mixer?");
                return;
            }
 
            PlayerPrefs.SetFloat(prefsKey, normalizedVolume);
        }
    }
}