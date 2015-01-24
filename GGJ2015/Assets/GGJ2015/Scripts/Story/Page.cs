using System.Collections.Generic;
using Assets.GGJ2015.Scripts.Gui;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.GGJ2015.Scripts.Story {
    [RequireComponent(typeof(CanvasGroup))]
    public class Page : MonoBehaviour {
        public List<ChoiceGui> ChoiceGuis = new List<ChoiceGui>();
        public PivotGraphics PivotGraphics;

        public void Refresh(Pivot pivot) {
            if (ChoiceGuis.Count != 2) {
                Debug.LogError("Choices text count isn't 2!");
            }
            if (pivot.Choices.Count != 2) {
                Debug.LogError(string.Format("'{0}' doesn't have 2 choices!", pivot.Id));
            }
            ChoiceGuis [0].Text.text = pivot.Choices [0].Description;
            ChoiceGuis [1].Text.text = pivot.Choices [1].Description;

        }


    }
}
