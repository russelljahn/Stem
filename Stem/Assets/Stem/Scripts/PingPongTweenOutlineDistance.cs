using Assets.Stem.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.Stem.Scripts {
    public class PingPongTweenOutlineDistance : MonoBehaviour {

        public float Time = 1;

        public Outline Outline;
        public Vector2 EndDistance = Vector2.one;
        public AnimationCurve Easing = AnimationCurveUtils.GetLinearCurve();

        private Vector2 _initialDistance;


        private void Awake() {
            _initialDistance = Outline.effectDistance;            
        }

        private void OnEnable() {
            PlayForwards();
        }


        private void PlayForwards() {
            TweenUtils.TweenSize(Outline, EndDistance, Time, Easing, () => { if (enabled) PlayBackwards(); });
        }


        private void PlayBackwards() {
            TweenUtils.TweenSize(Outline, _initialDistance, Time, Easing, () => { if (enabled) PlayForwards(); });
        }
    }
}
