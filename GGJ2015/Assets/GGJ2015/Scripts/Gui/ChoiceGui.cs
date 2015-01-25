using System;
using Assets.GGJ2015.Scripts.Pivots;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Assets.GGJ2015.Scripts.Gui {
    [RequireComponent(typeof(Text))]
    [RequireComponent(typeof(Button))]
    public class ChoiceGui : MonoBehaviour {
        [SerializeField] private Text _text;
        [SerializeField] private Button _button;
        private Choice _choice;

        public event Action<Choice> ChoiceClicked = delegate { }; 


        private void Reset() {
            _text = GetComponent<Text>();
            _button = GetComponent<Button>();
        }


        private void OnEnable() {
            _button.onClick.AddListener(OnChoiceClicked);
        }


        private void OnDisable() {
            _button.onClick.RemoveListener(OnChoiceClicked);
        }


        private void OnChoiceClicked() {
            ChoiceClicked.Invoke(_choice);
        }


        public void LoadChoice(Choice choice) {
            _choice = choice;
            _text.text = choice.Description;
        }


        public void AnimateIn() {
            
        }


        public void AnimateOut() {
            
        }

    }
}
