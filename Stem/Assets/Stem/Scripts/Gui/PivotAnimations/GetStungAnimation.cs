using Assets.Stem.Scripts.Extensions;
using Assets.Stem.Scripts.Utils;
using System.Collections.Generic;
using Assets.Stem.Scripts.PropertyAttributes;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.Stem.Scripts.Gui.PivotAnimations {
    public class GetStungAnimation : PivotAnimation {

        [SerializeField] private AnimationClip _getStungClip;

        [SerializeField] private float _fadeTime = 0.5f;
        [SerializeField] private AnimationCurve _fadeEasing = AnimationCurveUtils.GetLinearCurve();

        [SerializeField] private SpriteRenderer _getStungSpriteRenderer;
        [SerializeField] private SpriteRenderer _hospitalSpriteRenderer;

        [SerializeField] private Image _stingFlash;
        [SerializeField] private Color _stingFlashColor = Color.yellow;

        [SerializeField] private List<float> _stingFlashTimes;
        [SerializeField, Readonly] private float _time;
        [SerializeField, Readonly] private int _currentTimeIndex;

        private void OnEnable() {
            _getStungSpriteRenderer.color = Color.white;

            Length = _getStungClip.length;
            this.InvokeAfterTime(_getStungClip.length - _fadeTime, FadeToHospital);

            _hospitalSpriteRenderer.color = new Color(1f, 1f, 1f, 0f);

            _time = 0f;
            _currentTimeIndex = 0;
            _stingFlashTimes.Sort();
        }


        private void Update() {
            _time += Time.deltaTime;
            if (_currentTimeIndex < _stingFlashTimes.Count && _time >= _stingFlashTimes [_currentTimeIndex]) {
                _stingFlash.color = _stingFlashColor;
                ++_currentTimeIndex;
            }
            else {
                _stingFlash.color = Color.clear;                
            }
        }


        private void FadeToHospital() {
            _hospitalSpriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            TweenUtils.TweenAlpha(_getStungSpriteRenderer, 0f, _fadeTime, _fadeEasing, RaiseFinishedEvent);
        }

    }
}
