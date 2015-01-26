using System.Security.Cryptography;
using Assets.GGJ2015.Scripts.Extensions;
using Assets.GGJ2015.Scripts.Utils;
using UnityEngine;


namespace Assets.GGJ2015.Scripts.Gui.PivotAnimations {
    public class BaconWitchAnimation : PivotAnimation {

        [SerializeField]
        private float _growTime = 0.5f;
        [SerializeField]
        private AnimationCurve _growEasing = AnimationCurveUtils.GetLinearCurve();

        [SerializeField]
        private float _waitTime = 0.5f;


        [SerializeField]
        private float _graveFadeTime = 0.25f;
        [SerializeField]
        private AnimationCurve _graveFadeEasing = AnimationCurveUtils.GetLinearCurve();

        [SerializeField]
        private SpriteRenderer _graveSpriteRenderer;

        private Vector3 _initialLocalPosition;

        [SerializeField]
        private AnimationClip _baconWitchAnimationClip;
        [SerializeField]
        private SpriteRenderer _baconWitchSpriteRenderer;


        [SerializeField]
        private Animator _growFlowerAnimator;
        [SerializeField]
        private AnimationClip _growFlowerAnimationClip;

        [SerializeField]
        private SpriteRenderer _growFlowerSpriteRenderer;



        private void OnEnable() {
            _growFlowerAnimator.speed = 0;
            _initialLocalPosition = _graveSpriteRenderer.transform.localPosition;

            _growFlowerSpriteRenderer.color = new Color(1f, 1f, 1f, 0f);
            _graveSpriteRenderer.color = new Color(0f, 0f, 0f, 0f);

            Length = _growTime + _graveFadeTime + _baconWitchAnimationClip.length;
            _graveSpriteRenderer.transform.localScale = Vector3.zero;

            this.InvokeAfterTime(_baconWitchAnimationClip.length - _graveFadeTime, FadeOutWitch);
        }


        private void FadeOutWitch() {
            TweenUtils.TweenAlpha(_baconWitchSpriteRenderer, 0f, _graveFadeTime, _graveFadeEasing, FadeOutGrave);
            //TweenUtils.TweenColor(_graveSpriteRenderer, Color.white, _graveFadeTime, _graveFadeEasing, Wait);
            //TweenUtils.TweenAlpha(_graveSpriteRenderer, 1f, _graveFadeTime, _graveFadeEasing);

        }


        //private void Wait() {
        //    this.InvokeAfterTime(_waitTime, FadeOutGrave);
        //}


        private void FadeOutGrave() {
            //TweenUtils.TweenAlpha(_graveSpriteRenderer, 0f, _graveFadeTime, _graveFadeEasing, RaiseFinishedEvent);
            TweenUtils.TweenAlpha(_growFlowerSpriteRenderer, 1f, _graveFadeTime, _graveFadeEasing, RaiseFinishedEvent);
            TweenUtils.TweenLocalPosition(_graveSpriteRenderer.transform, new Vector3(0f, -200f, 0f), _graveFadeTime, _graveFadeEasing, () => {
                _graveSpriteRenderer.transform.localPosition = _initialLocalPosition;
                _growFlowerAnimator.speed = 1;

                RaiseFinishedEvent();
            });
        }

    }
}
