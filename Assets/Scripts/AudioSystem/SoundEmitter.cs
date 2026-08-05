using System;
using System.Collections;
using Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AudioSystem
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundEmitter : MonoBehaviour
    {
        public event Action<SoundEmitter> OnStopped;
        public AudioData Data { get; private set; }
        private AudioSource _audioSource;

        private Coroutine _playingCoroutine;
        private WaitWhile _waitWhile;

        private void Awake()
        {
            _audioSource = gameObject.GetOrAdd<AudioSource>();
            //_waitWhile = new WaitWhile(() => _audioSource.isPlaying && _audioSource.time > Mathf.Epsilon);
            _waitWhile = new WaitWhile(() => _audioSource.isPlaying);
        }

        public void Initialize(AudioData data)
        {
            Data = data;
            _audioSource.clip = data.Clip;
            _audioSource.outputAudioMixerGroup = data.MixerGroup;
            _audioSource.loop = data.Loop;
            _audioSource.playOnAwake = data.PlayOnAwake;

            _audioSource.mute = data.Mute;
            _audioSource.bypassEffects = data.BypassEffects;
            _audioSource.bypassListenerEffects = data.BypassListenerEffects;
            _audioSource.bypassReverbZones = data.BypassReverbZones;

            _audioSource.priority = data.Priority;
            _audioSource.volume = data.Volume;
            _audioSource.pitch = data.Pitch;
            _audioSource.panStereo = data.PanStereo;
            _audioSource.spatialBlend = data.SpatialBlend;
            _audioSource.reverbZoneMix = data.ReverbZoneMix;
            _audioSource.dopplerLevel = data.DopplerLevel;
            _audioSource.spread = data.Spread;

            _audioSource.minDistance = data.MinDistance;
            _audioSource.maxDistance = data.MaxDistance;

            _audioSource.ignoreListenerVolume = data.IgnoreListenerVolume;
            _audioSource.ignoreListenerPause = data.IgnoreListenerPause;

            _audioSource.rolloffMode = data.RolloffMode;
        }

        public void Play()
        {
            if (_playingCoroutine != null)
            {
                StopCoroutine(_playingCoroutine);
            }

            _audioSource.Play();
            _playingCoroutine = StartCoroutine(WaitForSoundToEnd());
        }

        private IEnumerator WaitForSoundToEnd()
        {
            yield return _waitWhile;
            Stop();
        }

        public void Stop()
        {
            if (_playingCoroutine != null)
            {
                StopCoroutine(_playingCoroutine);
                _playingCoroutine = null;
            }

            _audioSource.Stop();
            OnStopped?.Invoke(this);
        }

        public void WithRandomPitch(float min = -0.05f, float max = 0.05f)
        {
            _audioSource.pitch += Random.Range(min, max);
        }
    }
}