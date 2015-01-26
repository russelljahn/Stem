
using System;
using System.Collections.Generic;


namespace Assets.Stem.Scripts.Pivots {
    public class Choice {
        public string Description;
        public string NextPivot;
        public string OnTriggerAnimationName;
        public string OnTriggerTrackName;
        public List<Action> OnTrigger;
    }
}
