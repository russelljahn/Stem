using Assets.GGJ2015.Scripts.Extensions;
using Assets.GGJ2015.Scripts.Utils;
using UnityEngine;


namespace Assets.GGJ2015.Scripts.Gui.PivotAnimations {
    public class SpaceBeesAnimation : PivotAnimation {

        [SerializeField] private AnimationClip _spaceBeesClip;
        [SerializeField] private AnimationClip _flowerGrowsClip;

        [SerializeField] private Animator _flowerGrowsAnimator;

        [SerializeField] private float _fadeTime = 0.5f;
        [SerializeField] private AnimationCurve _fadeEasing = AnimationCurveUtils.GetLinearCurve();

        [SerializeField] private SpriteRenderer _spaceBeesSpriteRenderer;
        [SerializeField] private SpriteRenderer _flowerGrowsSpriteRenderer;



        private void OnEnable() {
            Length = _spaceBeesClip.length + _flowerGrowsClip.length - _fadeTime;
            _flowerGrowsAnimator.speed = 0f;
            this.InvokeAfterTime(_spaceBeesClip.length - _fadeTime, FadeToFlowerGrows);

            _flowerGrowsSpriteRenderer.color = new Color(1f, 1f, 1f, 0f);
        }


        private void FadeToFlowerGrows() {
            _flowerGrowsSpriteRenderer.color = new Color(1f, 1f, 1f, 1f);

            TweenUtils.TweenAlpha(_spaceBeesSpriteRenderer, 0f, _fadeTime, _fadeEasing);

            _flowerGrowsAnimator.speed = 1f;
            this.InvokeAfterTime(_flowerGrowsClip.length, RaiseFinishedEvent);

        }

    }
}
