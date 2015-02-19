using Assets.Stem.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.Stem.Scripts {
    public class PingPongTweenLocalRotation : MonoBehaviour {

        public float Time = 1;

        public Vector3 EndLocalRotation = Vector3.zero;
        public AnimationCurve Easing = AnimationCurveUtils.GetLinearCurve();

        private Vector3 _initiaLocalRotation;


        private void Awake() {
            _initiaLocalRotation = transform.localRotation.eulerAngles;            
        }

        private void OnEnable() {
            PlayForwards();
        }


        private void PlayForwards() {
            TweenUtils.TweenLocalRotation(transform, EndLocalRotation, Time, Easing, () => { if (enabled) PlayBackwards(); });
        }


        private void PlayBackwards() {
            TweenUtils.TweenLocalRotation(transform, _initiaLocalRotation, Time, Easing, () => { if (enabled) PlayForwards(); });
        }
    }
}
