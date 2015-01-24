using System;
using System.Collections.Generic;
using Assets.GGJ2015.Scripts.Gui;


namespace Assets.GGJ2015.Scripts.Story {
    [Serializable]
    public class Pivot {
        public string Id;
        public string Description;
        public PivotGraphics Graphics;
        public List<Choice> Choices;
    }
}
