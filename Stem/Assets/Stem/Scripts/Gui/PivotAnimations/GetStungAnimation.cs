using Assets.Stem.Scripts.Extensions;
using Assets.Stem.Scripts.Utils;
using UnityEngine;


namespace Assets.Stem.Scripts.Gui.PivotAnimations {
    public class GetStungAnimation : PivotAnimation {

        [SerializeField] private AnimationClip _getStungClip;

        [SerializeField] private float _fadeTime = 0.5f;
        [SerializeField] private AnimationCurve _fadeEasing = AnimationCurveUtils.GetLinearCurve();

        [SerializeField] private SpriteRenderer _getStungSpriteRenderer;
        [SerializeField] private SpriteRenderer _hospitalSpriteRenderer;



        private void OnEnable() {
            _getStungSpriteRenderer.color = Color.white;

            Length = _getStungClip.length;
            this.InvokeAfterTime(_getStungClip.length - _fadeTime, FadeToHospital);

            _hospitalSpriteRenderer.color = new Color(1f, 1f, 1f, 0f);
        }


        private void FadeToHospital() {
            _hospitalSpriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            TweenUtils.TweenAlpha(_getStungSpriteRenderer, 0f, _fadeTime, _fadeEasing, RaiseFinishedEvent);
        }

    }
}
