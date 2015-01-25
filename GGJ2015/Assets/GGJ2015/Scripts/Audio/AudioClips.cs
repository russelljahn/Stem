using System.Collections.Generic;
using System.Linq;
using Assets.GGJ2015.Scripts.Extensions;
using UnityEngine;


namespace Assets.GGJ2015.Scripts.Audio {
    public class AudioClips : MonoBehaviour {
       

        [SerializeField] protected List<AudioClip> Clips = new List<AudioClip>();

        public const string BgNormal = "BgNormal";
        public const string SfxButton = "SfxButton";



         #region Singleton nonsense
        private static AudioClips _instance;

        private static AudioClips Instance
        {
            get
            {
                if (_instance.IsNull())
                {
                    _instance = FindObjectOfType<AudioClips>();
                }
                if (_instance.IsNull())
                {
                    var go = new GameObject("AudioClips");
                    _instance = go.AddComponent<AudioClips>();
                    DontDestroyOnLoad(_instance);
                }
                return _instance;
            }
        }


        protected AudioClips() { }
        #endregion


        public static AudioClip GetClip(string clipName) {
            foreach (var clip in Instance.Clips) {
                if (clip.IsNull()) {
                    continue;
                }
                if (clip.name.Equals(clipName)) {
                    return clip;
                }
            }
            Debug.LogError("Could not find clip with name: " + clipName);
            return null;
        }


        public static int GetClipTrackId(string clipName) {
            switch (clipName) {
                case BgNormal:
                    return AudioTrackIds.Bg1;

                case SfxButton:
                    return AudioTrackIds.Sfx1;

                default:
                    Debug.LogError(string.Format("Unknown clip name: " + clipName));
                    return AudioTrackIds.Sfx3;
            }
        }
    }
}
