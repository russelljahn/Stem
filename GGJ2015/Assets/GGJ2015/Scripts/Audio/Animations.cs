using System.Collections.Generic;
using Assets.GGJ2015.Scripts.Extensions;
using Assets.GGJ2015.Scripts.Gui.PivotAnimations;
using UnityEngine;


namespace Assets.GGJ2015.Scripts.Audio {
    public class Animations : MonoBehaviour {
       

        [SerializeField] protected List<PivotAnimation> PivotAnimations = new List<PivotAnimation>();

        public const string BeeExperiment = "BeeExperiment";
        public const string BeeUtopia = "BeeUtopia";
        public const string BootStomp = "BootStomp";
        public const string DrinkPee = "DrinkPee";
        public const string DrinkWater = "DrinkWater";
        public const string GotEbola = "GotEbola";
        public const string Grave = "Grave";
        public const string FlowerGrow = "FlowerGrow";
        public const string FlowersSprout = "FlowersSprout";
        public const string GetSickFromPee = "GetSickFromPee";
        public const string GetSickFromWater = "GetSickFromWater";
        public const string GetStung = "GetStung";
        public const string InfectOthers = "InfectOthers";
        public const string Pee = "Pee";
        public const string Pesticide = "Pesticide";
        public const string SpaceBees = "SpaceBees";
        public const string WaterFlower = "WaterFlower";

        public const string BeLovers = "BeLovers";
        public const string GetEbolaSting = "GetEbolaSting";
        public const string BaconWitch = "BaconWitch"; // Stake + grave

        // friend gets sick




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
            Debug.LogError("Could not find clip with name: " + animationName);
            return null;
        }
    }
}
