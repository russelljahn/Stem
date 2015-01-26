using System;
using System.ComponentModel;
using Assets.Stem.Scripts.Extensions;
using Assets.Stem.Scripts.PropertyAttributes;
using Assets.Stem.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.Stem.Scripts.Gui {
    [RequireComponent(typeof(CanvasGroup))]
    public class MenuGui : MonoBehaviour {

        private CanvasGroup _canvasGroup;
        [SerializeField] private Button _menuFadeInButton;
        [SerializeField] private Button _menuFadeOutButton;
        [SerializeField] private Button _quitButton;



        [SerializeField] private float _fadeTime = 1f;
        [SerializeField] private AnimationCurve _fadeEasing = AnimationCurveUtils.GetLinearCurve();


        private void Awake() {
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 0f;
            DisableInteraction();
        }


        private void OnEnable() {
            _menuFadeInButton.onClick.AddListener(FadeIn);
            _menuFadeOutButton.onClick.AddListener(FadeOut);
            _quitButton.onClick.AddListener(Application.Quit);
        }


        private void OnDisable() {
            _menuFadeInButton.onClick.RemoveListener(FadeIn);
            _menuFadeOutButton.onClick.RemoveListener(FadeOut);
            _quitButton.onClick.RemoveListener(Application.Quit);
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
