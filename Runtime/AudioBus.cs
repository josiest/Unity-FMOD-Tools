using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace Pi.FMODTools
{
    [UnityEngine.CreateAssetMenu(menuName="Pi Toolset/FMOD Tools/Audio Bus")]
    public class AudioBus : ScriptableObject
    {
        // Configuration
        [Tooltip("The FMOD path for the bus - empty for master bus")]
        public string Path;
        
        [Range(0f, 1f)]
        [Tooltip("Value for volume on a scale of 0-1")]
        [SerializeField] private float volume = 1f;

        // Public Interface
        public delegate void VolumeChangedEvent(float value);
        public VolumeChangedEvent OnVolumeChanged;

        public void Initialize()
        {
            if (RuntimeManager.IsInitialized)
            {
                bus = RuntimeManager.GetBus($"bus:/{Path}");
                bus.setVolume(volume);
            }
        }

        public float Volume
        {
            get
            {
                if (!bus.isValid()) { Initialize(); }
                return volume;
            }
            set
            {
                if (Mathf.Approximately(volume, value)) { return; }
                volume = value;
                UpdateVolume();
            }
        }
        
        // Internal Interface
        private Bus bus;

        private void UpdateVolume()
        {
            if (RuntimeManager.IsInitialized)
            {
                if (bus.isValid()) { bus.setVolume(volume); }
                OnVolumeChanged?.Invoke(volume);
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            UpdateVolume();
        }
#endif
    }
}
