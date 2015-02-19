using Assets.Stem.Scripts.Audio;
using Assets.Stem.Scripts.Extensions;
using Assets.Stem.Scripts.PropertyAttributes;
using Assets.Stem.Scripts.Utils;
using UnityEngine;


namespace Assets.Stem.Scripts.Gui.PivotAnimations {
    public class HoneySplatAnimation : PivotAnimation {

        [SerializeField] private AnimationClip _splatAnimationClip;
        //[SerializeField] private AnimationClip _getSickAnimationClip;
        [SerializeField] private Animator _splatAnimator;

        [SerializeField, Max(0f)] private float _splatDripDistance = -50f;
        [SerializeField, Min(0f)] private float _splatDripTime = 1f;
        [SerializeField] private AnimationCurve _splatDripEasing = AnimationCurveUtils.GetLinearCurve();





        [SerializeField] private SpriteRenderer _splatSpriteRenderer;
        //[SerializeField] private SpriteRenderer _getSickSpriteRenderer;
        //[SerializeField] private SpriteRenderer _hospitalSpriteRenderer;

        [SerializeField] private float _strawSfxPlayDelay = 0f;

        [SerializeField] private float _splatSpeed = 1.0f;



        private void OnEnable() {
            _splatAnimator.speed = _splatSpeed;
            TweenUtils.TweenLocalPosition(transform, new Vector3(0f, _splatDripDistance, 0f), _splatDripTime, _splatDripEasing);

            //_drinkSpriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            //_getSickSpriteRenderer.color = new Color(1f, 1f, 1f, 0f);
            //_hospitalSpriteRenderer.color = new Color(1f, 1f, 1f, 0f);

            //_getSickAnimator.speed = 0f;

            //Length = _drinkAnimationClip.length + _getSickAnimationClip.length + _fadeTime + _waitTime;
            //this.InvokeAfterTime(_drinkAnimationClip.length, GetSickAnimation);
            //this.InvokeAfterTime(_strawSfxPlayDelay, () => AudioManager.PlayTrackOneShot(AudioClips.SfxStraw));
        }


        //private void GetSickAnimation() {
        //    _drinkSpriteRenderer.color = new Color(1f, 1f, 1f, 0f);
        //    _getSickSpriteRenderer.color = new Color(1f, 1f, 1f, 1f);

        //    _getSickAnimator.speed = 1f;

        //    this.InvokeAfterTime(_getSickAnimationClip.length + _waitTime, FadeInHospital);
        //}


        //private void FadeInHospital() {
        //    TweenUtils.TweenAlpha(_getSickSpriteRenderer, 0f, _fadeTime, _fadeEasing);
        //    TweenUtils.TweenAlpha(_hospitalSpriteRenderer, 1f, _fadeTime, _fadeEasing, RaiseFinishedEvent);
        //}

    }
}
