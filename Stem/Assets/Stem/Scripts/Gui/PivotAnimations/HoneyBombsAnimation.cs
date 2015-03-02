using System.Linq;
using System.Security.Cryptography;
using Assets.Stem.Scripts.Audio;
using Assets.Stem.Scripts.Extensions;
using Assets.Stem.Scripts.Utils;
using System.Collections.Generic;
using Assets.Stem.Scripts.PropertyAttributes;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.Stem.Scripts.Gui.PivotAnimations {
    public class HoneyBombsAnimation : PivotAnimation {

        [SerializeField] private Animator _flowerGoopAnimator;
        [SerializeField] private AnimationClip _flowerGoopAnimationClip;

        [SerializeField] private SpriteRenderer _flowerGoopSpriteRenderer;
        [SerializeField] private SpriteRenderer _flowerBgSpriteRenderer;

        [SerializeField] private SpriteRenderer _beeSpriteRenderer;

        [SerializeField] private Image _bgImage;


        [SerializeField] private Transform _honeyBombContainer;
        [SerializeField] private Transform _honeySplatContainer;



        [SerializeField] private Vector3 _honeyBombContainerInitialPositionViewportCoord = new Vector3(-0.5f,  1.5f,  0.5f);
        [SerializeField] private Vector3 _honeyBombContainerEndPositionViewportCoord     = new Vector3( 1.5f, -0.5f,  0.5f);

        [SerializeField] private Vector3 _honeySplatEndLocalPosition    = new Vector3( 0f, -1.0f,  0.0f);
        [SerializeField] private Vector3 _beeEndPositionViewportCoord = new Vector3( 1.5f, 0.5f, 0.5f);

        [SerializeField] private Vector3 _beeEndLocalScale = new Vector3(0.25f, 0.25f, 1);

        private Color _flowerGoopInitialColor = Color.yellow;
        private Color _bgImageInitialColor = Color.red;



        [SerializeField] private float _honeyBombContainerLaunchTime = 0.5f;
        [SerializeField] private float _beeAngerFadeTime = 1f;

        [SerializeField] private float _honeySplatTimeRange = 2.0f;
        [SerializeField] private float _honeySplatFadeTime = 2.0f;

        [SerializeField] private float _flowerGoopPlaybackDelayTime = 2.0f;
        [SerializeField] private float _flowerGoopFadeDelayTime = 2.0f;
        [SerializeField] private float _flowerGoopFadeTime = 2.0f;

        [SerializeField, Min(0f)] private float _shortenAnimationTime = 2.0f;


        [SerializeField] private AnimationCurve _honeyBombContainerLaunchEasing = AnimationCurveUtils.GetLinearCurve();
        [SerializeField] private AnimationCurve _beeAngerFadeEasing = AnimationCurveUtils.GetLinearCurve();
        [SerializeField] private AnimationCurve _honeySplatFadeEasing = AnimationCurveUtils.GetLinearCurve();
        [SerializeField] private AnimationCurve _flowerGoopFadeEasing = AnimationCurveUtils.GetLinearCurve();



        private Vector3 _initialBeeLocalScale;
        private Vector3 _initialBeeLocalPosition;


        private Color _initialHoneySplatColor;

        private bool _firstPlay = true;


        private void OnEnable() {
            Length = _honeyBombContainerLaunchTime + _honeySplatTimeRange + _flowerGoopPlaybackDelayTime + _flowerGoopFadeDelayTime + _flowerGoopFadeTime - _shortenAnimationTime;

            _initialBeeLocalPosition = _beeSpriteRenderer.transform.localPosition;
            _initialBeeLocalScale = _beeSpriteRenderer.transform.localScale;
            _beeSpriteRenderer.color = Color.white;

            // Randomize the playback times of each honeybomb animation
            var honeyBombAnimations = _honeyBombContainer.GetComponentsInChildren<Animator>();
            foreach (var anim in honeyBombAnimations) {
                var state = anim.GetCurrentAnimatorStateInfo(0);
                anim.Play(state.nameHash, 0, Random.value);
            }

            var honeySplatAnimations = _honeySplatContainer.GetComponentsInChildren<Animator>();
            foreach (var anim in honeySplatAnimations) {
                anim.speed = 0f;
                var spriteRenderer = anim.GetComponent<SpriteRenderer>();
                spriteRenderer.enabled = false;

                if (_firstPlay) {
                    _initialHoneySplatColor = spriteRenderer.color;
                }
                else {
                    spriteRenderer.color = _initialHoneySplatColor;
                }
            }

            _flowerGoopAnimator.speed = 0.0f;
            if (_firstPlay) {
                _flowerGoopInitialColor = _flowerGoopSpriteRenderer.color;
                _bgImageInitialColor = _bgImage.color;
            }
            else {
                _flowerGoopSpriteRenderer.color = _flowerGoopInitialColor;
                _bgImage.color = _bgImageInitialColor;
            }
            _flowerBgSpriteRenderer.color = Color.clear;
            _bgImage.color = new Color(1f, 1f, 1f, 0f);

            _firstPlay = false;

            AudioManager.LoadClip(AudioClips.BgHoneyBombs);
            AudioManager.PlayTrack(AudioClips.BgHoneyBombs);
            AudioManager.Crossfade(AudioClips.BgNormal, AudioClips.BgHoneyBombs);

            /*
             * Ideally we want to do the following right now in OnEnable(), but setting position somehow fails on the first frame of OnEnable()
             * and we can't use Start() b/c it isn't kicked off when re-enabling this script. 
             * So we just need to wait until next frame to do the following.
             * */
            this.InvokeAfterTime(0.01f, () =>
            {
                var mainCamera = Camera.main;

                TweenUtils.TweenColor(_beeSpriteRenderer, Color.red, 0.5f*_beeAngerFadeTime, _beeAngerFadeEasing);
                TweenUtils.TweenWorldPosition(_beeSpriteRenderer.transform, mainCamera.ViewportToWorldPoint(_beeEndPositionViewportCoord), _beeAngerFadeTime, _beeAngerFadeEasing);
                TweenUtils.TweenLocalScale(_beeSpriteRenderer.transform, _beeEndLocalScale, _beeAngerFadeTime, _beeAngerFadeEasing);

                TweenUtils.TweenColor(_bgImage, _bgImageInitialColor, 0.5f * _beeAngerFadeTime, _beeAngerFadeEasing);

                _honeyBombContainer.position = mainCamera.ViewportToWorldPoint(_honeyBombContainerInitialPositionViewportCoord);
                var honeyBombEndPosition = mainCamera.ViewportToWorldPoint(_honeyBombContainerEndPositionViewportCoord);
                TweenUtils.TweenWorldPosition(_honeyBombContainer, honeyBombEndPosition, _honeyBombContainerLaunchTime, _honeyBombContainerLaunchEasing);
                this.InvokeAfterTime(0.75f*_honeyBombContainerLaunchTime, PlayHoneySplatAnimations);
            });
        }


        private void PlayHoneySplatAnimations() {
            var honeySplatAnimations = new List<Animator>();
            _honeySplatContainer.GetComponentsInChildren(honeySplatAnimations);

            honeySplatAnimations.Sort((lhs, rhs) => Random.Range(-1, 2));

            for (int i = 0; i < honeySplatAnimations.Count; ++i) {
                var anim = honeySplatAnimations[i];
                var playbackTime = i/(float) honeySplatAnimations.Count * _honeySplatTimeRange;
                this.InvokeAfterTime(playbackTime, () => {
                    anim.speed = 1;
                    var spriteRenderer = anim.GetComponent<SpriteRenderer>();
                    spriteRenderer.enabled = true;
                    var initialPosition = anim.transform.localPosition;
                    TweenUtils.TweenLocalPosition(anim.transform, anim.transform.localPosition + _honeySplatEndLocalPosition, _honeySplatFadeTime, _honeySplatFadeEasing);
                    this.InvokeAfterTime(0.75f*_honeySplatFadeTime, () => {
                        TweenUtils.TweenColor(spriteRenderer, new Color(1f, 1f, 1f, 0f), 0.25f*_honeySplatFadeTime, _honeySplatFadeEasing, () => {
                            anim.transform.localPosition = initialPosition;
                        });                        
                    });
                });
            }
            this.InvokeAfterTime(_honeySplatTimeRange + _flowerGoopPlaybackDelayTime, PlayFlowerGoopAnimation);
        }


        private void PlayFlowerGoopAnimation() {
            _flowerGoopAnimator.speed = 1.0f;
            this.InvokeAfterTime(_flowerGoopFadeDelayTime, () =>
            {
                _flowerBgSpriteRenderer.color = Color.white;
                TweenUtils.TweenColor(_bgImage, Color.clear, _flowerGoopFadeTime, _flowerGoopFadeEasing);
                TweenUtils.TweenColor(_flowerGoopSpriteRenderer, new Color(1f, 1f, 1f, 0f), _flowerGoopFadeTime, _flowerGoopFadeEasing, RaiseFinishedEvent);

                AudioManager.Crossfade(AudioClips.BgHoneyBombs, AudioClips.BgNormal);
            });
        }


        private void OnDisable() {
            _beeSpriteRenderer.transform.localPosition = _initialBeeLocalPosition;
            _beeSpriteRenderer.transform.localScale = _initialBeeLocalScale;
        }
    }
}
