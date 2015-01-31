using Assets.Stem.Scripts.Extensions;
using Assets.Stem.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.Stem.Scripts.Gui.PivotAnimations {
    public class GetEbolaAnimation : PivotAnimation {

        [SerializeField] private Image _biohazardImage;

        [SerializeField] private float _tweenTime = 1f;
        [SerializeField] private AnimationCurve _tweenEasing = AnimationCurveUtils.GetLinearCurve();

        private Vector3 _initialLocalPosition;
        [SerializeField] private Vector3 _targetLocalPosition = new Vector3(0f, -100f, 0f);

        private Vector3 _initialLocalScale;


        private void OnEnable() {
            _initialLocalPosition = _biohazardImage.transform.localPosition;
            _initialLocalScale = _biohazardImage.transform.localScale;

            Length = _tweenTime;

            TweenUtils.TweenLocalScale(_biohazardImage.transform, Vector3.one, _tweenTime, _tweenEasing);
            TweenUtils.TweenLocalPosition(_biohazardImage.transform, _targetLocalPosition, _tweenTime, _tweenEasing);

            this.InvokeAfterTime(Length, RaiseFinishedEvent);
        }


        private void OnDisable() {
             _biohazardImage.transform.localPosition = _initialLocalPosition;
             _biohazardImage.transform.localScale = _initialLocalScale;
        }

    }
}
