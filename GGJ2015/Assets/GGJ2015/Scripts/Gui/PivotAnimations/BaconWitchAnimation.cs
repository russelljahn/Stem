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


        [SerializeField] private float _ashesWaitTime = 0.5f;


        private void OnEnable() {
            _growFlowerAnimator.speed = 0;
            _baconWitchSpriteRenderer.color = Color.white;
            _growFlowerSpriteRenderer.color = new Color(0f, 0f, 0f, 0f);

            Length = _baconWitchAnimationClip.length + _growTime + _growFlowerAnimationClip.length;

            this.InvokeAfterTime(_baconWitchAnimationClip.length - _growTime, FadeOutWitch);
        }


        private void FadeOutWitch() {
            TweenUtils.TweenColor(_growFlowerSpriteRenderer, Color.black, _growTime, _growEasing);
            TweenUtils.TweenColor(_baconWitchSpriteRenderer, new Color(0f, 0f, 0f, 0f),  _growTime, _growEasing);
            this.InvokeAfterTime(_growTime + _ashesWaitTime, PlayFlowerAnimation);
        }



        private void PlayFlowerAnimation()
        {
            TweenUtils.TweenColor(_growFlowerSpriteRenderer, Color.white, _growFlowerAnimationClip.length, _growEasing);
            _growFlowerAnimator.speed = 1;
            this.InvokeAfterTime(_growFlowerAnimationClip.length, RaiseFinishedEvent);
        }

    }
}
