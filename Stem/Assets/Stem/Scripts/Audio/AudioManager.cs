using System.Collections.Generic;
using System.Linq;
using Assets.Stem.Scripts.Extensions;
using Assets.Stem.Scripts.Utils;
using UnityEngine;


namespace Assets.Stem.Scripts.Audio {
    public class AudioManager : MonoBehaviour {

        private readonly List<AudioSource> _tracks = new List<AudioSource>();
        private const int NumTracks = 5;

        [SerializeField] private AnimationCurve _fadeInEasing = AnimationCurveUtils.GetLinearCurve();
        [SerializeField] private AnimationCurve _fadeOutEasing = AnimationCurveUtils.GetLinearCurve();

        [SerializeField] private float _fadeTime = 1.0f;


        #region Singleton nonsense
        private static AudioManager _instance;

        public static AudioManager Instance {
            get {
                if (_instance.IsNull()) {
                    _instance = FindObjectOfType<AudioManager>();
                }
                if (_instance.IsNull()) {
                    var go = new GameObject("AudioManager");
                    _instance = go.AddComponent<AudioManager>();
                    DontDestroyOnLoad(_instance);
                }
                return _instance;
            }
        }


        protected AudioManager() { }
        #endregion


        private void Awake() {
            SetupTracks();
        }


        private void SetupTracks() {
            var sources = GetComponents<AudioSource>();
            int trackId = 0;
            for (; trackId < NumTracks; ++trackId) {
                if (trackId >= sources.Count() || trackId >= NumTracks) {
                    break;
                }
                var source = sources [trackId];
                SetDefaultSourceSettings(source);
                _tracks.Add(source);
            }

            while (trackId < NumTracks) {
                var source = gameObject.AddComponent<AudioSource>();
                SetDefaultSourceSettings(source);
                _tracks.Add(source);
                ++trackId;
            }
        }


        private void SetDefaultSourceSettings(AudioSource source) {
            source.loop = false;
            source.playOnAwake = false;
            source.volume = 1f;
            source.pitch = 1f;
        }


        public void LoadClip(string clipName, float volume = 1.0f, float pitch = 1.0f, bool loop = false) {
            var trackId = AudioClips.GetClipTrackId(clipName);
            var clip = AudioClips.GetClip(clipName);
            LoadClip(trackId, clip, volume, pitch, loop);
        }


        private void LoadClip(int trackId, AudioClip clip, float volume = 1.0f, float pitch = 1.0f, bool loop = false) {
            var track = _tracks[trackId];
            track.time = 0f;
            track.clip = clip;
            track.volume = volume;
            track.pitch = pitch;
            track.loop = loop;
        }


        public void PlayTrack(string clipName) {
            var trackId = AudioClips.GetClipTrackId(clipName);
            PlayTrack(trackId);
        }


        private void PlayTrack(int trackId) {
            var track = _tracks [trackId];
            track.Play();
        }


        public void PlayTrackOneShot(string clipName, float volume = 1.0f, float pitch = 1.0f) {
            var clip = AudioClips.GetClip(clipName);
            var trackId = AudioClips.GetClipTrackId(clipName);
            LoadClip(trackId, clip, volume, pitch);
            PlayTrackOneShot(trackId);
        }


        private void PlayTrackOneShot(int trackId) {
            var track = _tracks [trackId];
            track.PlayOneShot(track.clip);
        }


        public void Crossfade(string fadeOutClipName, string fadeInClipName, float fadeOutVolume = 0.0f, float fadeInVolume = 1.0f) {
            var fadeOutTrackId = AudioClips.GetClipTrackId(fadeOutClipName);
            var fadeInTrackId = AudioClips.GetClipTrackId(fadeInClipName);

            if (fadeInTrackId == fadeOutTrackId) {
                Debug.LogError(string.Format("'{0}' and '{1}' share the same track id of '{2}'!", fadeOutClipName, fadeInClipName, fadeInTrackId));
                return;
            }

            Crossfade(fadeOutTrackId, fadeInTrackId, fadeOutVolume, fadeInVolume);
        }


        private void Crossfade(int fadeOutTrackId, int fadeInTrackId, float fadeOutVolume = 0.0f, float fadeInVolume = 1.0f) {
            Fade(fadeOutTrackId, fadeOutVolume, _fadeOutEasing);
            Fade(fadeInTrackId, fadeInVolume, _fadeInEasing);
        }


        public void Fade(string clipName, float volume = 1.0f, AnimationCurve easing = null) {
            easing = easing ?? AnimationCurveUtils.GetLinearCurve();
            var trackId = AudioClips.GetClipTrackId(clipName);
            var fadeTrack = _tracks [trackId];
            TweenUtils.TweenVolume(fadeTrack, volume, _fadeTime, easing);
        }


        private void Fade(int trackId, float volume = 1.0f, AnimationCurve easing = null) {
            easing = easing ?? AnimationCurveUtils.GetLinearCurve();
            var fadeTrack = _tracks [trackId];
            TweenUtils.TweenVolume(fadeTrack, volume, _fadeTime, easing);
        }

    }
}
