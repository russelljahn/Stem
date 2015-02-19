using Assets.Stem.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.Stem.Scripts {
    public class PingPongTweenLocalPosition : MonoBehaviour {

        public float Time = 1;

        public Vector3 EndLocalPosition = Vector3.down;
        public AnimationCurve Easing = AnimationCurveUtils.GetLinearCurve();

        private Vector3 _initiaLocalPosition;


        private void Awake() {
            _initiaLocalPosition = transform.localPosition;            
        }

        private void OnEnable() {
            PlayForwards();
        }


        private void PlayForwards() {
            TweenUtils.TweenLocalPosition(transform, EndLocalPosition, Time, Easing, () => { if (enabled) PlayBackwards(); });
        }


        private void PlayBackwards() {
            TweenUtils.TweenLocalPosition(transform, _initiaLocalPosition, Time, Easing, () => { if (enabled) PlayForwards(); });
        }
    }
}
