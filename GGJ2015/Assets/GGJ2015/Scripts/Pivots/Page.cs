using System;
using System.Collections.Generic;
using Assets.GGJ2015.Scripts.Audio;
using Assets.GGJ2015.Scripts.Extensions;
using Assets.GGJ2015.Scripts.Gui;
using Assets.GGJ2015.Scripts.Gui.PivotAnimations;
using Assets.GGJ2015.Scripts.PropertyAttributes;
using Assets.GGJ2015.Scripts.Utils;
using UnityEngine;


namespace Assets.GGJ2015.Scripts.Pivots {
    [RequireComponent(typeof(CanvasGroup))]
    public class Page : MonoBehaviour {
        [SerializeField] private List<ChoiceGui> _choiceGuis = new List<ChoiceGui>();
        [SerializeField] private AudioManager _audioManager;


        [SerializeField, Readonly] private Pivot _currentPivot;
        [SerializeField, Readonly] private Story _currentStory;
        [SerializeField, Readonly] private PivotAnimation _currentPivotAnimation;


        private Choice _previousChoice;
        [SerializeField] private float _guiFadeTime = 1f;
        [SerializeField] private float _musicFadeTime = 1f;
        [SerializeField] private AnimationCurve _musicFadeEasing = AnimationCurveUtils.GetLinearCurve();


        public void Setup(Story story, PivotAnimation initialAnimation) {
            _currentStory = story;
            _currentPivotAnimation = initialAnimation;
        }


        private void Start() {
            var normalBgTrackId = AudioClips.GetClipTrackId(AudioClips.BgNormal);
            var nextChoiceClip = AudioClips.GetClip(AudioClips.BgNormal);

            _audioManager.LoadClip(normalBgTrackId, nextChoiceClip, 0f, true);
            _audioManager.PlayTrack(normalBgTrackId);
            _audioManager.Fade(normalBgTrackId, _musicFadeTime, _musicFadeEasing);
        }


        public void LoadPivot(Pivot pivot) {
            if (_choiceGuis.Count != 2) {
                Debug.LogError("Choices text count isn't 2!");
            }
            if (pivot.Choices.Count != 2) {
                Debug.LogError(string.Format("'{0}' doesn't have 2 choices!", pivot.Id));
            }

            _currentPivot = pivot;

            for (int i = 0; i < pivot.Choices.Count; ++i) {
                var choiceGui = _choiceGuis [i];
                var choice = pivot.Choices[i];
                choiceGui.LoadChoice(choice);
            }
        }


        private void UnloadCurrentPivot(Action onComplete = null) {
            if (_currentPivot.IsNotNull()) {
                for (int i = 0; i < _currentPivot.Choices.Count; ++i) {
                    var choiceGui = _choiceGuis [i];
                    choiceGui.ChoiceClicked -= OnClickChoice;
                    choiceGui.AnimateOut(_guiFadeTime);
                }
                _currentPivot = null;
            }
            if (onComplete.IsNotNull()) {
                this.InvokeAfterTime(_guiFadeTime, onComplete);
            }
        }


        public void AnimatePivotTransition(Pivot pivot) {
            _currentPivotAnimation.Play();
            var guiAnimationWaitTime = _currentPivotAnimation.Length - _guiFadeTime;
            this.InvokeAfterTime(guiAnimationWaitTime, () => {
                for (int i = 0; i < pivot.Choices.Count; ++i) {
                    var choiceGui = _choiceGuis[i];
                    choiceGui.AnimateIn(_guiFadeTime, () => { choiceGui.ChoiceClicked += OnClickChoice; });
                }
            });
        }


        private void OnClickChoice(Choice choice) {
            var pivot = _currentStory.GetPivot(choice.NextPivot);
            HandleOnClickChoiceAudio(choice);

            UnloadCurrentPivot(() => {
                LoadPivot(pivot); 
                AnimatePivotTransition(pivot);
            });
            //_currentPivotAnimation = choice.PivotAnimation; //TODO: Implement!
            _previousChoice = choice;
        }


        private void HandleOnClickChoiceAudio(Choice choice) {
            _audioManager.PlayTrackOneShot(AudioClips.SfxButton);

            //var nextChoiceClip = AudioClips.GetClip(choice.OnTriggerTrackName);
            //var nextChoiceTrackId = AudioClips.GetClipTrackId(choice.OnTriggerTrackName);
            //_audioManager.Fade(normalBgTrackId, _musicFadeTime, _musicFadeEasing);
        }


        


    }
}
