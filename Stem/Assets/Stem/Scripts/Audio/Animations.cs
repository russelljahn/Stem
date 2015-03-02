using System.Collections.Generic;
using Assets.Stem.Scripts.Constants;
using Assets.Stem.Scripts.Extensions;
using Assets.Stem.Scripts.Gui.PivotAnimations;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif


namespace Assets.Stem.Scripts.Audio {
    public class Animations : MonoBehaviour {

        [SerializeField] protected List<PivotAnimation> PivotAnimations = new List<PivotAnimation>();

        public const string BaconWitch = "BaconWitch";
        public const string BeeExperiment = "BeeExperiment";
        public const string BeLovers = "BeLovers";
        public const string BeeUtopia = "BeeUtopia";
        public const string BootStomp = "BootStomp";
        public const string DrinkPee = "DrinkPee";
        public const string DrinkWater = "DrinkWater";
        public const string FlowerGrow = "FlowerGrow";
        public const string FlowersSprout = "FlowersSprout";
        public const string FriendDrink = "FriendDrink";
        public const string GotEbola = "GotEbola";
        public const string GetSickFromPee = "GetSickFromPee";
        public const string GetSickFromWater = "GetSickFromWater";
        public const string GetStung = "GetStung";
        public const string God = "God";
        public const string Grave = "Grave";
        public const string HoneyBombs = "HoneyBombs";
        public const string Pee = "Pee";
        public const string Pesticide = "Pesticide";
        public const string SpaceBees = "SpaceBees";
        public const string WaterFlower = "WaterFlower";


         #region Singleton nonsense
        private static Animations _instance;

        private static Animations Instance
        {
            get
            {
                if (_instance.IsNull())
                {
                    _instance = FindObjectOfType<Animations>();
                }
                if (_instance.IsNull())
                {
                    var go = new GameObject("Animations");
                    _instance = go.AddComponent<Animations>();
                    DontDestroyOnLoad(_instance);
                }
                return _instance;
            }
        }


        protected Animations() { }
        #endregion


        public static PivotAnimation GetAnimation(string animationName) {
            foreach (var animation in Instance.PivotAnimations) {
                if (animation.IsNull()) {
                    continue;
                }
                if (animation.name.Equals(animationName)) {
                    return animation;
                }
            }
            Debug.LogError("Could not find animation clip with name: " + animationName);
            return null;
        }


#if UNITY_EDITOR
        [MenuItem("Stem/Refresh Pivot Animations")]
        private static void RefreshPivotAnimations() {
            var pivotAnimationsRoot = GameObject.FindWithTag(TagConstants.PivotAnimationsRoot);
            if (pivotAnimationsRoot.IsNull()) {
                Debug.LogError("GameObject with tag '" + TagConstants.PivotAnimationsRoot + "' is null!");
            }

            var pivotAnimations = pivotAnimationsRoot.GetComponentsInChildren<PivotAnimation>(true);
            Instance.PivotAnimations.Clear();
            Instance.PivotAnimations.Capacity = pivotAnimations.Length;

            foreach (var pivotAnimation in pivotAnimations) {
                Instance.PivotAnimations.Add(pivotAnimation);
            }

            Debug.Log("Refreshed " + pivotAnimations.Length + " pivot animations!");
        }
#endif

    }
}
