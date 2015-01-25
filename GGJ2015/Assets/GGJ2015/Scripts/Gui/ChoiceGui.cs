using System;
using Assets.GGJ2015.Scripts.Pivots;
using Assets.GGJ2015.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.GGJ2015.Scripts.Gui {

	[RequireComponent(typeof(CanvasGroup))]
	[RequireComponent(typeof(Text))]
    [RequireComponent(typeof(Button))]

    public class ChoiceGui : MonoBehaviour {

		[SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Text _text;
        [SerializeField] private Button _button;
		[SerializeField] private AnimationCurve _animateInEasing = AnimationCurveUtils.GetLinearCurve();
		[SerializeField] private AnimationCurve _animateOutEasing = AnimationCurveUtils.GetLinearCurve();
        private Choice _choice;


        public event Action<Choice> ChoiceClicked = delegate { }; 


        private void Reset() {
			_canvasGroup = GetComponent<CanvasGroup>();
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


        public void AnimateIn(float time = 1.0f, Action onComplete = null) {
            Debug.Log("Animating in!");
            TweenUtils.TweenAlpha(_canvasGroup, 1f, time, _animateInEasing, onComplete);
        }


		public void AnimateOut(float time = 1.0f, Action onComplete = null) {
            TweenUtils.TweenAlpha(_canvasGroup, 0f, time, _animateOutEasing, onComplete);
		}		
	}
}
