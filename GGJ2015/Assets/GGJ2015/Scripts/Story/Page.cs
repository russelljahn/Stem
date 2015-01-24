using System.Collections.Generic;
using Assets.GGJ2015.Scripts.Gui;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.GGJ2015.Scripts.Story {
    [RequireComponent(typeof(CanvasGroup))]
    public class Page : MonoBehaviour {
        public List<ChoiceGui> ChoiceGuis = new List<ChoiceGui>();
        public PivotGraphics PivotGraphics;

        public void Load(Pivot pivot) {
            if (ChoiceGuis.Count != 2) {
                Debug.LogError("Choices text count isn't 2!");
            }
            if (pivot.Choices.Count != 2) {
                Debug.LogError(string.Format("'{0}' doesn't have 2 choices!", pivot.Id));
            }

            for (int i = 0; i < pivot.Choices.Count; ++i) {
                var choiceGui = ChoiceGuis [i];
                var choice = pivot.Choices[i];

                choiceGui.LoadChoice(choice);
                choiceGui.AnimateIn();
            }
        }


    }
}
