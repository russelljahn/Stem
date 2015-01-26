using System.Security.Cryptography;
using Assets.Stem.Scripts.Extensions;
using Assets.Stem.Scripts.Utils;
using UnityEngine;


namespace Assets.Stem.Scripts.Gui.PivotAnimations {
    public class FriendDrinkAnimation : PivotAnimation {

        [SerializeField] private AnimationClip _friendDrinkAnimationClip;
        //[SerializeField] private Animator _friendDrinkAnimator;

        [SerializeField] private float _faceShortenAnimationTime = 1.0f;

        [SerializeField] private float _faceFadeInTime = 0.25f;
        [SerializeField] private float _faceFadeOutTime = 0.25f;
        [SerializeField] private AnimationCurve _fadeEasing = AnimationCurveUtils.GetLinearCurve();

        [SerializeField] private SpriteRenderer _peeSpriteRenderer;
        [SerializeField] private SpriteRenderer _friendDrinkSpriteRenderer;
        [SerializeField] private SpriteRenderer _gravestoneSpriteRenderer;


        private void OnEnable() {
            _peeSpriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            _friendDrinkSpriteRenderer.color = new Color(1f, 1f, 1f, 0f);
            _gravestoneSpriteRenderer.color = new Color(1f, 1f, 1f, 0f);

            Length = _friendDrinkAnimationClip.length + _faceFadeInTime + _faceFadeOutTime - _faceShortenAnimationTime;
            TweenUtils.TweenAlpha(_friendDrinkSpriteRenderer, 1f, _faceFadeInTime, _fadeEasing, GetSickAnimation);
        }


        private void GetSickAnimation() {
            _peeSpriteRenderer.color = new Color(1f, 1f, 1f, 0f);

            this.InvokeAfterTime(_friendDrinkAnimationClip.length - _faceFadeInTime - _faceShortenAnimationTime, FadeInGravestone);
        }


        private void FadeInGravestone() {
            TweenUtils.TweenAlpha(_friendDrinkSpriteRenderer, 0f, _faceFadeOutTime, _fadeEasing);
            TweenUtils.TweenAlpha(_gravestoneSpriteRenderer, 1f, _faceFadeOutTime, _fadeEasing, RaiseFinishedEvent);
        }

    }
}
