using Assets.GGJ2015.Scripts.Extensions;
using Assets.GGJ2015.Scripts.Utils;
using UnityEngine;


namespace Assets.GGJ2015.Scripts.Gui.PivotAnimations {
    public class GetBusyAnimation : PivotAnimation {

        [SerializeField] private AnimationClip _flytrapWaggleClip;
        [SerializeField] private AnimationClip _heartsClip;

        [SerializeField] private Animator _heartsAnimator;


        [SerializeField] private float _shortenFlytrapWaggleAnimationTime = .5f;
        [SerializeField]private float _shortenHeartsAnimationTime = .15f;


        [SerializeField] private float _fadeTime = 0.5f;
        [SerializeField] private AnimationCurve _fadeEasing = AnimationCurveUtils.GetLinearCurve();

        [SerializeField] private SpriteRenderer _flytrapWaggleSpriteRenderer;
        [SerializeField] private SpriteRenderer _heartsSpriteRenderer;



        private void OnEnable() {
            Length = _flytrapWaggleClip.length + _heartsClip.length + 2f*_fadeTime - _shortenFlytrapWaggleAnimationTime;
            _heartsAnimator.speed = 0f;
            this.InvokeAfterTime(_flytrapWaggleClip.length - _shortenFlytrapWaggleAnimationTime, FadeInHearts);

            _heartsSpriteRenderer.color = new Color(1f, 1f, 1f, 0f);
        }


        private void FadeInHearts() {
            TweenUtils.TweenAlpha(_heartsSpriteRenderer, 1f, _fadeTime, _fadeEasing);
            _heartsAnimator.speed = 1f;
            this.InvokeAfterTime(_heartsClip.length - _shortenHeartsAnimationTime, FadeOutHearts);

        }


        private void FadeOutHearts() {
            TweenUtils.TweenAlpha(_heartsSpriteRenderer, 0f, _fadeTime, _fadeEasing, RaiseFinishedEvent);
        }

    }
}
