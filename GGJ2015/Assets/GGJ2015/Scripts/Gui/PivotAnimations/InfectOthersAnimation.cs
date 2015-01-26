using System.Security.Cryptography;
using Assets.GGJ2015.Scripts.Extensions;
using Assets.GGJ2015.Scripts.Utils;
using UnityEngine;


namespace Assets.GGJ2015.Scripts.Gui.PivotAnimations {
    public class InfectOthersAnimation : PivotAnimation {

        [SerializeField] private float _shrinkTime = 0.5f;
        [SerializeField] private AnimationCurve _shrinkEasing = AnimationCurveUtils.GetLinearCurve();


        [SerializeField] private float _textFadeTime = 0.25f;
        [SerializeField] private AnimationCurve _textFadeEasing = AnimationCurveUtils.GetLinearCurve();

        [SerializeField] private SpriteRenderer _ebolaSpriteRenderer;
        [SerializeField] private SpriteRenderer _textSpriteRenderer;


        private Vector3 _initialScale;



        private void OnEnable() {
            _initialScale = _ebolaSpriteRenderer.transform.localScale;

            _ebolaSpriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            _textSpriteRenderer.color = new Color(1f, 1f, 1f, 0f);
            Length = _shrinkTime + _textFadeTime;

            TweenUtils.TweenLocalScale(_ebolaSpriteRenderer.transform, Vector3.zero, _shrinkTime, _shrinkEasing, FadeInText);
        }


        private void FadeInText() {
            TweenUtils.TweenAlpha(_textSpriteRenderer, 1f, _textFadeTime, _textFadeEasing, RaiseFinishedEvent);
        }


        private void OnDisable() {
            _ebolaSpriteRenderer.transform.localScale = _initialScale;
            _textSpriteRenderer.color = new Color(1f, 1f, 1f, 0f);
        }

    }
}
