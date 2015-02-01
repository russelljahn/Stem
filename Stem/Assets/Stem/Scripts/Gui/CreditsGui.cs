using System.Collections.Generic;
using Assets.Stem.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.Stem.Scripts.Gui {
    [RequireComponent(typeof(CanvasGroup))]
    public class CreditsGui : MonoBehaviour {

        private CanvasGroup _canvasGroup;
        [SerializeField] private Button _fadeInButton;
        [SerializeField] private Button _fadeOutButton;

        [SerializeField] private float _fadeTime = 1f;
        [SerializeField] private AnimationCurve _fadeEasing = AnimationCurveUtils.GetLinearCurve();


        private void Awake() {
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 0f;
            DisableInteraction();
        }


        private void OnEnable() {
            _fadeInButton.onClick.AddListener(FadeIn);
            _fadeOutButton.onClick.AddListener(FadeOut);
        }


        private void OnDisable() {
            _fadeInButton.onClick.RemoveListener(FadeIn);
            _fadeOutButton.onClick.RemoveListener(FadeOut);
        }


        private void FadeIn() {
            EnableInteraction();
            TweenUtils.TweenAlpha(_canvasGroup, 1f, _fadeTime, _fadeEasing);
        }


        private void FadeOut() {
            DisableInteraction();
            TweenUtils.TweenAlpha(_canvasGroup, 0f, _fadeTime, _fadeEasing);
        }


        private void EnableInteraction() {
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.interactable = true;
        }


        private void DisableInteraction() {
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.interactable = false;
        }

    }
}
