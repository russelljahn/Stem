using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Stem.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.Stem.Scripts {
    public class PingPongTweenGraphicColor : MonoBehaviour {

        public float Time = 1;

        public Graphic Graphic;
        public Color EndColor = Color.white;
        public AnimationCurve Easing = AnimationCurveUtils.GetLinearCurve();

        private Color _initialColor;


        private void Awake() {
            _initialColor = Graphic.color;            
        }

        private void OnEnable() {
            PlayForwards();
        }


        private void PlayForwards() {
            TweenUtils.TweenColor(Graphic, EndColor, Time, Easing, () => { if (enabled) PlayBackwards(); });
        }


        private void PlayBackwards() {
            TweenUtils.TweenColor(Graphic, _initialColor, Time, Easing, () => { if (enabled) PlayForwards(); });
        }
    }
}
