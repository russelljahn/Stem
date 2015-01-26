using System.Collections.Generic;
using System.Linq;
using Assets.GGJ2015.Scripts.Utils;
using UnityEngine;


namespace Assets.GGJ2015.Scripts.Audio {
    public class AudioManager : MonoBehaviour {

        private readonly List<AudioSource> _tracks = new List<AudioSource>();
        private const int NumTracks = 5;

        [SerializeField] private AnimationCurve _fadeInEasing = AnimationCurveUtils.GetLinearCurve();
        [SerializeField] private AnimationCurve _fadeOutEasing = AnimationCurveUtils.GetLinearCurve();

        [SerializeField] private float _crossfadeTime = 1.0f;


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
                _tracks.Add(source);
            }

            while (trackId < NumTracks) {
                var source = gameObject.AddComponent<AudioSource>();
                _tracks.Add(source);
                ++trackId;
            }
        }


        public void LoadClip(int trackId, AudioClip clip, float volume = 1.0f, bool loop = false) {
            var track = _tracks[trackId];
            track.clip = clip;
            track.loop = loop;
            track.volume = volume;
        }


        public void PlayTrack(int trackId) {
            var track = _tracks[trackId];
            track.Play();
        }


        public void PlayTrackOneShot(int trackId) {
            var track = _tracks [trackId];
            track.PlayOneShot(track.clip);
        }


        public void PlayTrackOneShot(string clipName) {
            var clip = AudioClips.GetClip(clipName);
            var trackId = AudioClips.GetClipTrackId(clipName);
            LoadClip(trackId, clip);
            PlayTrackOneShot(trackId);
        }



        public void Crossfade(int fadeInTrackId, int fadeOutTrackId, float fadeInVolume = 0.0f, float fadeOutVolume = 1.0f) {
            Fade(fadeOutTrackId, fadeOutVolume, _fadeOutEasing);
            Fade(fadeInTrackId, fadeInVolume, _fadeInEasing);
        }


        public void Fade(int trackId, float volume = 1.0f, AnimationCurve easing = null) {
            easing = easing ?? AnimationCurveUtils.GetLinearCurve();
            var fadeInTrack = _tracks [trackId];
            TweenUtils.TweenVolume(fadeInTrack, volume, _crossfadeTime, easing);
        }

    }
}
