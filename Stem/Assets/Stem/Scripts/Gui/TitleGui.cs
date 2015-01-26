using System;
using Assets.Stem.Scripts.Extensions;
using Assets.Stem.Scripts.Utils;
using UnityEngine;


namespace Assets.Stem.Scripts.Gui {
    [RequireComponent(typeof(CanvasGroup))]
    public class TitleGui : MonoBehaviour {

        private CanvasGroup _canvasGroup;
        [SerializeField] private SpriteRenderer _unicornSpriteRenderer;

        [SerializeField] private float _fadeTime = 1f;
        [SerializeField] private AnimationCurve _fadeEasing = AnimationCurveUtils.GetLinearCurve();

        public event Action Closed = delegate { };


        private void Awake() {
            _canvasGroup = GetComponent<CanvasGroup>();
        }


        private void Start() {
            this.InvokeAfterTime(2f, Fade);
        }


        private void Fade() {
            TweenUtils.TweenAlpha(_canvasGroup, 0f, _fadeTime, _fadeEasing, OnClose);
            TweenUtils.TweenAlpha(_unicornSpriteRenderer, 0f, _fadeTime, _fadeEasing);
        }


        private void OnClose() {
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;

            Closed.Invoke();
        }
    }
}
