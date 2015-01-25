using System.Collections.Generic;
using System.Linq;
using Assets.GGJ2015.Scripts.Extensions;
using UnityEngine;


namespace Assets.GGJ2015.Scripts.Audio {
    public class AudioClips : MonoBehaviour {
       

        [SerializeField] protected List<AudioClip> Clips = new List<AudioClip>();

        public const string BgHappy = "BgHappy";
        public const string BgDark = "BgDark";
        public const string ButtonClick = "ButtonClick";



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
            Debug.Log(Instance.Clips.First(clip => clip.name == clipName));
            return Instance.Clips.First(clip => clip.name == clipName);
        }


        public static int GetClipTrackId(string clipName) {
            switch (clipName) {
                case BgHappy:
                    return AudioTrackIds.Bg1;

                case BgDark:
                    return AudioTrackIds.Bg2;

                case ButtonClick:
                    return AudioTrackIds.Sfx1;

                default:
                    Debug.LogError(string.Format("Unknown clip name: " + clipName));
                    return AudioTrackIds.Sfx3;
            }
        }
    }
}
