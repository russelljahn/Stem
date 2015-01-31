using System;
using System.Collections.Generic;
using Assets.Stem.Scripts.Gui.PivotAnimations;


namespace Assets.Stem.Scripts.Pivots {
    [Serializable]
    public class Pivot {
        public string Id;
        public string Description;
        public List<Choice> Choices;
    }
}
