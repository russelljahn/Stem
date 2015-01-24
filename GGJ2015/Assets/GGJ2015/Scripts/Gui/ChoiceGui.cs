using UnityEngine;
using UnityEngine.UI;


namespace Assets.GGJ2015.Scripts.Gui {
    public class ChoiceGui : MonoBehaviour {
        public Text Text;
        public Button Button;


        private void Reset() {
            Text = GetComponent<Text>();
            Button = GetComponent<Button>();
        }
    }
}
