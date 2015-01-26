
using System;
using System.Collections.Generic;


namespace Assets.GGJ2015.Scripts.Pivots {
    public class Choice {
        public string Description;
        public string NextPivot;
        public string OnTriggerAnimationName;
        public string OnTriggerTrackName;
        public List<Action> OnTrigger;
    }
}
