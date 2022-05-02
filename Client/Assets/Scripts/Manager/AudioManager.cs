using UnityEngine;

namespace Manager
{
    public class AudioManager : BaseManager
    {
        public AudioManager(GameFacade gameFacade) : base(gameFacade)
        {
        }

        public const string SoundPrefix = "Sounds/";
        public const string SoundBg = "BgSound";
        public const string SoundClick = "ClickSound";
        public const string SoundBoiling = "BoilingSound";
        public const string SoundCut = "CutSound";
        public const string SoundFry = "FrySound";
        public const string SoundPan = "PanSound";
        public const string SoundWater = "WaterSound";

        private AudioSource bgAudioSource;
        private AudioSource normalAudioSource;

        public override void OnInit()
        {
            base.OnInit();
            GameObject audioSourceGO = new GameObject("AudioSource(GameObject)");
            bgAudioSource = audioSourceGO.AddComponent<AudioSource>();
            normalAudioSource = audioSourceGO.AddComponent<AudioSource>();

            PlaySound(bgAudioSource, LoadSound(SoundBg),1,  true);
        }

        public void PlayBgSound(string soundName)
        {
            PlaySound(bgAudioSource, LoadSound(soundName), 1f, true);
        }

        public void PlayNormalSound(string soundName)
        {
            PlaySound(normalAudioSource, LoadSound(soundName), 1f);
        }
        
        // 播放音效
        private void PlaySound(AudioSource audioSource, AudioClip audioClip, float volume, bool loop = false)
        {
            audioSource.clip = audioClip;
            audioSource.volume = volume;
            audioSource.loop = loop;
            audioSource.Play();
        }
        
        private AudioClip LoadSound(string soundName)
        {
            return Resources.Load<AudioClip>(SoundPrefix + soundName);
        }
    }
}