using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using Assets.Stem.Scripts.Extensions;
using UnityEditor;
using UnityEngine;


namespace Assets.Stem.Scripts.Audio {
    public class AudioClips : MonoBehaviour {
        [SerializeField] protected List<AudioClip> Clips = new List<AudioClip>();

        public const string BgNormal = "BgNormal";
        public const string SfxButton = "SfxButton";
		public const string BgPeePuddle = "BgPeePuddle";
		public const string BgRumblyTummy = "BgRumblyTummy";
		public const string BgFriendDies = "BgFriendDies";
		public const string BgGotEbola = "BgGotEbola";
		public const string BgAreYouSure = "BgAreYouSure";
		public const string BgYouSure = "BgYouSure";
		public const string BgYouDied = "BgYouDied";
		public const string BgInTrial = "BgInTrial";
		public const string BgGrave = "BgGrave";
		public const string BgAsylum = "BgAsylum";
		public const string BgIncineration = "BgIncineration";
		public const string BgStoryRoot = "BgStoryRoot";
		public const string BgMakeFlowerBaby = "BgMakeFlowerBaby";
		public const string BgBeFriends = "BgBeFriends";
		public const string BgBeLovers = "BgBeLovers";
		public const string BgExperiment = "BgExperiment";
		public const string BgGetStung = "BgGetStung";
		public const string BgInfinityAndBeyond = "BgInfinityAndBeyond";
		public const string BgTerriblePerson = "TerriblePerson";
		public const string BgFourthWall = "FourthWall";
		public const string BgFlowerMutates = "FlowerMutates";
		public const string BgBeeUtopia = "BeeUtopia";
        public const string ButtonClick = "ButtonClick";
		public const string BgYourBurial = "YourBurial";


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


        [MenuItem("Stem/Refresh Audio Clips")]
        private static void RefreshAudioClips() {
            var assetPaths = AssetDatabase.GetAllAssetPaths().Where(asset => asset.EndsWith(".mp3")).ToArray();
            
            Instance.Clips.Clear();
            Instance.Clips.Capacity = assetPaths.Length;

            foreach (var path in assetPaths) {
                var clip = (AudioClip)AssetDatabase.LoadAssetAtPath(path, typeof (AudioClip));
                Instance.Clips.Add(clip);
            }
        }
    }
}
