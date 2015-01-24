using Assets.GGJ2015.Scripts.Story;
using UnityEngine;


namespace Assets.GGJ2015.Scripts.Gui {
    public class PivotGraphics : MonoBehaviour {

        public void Play() {
            enabled = true;
        }


        public void Stop() {
            enabled = false;
            var gui = new ChoiceGui();
            gui.ChoiceClicked += OnChoiceClicked;
            gui.ChoiceClicked += Dummy;

        }


        private void OnChoiceClicked(Choice choice) {
            
        }

        private void Dummy(Choice choice) {

        }

    }
}
