using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using Assets.Stem.Scripts.Extensions;
using UnityEditor;
using UnityEngine;


namespace Assets.Stem.Scripts.Audio {
    public class AudioClips : MonoBehaviour {
        [SerializeField] protected List<AudioClip> Clips = new List<AudioClip>();

        public const string BgDubstep = "BgDubstep";
        public const string BgGetBusy = "BgGetBusy";
        public const string BgHell = "BgHell";
        public const string BgHoneyBombs = "BgHoneyBombs";
        public const string BgNormal = "BgNormal";
        public const string BgRadioactive = "BgRadioactive";
        public const string BgSpaceBees = "BgSpaceBees";

        public const string SfxBees = "SfxBees";
        public const string SfxBeeExperiment = "SfxBeeExperiment";
        public const string SfxBell = "SfxBell";
        public const string SfxBootStomp = "SfxBootStomp";
        public const string SfxButton = "SfxButton";
        public const string SfxEvilLaugh = "SfxEvilLaugh";
        public const string SfxLionRoar = "SfxLionRoar";
        public const string SfxOrchestra = "SfxOrchestra";
        public const string SfxPee = "SfxPee";
        public const string SfxPesticide = "SfxPesticide";
        public const string SfxPsycho = "SfxPsycho";
        public const string SfxStork = "SfxStork";
        public const string SfxStraw = "SfxStraw";
        public const string SfxWaterFlower = "SfxWaterFlower";


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

                case BgDubstep:
                case BgHell:
                case BgGetBusy:
                case BgRadioactive:
                case BgSpaceBees:
                case BgHoneyBombs:
                    return AudioTrackIds.Bg2;

                case SfxButton:
                    return AudioTrackIds.Sfx1;

                case SfxBees:
                case SfxBeeExperiment:
                case SfxBell:
                case SfxBootStomp:
                case SfxEvilLaugh:
                case SfxLionRoar:
                case SfxOrchestra:
                case SfxPee:
                case SfxPesticide:
                case SfxPsycho:
                case SfxStork:
                case SfxStraw:
                case SfxWaterFlower:
                    return AudioTrackIds.Sfx2;

                default:
                    Debug.LogError(string.Format("Unknown clip name: " + clipName));
                    return AudioTrackIds.Sfx3;
            }
        }


        [MenuItem("Stem/Refresh Audio Clips")]
        private static void RefreshAudioClips() {
            var assetPaths = AssetDatabase.GetAllAssetPaths().Where(assetName => IsAudioFile(assetName)).ToArray();
            
            Instance.Clips.Clear();
            Instance.Clips.Capacity = assetPaths.Length;

            foreach (var path in assetPaths) {
                var clip = (AudioClip)AssetDatabase.LoadAssetAtPath(path, typeof (AudioClip));
                Instance.Clips.Add(clip);
            }

            Debug.Log("Refreshed " + assetPaths.Length + " audio clips!");
        }


        private static bool IsAudioFile(string fileName) {
            return fileName.EndsWith(".mp3") || fileName.EndsWith(".wav");
        }
    }
}
