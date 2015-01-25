using System;
using System.Collections.Generic;
using Assets.GGJ2015.Scripts.Audio;
using Assets.GGJ2015.Scripts.Extensions;
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
        [SerializeField, Readonly] private AudioManager _audioManager;

        private Choice _previousChoice;

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

            HandleOnClickChoiceAudio(choice);

            UnloadCurrentPivot(() => { LoadPivot(pivot); });
            _previousChoice = choice;
        }


        private void HandleOnClickChoiceAudio(Choice choice) {
            var buttonClickClip = AudioClips.GetClip(AudioClips.ButtonClick);
            var buttonClickTrackId = AudioClips.GetClipTrackId(AudioClips.ButtonClick);
            _audioManager.LoadClip(buttonClickTrackId, buttonClickClip);
            _audioManager.PlayTrackOneShot(buttonClickTrackId);

            var nextChoiceClip = AudioClips.GetClip(choice.OnTriggerTrackName);
            var nextChoiceTrackId = AudioClips.GetClipTrackId(choice.OnTriggerTrackName);
            var previousChoiceTrackId = AudioClips.GetClipTrackId(_previousChoice.OnTriggerTrackName);

            _audioManager.LoadClip(nextChoiceTrackId, nextChoiceClip, 0.0f, true);
            _audioManager.PlayTrack(previousChoiceTrackId);
            _audioManager.PlayTrack(nextChoiceTrackId);

            if (_previousChoice.IsNotNull()) {
                _audioManager.Crossfade(nextChoiceTrackId, previousChoiceTrackId);
            }
        }


    }
}
