using System.Security.Cryptography;
using Assets.GGJ2015.Scripts.Extensions;
using Assets.GGJ2015.Scripts.Utils;
using UnityEngine;


namespace Assets.GGJ2015.Scripts.Gui.PivotAnimations {
    public class GraveAnimation : PivotAnimation {

        [SerializeField] private float _growTime = 0.5f;
        [SerializeField] private AnimationCurve _growEasing = AnimationCurveUtils.GetLinearCurve();

        [SerializeField] private float _waitTime = 0.5f;


        [SerializeField] private float _graveFadeTime = 0.25f;
        [SerializeField] private AnimationCurve _graveFadeEasing = AnimationCurveUtils.GetLinearCurve();

        [SerializeField] private SpriteRenderer _graveSpriteRenderer;

        private Vector3 _initialScale;
        private Vector3 _initialLocalPosition;


        [SerializeField] private Animator _growFlowerAnimator;
        [SerializeField]private AnimationClip _growFlowerAnimationClip;

        [SerializeField] private SpriteRenderer _growFlowerSpriteRenderer;



        private void OnEnable() {
            _initialScale = _graveSpriteRenderer.transform.localScale;
            _initialLocalPosition = _graveSpriteRenderer.transform.localPosition;

            _graveSpriteRenderer.color = new Color(1f, 1f, 1f, 1f);

            _growFlowerSpriteRenderer.color = new Color(1f, 1f, 1f, 0f);
            Length = _growTime + _graveFadeTime + _waitTime;
            _graveSpriteRenderer.transform.localScale = Vector3.zero;
            TweenUtils.TweenLocalScale(_graveSpriteRenderer.transform, _initialScale, _growTime, _growEasing, Wait);
        }


        private void Wait() {
            this.InvokeAfterTime(_waitTime, FadeOutGrave);
        }

        private void FadeOutGrave() {
            TweenUtils.TweenAlpha(_graveSpriteRenderer, 0f, _graveFadeTime, _graveFadeEasing);
            TweenUtils.TweenAlpha(_growFlowerSpriteRenderer, 1f, _graveFadeTime, _graveFadeEasing);
            TweenUtils.TweenLocalPosition(_graveSpriteRenderer.transform, new Vector3(0f, -200f, 0f), _graveFadeTime, _graveFadeEasing, () => {
                _graveSpriteRenderer.transform.localPosition = _initialLocalPosition;
                RaiseFinishedEvent();
            });
        }

    }
}
