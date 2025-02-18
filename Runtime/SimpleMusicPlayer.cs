using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;

namespace Pi.FMODTools {
    public class SimpleMusicPlayer : MonoBehaviour
    {
        // Configuration
        [Header("General")]
        [Tooltip("The music to play when this objet is loaded")]
        public EventReference Music;

        [Header("Advanced")]
        [Tooltip("Should the music cut without fading when this object is destroyed (not recommended)?")]
        public bool NoFadeOut;
        
        // Internal interface
        private EventInstance musicInstance;
        
        // Unity Events
        private void Start()
        {
            musicInstance = RuntimeManager.CreateInstance(Music);
            if (musicInstance.isValid()) { musicInstance.start(); }
        }

        private void OnDestroy()
        {
            if (!musicInstance.isValid()) { return; }
            musicInstance.stop(NoFadeOut? STOP_MODE.IMMEDIATE : STOP_MODE.ALLOWFADEOUT);
            musicInstance.release();
        }
    }
}