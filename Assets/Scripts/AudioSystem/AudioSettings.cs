using UnityEngine;
using UnityEngine.Audio;

namespace AudioSystem
{
    [CreateAssetMenu(fileName = "AudioSettings", menuName = "Audio System/Audio Settings", order = 0)]
    public class AudioSettings : ScriptableObject
    {
        [field:SerializeField]
        public SoundEmitter SoundEmitterPrefab { get; private set; }
        
        [field: SerializeField]
        public bool CollectionCheck { get; private set; } = true;
        
        [field: SerializeField]
        public int DefaultCapacity { get; private set; } = 10;
        
        [field: SerializeField]
        public int MaxPoolSize { get; private set; } = 100;
        
        [field: SerializeField]
        public int MaxSoundInstances { get; private set; } = 30;
        
        [field: SerializeField]
        public AudioMixer Mixer { get; private set; }
 
        [field: SerializeField]
        public string MasterVolumeParameter { get; private set; } = "MasterVolume";
 
        [field: SerializeField]
        public string SoundVolumeParameter { get; private set; } = "SoundVolume";
 
        [field: SerializeField]
        public string MusicVolumeParameter { get; private set; } = "MusicVolume";
    }
}