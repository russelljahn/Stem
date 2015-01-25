using System;
using System.Collections.Generic;
using Assets.GGJ2015.Scripts.Gui;
using Assets.GGJ2015.Scripts.PropertyAttributes;
using UnityEngine;


namespace Assets.GGJ2015.Scripts.Pivots {
    [RequireComponent(typeof(CanvasGroup))]
    public class Page : MonoBehaviour {
        public List<ChoiceGui> ChoiceGuis = new List<ChoiceGui>();
        public PivotGraphics PivotGraphics;

        [SerializeField, Readonly] private Pivot _currentPivot;
        [SerializeField, Readonly] private Story _currentStory;


        public void Setup(Story story) {
            _currentStory = story;
        }


        public void LoadPivot(Pivot pivot) {
            if (ChoiceGuis.Count != 2) {
                Debug.LogError("Choices text count isn't 2!");
            }
            if (pivot.Choices.Count != 2) {
                Debug.LogError(string.Format("'{0}' doesn't have 2 choices!", pivot.Id));
            }

            _currentPivot = pivot;

            for (int i = 0; i < pivot.Choices.Count; ++i) {
                var choiceGui = ChoiceGuis [i];
                var choice = pivot.Choices[i];

                choiceGui.LoadChoice(choice);
                choiceGui.AnimateIn();

                //TODO: Make this a callback after animating in
                choiceGui.ChoiceClicked += OnClickChoice;
            }
        }


        private void UnloadCurrentPivot(Action onComplete = null) {
            for (int i = 0; i < _currentPivot.Choices.Count; ++i) {
                var choiceGui = ChoiceGuis[i];
                choiceGui.ChoiceClicked -= OnClickChoice;
            }
            _currentPivot = null;
            if (onComplete != null) {
                onComplete.Invoke();                
            }
        }


        private void OnClickChoice(Choice choice) {
            var pivot = _currentStory.GetPivot(choice.NextPivot);
            UnloadCurrentPivot(() => { LoadPivot(pivot); });
        }


    }
}
