using System.Collections.Generic;
using Extensions;
using UnityEngine;
using UnityEngine.Pool;

namespace AudioSystem
{
    public class AudioManager
    {
        private readonly AudioSettings _settings;

        private readonly IObjectPool<SoundEmitter> _soundEmitterPool;
        private readonly List<SoundEmitter> _activeSoundEmitters;
        private readonly Dictionary<AudioClip, LinkedList<SoundEmitter>> _frequentSoundEmitters;
        
        public Transform SoundContainer { get; private set; }

        public AudioManager(AudioSettings settings, Transform soundContainer)
        {
            _settings = settings;
            SoundContainer = soundContainer;
            _soundEmitterPool = new ObjectPool<SoundEmitter>(
                CreateSoundEmitter,
                GetSoundEmitter,
                ReleaseSoundEmitter,
                DestroySoundEmitter,
                _settings.CollectionCheck,
                _settings.DefaultCapacity,
                _settings.MaxPoolSize);
            _activeSoundEmitters = new List<SoundEmitter>();
            _frequentSoundEmitters = new Dictionary<AudioClip, LinkedList<SoundEmitter>>();
        }
        
        public SoundBuilder CreateSoundBuilder() => new SoundBuilder(this);

        public bool CanPlaySound(AudioData audioData)
        {
            if (!audioData.FrequentSound)
            {
                return true;
            }
 
            if (!audioData.Clip || !_frequentSoundEmitters.TryGetValue(audioData.Clip, out var emitters) || emitters.Count == 0)
            {
                return true;
            }
 
            while (emitters.Count > 0 && !emitters.First.Value)
            {
                emitters.RemoveFirst();
            }
 
            if (emitters.Count < _settings.MaxSoundInstances)
            {
                return true;
            }
 
            var oldest = emitters.First.Value;
            emitters.RemoveFirst();
            oldest.Stop();
            return true;
        }

        public void AddFrequentSound(SoundEmitter soundEmitter)
        {
            var data = soundEmitter.Data;
            if (!data.FrequentSound || !data.Clip)
            {
                return;
            }
 
            if (!_frequentSoundEmitters.TryGetValue(data.Clip, out var emitters))
            {
                emitters = new LinkedList<SoundEmitter>();
                _frequentSoundEmitters[data.Clip] = emitters;
            }
 
            emitters.AddLast(soundEmitter);
        }
        
        public SoundEmitter Get()
        {
            return _soundEmitterPool.Get();
        }

        public void StopAll()
        {
            for (var i = _activeSoundEmitters.Count - 1; i >= 0; i--)
            {
                _activeSoundEmitters[i].Stop(); 
            }
            _frequentSoundEmitters.Clear();
        }

        private SoundEmitter CreateSoundEmitter()
        {
            var soundEmitter = Object.Instantiate(_settings.SoundEmitterPrefab);
            soundEmitter.SetInactive();
            return soundEmitter;
        }

        private void GetSoundEmitter(SoundEmitter soundEmitter)
        {
            soundEmitter.SetActive();
            _activeSoundEmitters.Add(soundEmitter);
            soundEmitter.OnStopped += OnSoundStopped;
        }

        private void OnSoundStopped(SoundEmitter soundEmitter)
        {
            RemoveFrequentSound(soundEmitter);
            _soundEmitterPool.Release(soundEmitter);
        }

        private void RemoveFrequentSound(SoundEmitter soundEmitter)
        {
            var data = soundEmitter.Data;
            if (data == null || !data.FrequentSound || !data.Clip)
            {
                return;
            }
 
            if (_frequentSoundEmitters.TryGetValue(data.Clip, out var emitters))
            {
                emitters.Remove(soundEmitter);
            }
        }

        private void ReleaseSoundEmitter(SoundEmitter soundEmitter)
        {
            soundEmitter.SetInactive();
            _activeSoundEmitters.Remove(soundEmitter);
            soundEmitter.OnStopped -= OnSoundStopped;
        }

        private void DestroySoundEmitter(SoundEmitter soundEmitter)
        {
            Object.Destroy(soundEmitter.gameObject);
        }
    }
}