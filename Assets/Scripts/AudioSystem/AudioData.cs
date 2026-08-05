using System;
using UnityEngine;
using UnityEngine.Audio;

namespace AudioSystem
{
    [Serializable]
    public class AudioData
    {
        [field:SerializeField]
        public AudioClip Clip { get; private set; }
        
        [field:SerializeField]
        public AudioMixerGroup MixerGroup { get; private set; }
        
        [field:SerializeField]
        public bool Loop { get; private set; }
        
        [field:SerializeField]
        public bool PlayOnAwake { get; private set; }
        
        [field:SerializeField]
        public bool FrequentSound { get; private set; }
        
        [field:SerializeField]
        public bool Mute { get; private set; }
        
        [field:SerializeField]
        public bool BypassEffects { get; private set; }
        
        [field:SerializeField]
        public bool BypassListenerEffects { get; private set; }
        
        [field:SerializeField]
        public bool BypassReverbZones { get; private set; }
        
        [field:SerializeField]
        public int Priority { get; private set; } = 128;
        
        [field:SerializeField]
        public float Volume { get; private set; } = 1f;
        
        [field:SerializeField]
        public float Pitch { get; private set; } = 1f;
        
        [field:SerializeField]
        public float PanStereo { get; private set; }
        
        [field:SerializeField]
        public float SpatialBlend { get; private set; }
        
        [field:SerializeField]
        public float ReverbZoneMix { get; private set; } = 1f;
        
        [field:SerializeField]
        public float DopplerLevel { get; private set; } = 1f;
        
        [field:SerializeField]
        public float Spread { get; private set; }
        
        [field:SerializeField]
        public float MinDistance { get; private set; } = 1f;
        
        [field:SerializeField]
        public float MaxDistance { get; private set; } = 500f;
        
        [field:SerializeField]
        public bool IgnoreListenerVolume { get; private set; }
        
        [field:SerializeField]
        public bool IgnoreListenerPause { get; private set; }
        
        [field:SerializeField]
        public AudioRolloffMode RolloffMode { get; private set; } = AudioRolloffMode.Logarithmic;
    }
}