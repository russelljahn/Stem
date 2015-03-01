using System.Security.Cryptography;
using Assets.Stem.Scripts.Audio;
using Assets.Stem.Scripts.Extensions;
using Assets.Stem.Scripts.Utils;
using UnityEngine;


namespace Assets.Stem.Scripts.Gui.PivotAnimations {
    public class GetBusyAnimation : PivotAnimation {

        [SerializeField] private float _shortenFlytrapWaggleAnimationTime = .5f;
        [SerializeField] private float _shortenHeartsAnimationTime = .15f;
        [SerializeField] private float _fadeInGroundTime = 0.5f;
        [SerializeField] private float _bgNormalFadeTime = 4f;

        [SerializeField] private float _storkFlyTime = 2.0f;
        [SerializeField] private float _seedStartFallingAfterStorkSpawnTime = 0.95f;
        [SerializeField] private float _seedFallTime = 0.75f;
        [SerializeField] private float _bagUnfoldingTime = 0.2f;
        [SerializeField] private float _bagBlowingTime = 2.0f;

        [SerializeField] private float _seedIncubationTime = 1.0f;


        [SerializeField] private AnimationCurve _fadeInGroundEasing = AnimationCurveUtils.GetLinearCurve();
        [SerializeField] private AnimationCurve _storkFlyEasing = AnimationCurveUtils.GetLinearCurve();
        [SerializeField] private AnimationCurve _seedFallEasing = AnimationCurveUtils.GetLinearCurve();
        [SerializeField] private AnimationCurve _bagBlowingEasing = AnimationCurveUtils.GetLinearCurve();

        [SerializeField] private float _babyBagUnfoldingSpeed = 0.5f;

        [SerializeField] private SpriteRenderer _flytrapWaggleSpriteRenderer;
        [SerializeField] private SpriteRenderer _heartsSpriteRenderer;
        [SerializeField] private SpriteRenderer _growFlowerSpriteRenderer;
        [SerializeField] private SpriteRenderer _seedSpriteRenderer;
        [SerializeField] private SpriteRenderer _babyBagIdleSpriteRenderer;
        [SerializeField] private SpriteRenderer _babyBagUnfoldingSpriteRenderer;
        [SerializeField] private SpriteRenderer _babyBagBlowingSpriteRenderer;


        [SerializeField] private Transform _stork;
        [SerializeField] private Transform _seed;

        [SerializeField] private Vector3 _storkSpawnPositionViewportCoord = new Vector3(1.25f, 0.5f, 1f);
        [SerializeField] private Vector3 _storkFlyPositionViewportCoord = new Vector3(-0.25f, 0.5f, 1f);
        [SerializeField] private Vector3 _seedFallPositionRelativeToFlower = new Vector3(0f, -4f, 0f);
        [SerializeField] private Vector3 _seedFallRotation = new Vector3(0, 0f, 45f);

        private Vector3 _babyBagIdleInitialLocalPosition;
        private Vector3 _babyBagUnfoldingInitialLocalPosition;
        private Vector3 _babyBagBlowingInitialLocalPosition;
        private Vector3 _seedInitialLocalPosition;

        [SerializeField] private Animator _heartsAnimator;

        [SerializeField] private Animator _growFlowerAnimator;
        [SerializeField] private Animator _babyBagUnfoldingAnimator;

        [SerializeField] private AnimationClip _flytrapWaggleClip;
        [SerializeField] private AnimationClip _heartsClip;
        [SerializeField] private AnimationClip _growFlowerAnimationClip;


        private void OnEnable() {
            _babyBagUnfoldingSpriteRenderer.enabled = false;
            _babyBagBlowingSpriteRenderer.enabled = false;
            
            _seedSpriteRenderer.color = Color.clear;
            _flytrapWaggleSpriteRenderer.color = Color.white;
            _heartsSpriteRenderer.color = new Color(1f, 1f, 1f, 0f);

            _babyBagUnfoldingAnimator.speed = _babyBagUnfoldingSpeed;
            _growFlowerAnimator.speed = 0f;
            _heartsAnimator.speed = 0f;

            _babyBagIdleInitialLocalPosition = _babyBagIdleSpriteRenderer.transform.localPosition;
            _babyBagUnfoldingInitialLocalPosition = _babyBagUnfoldingSpriteRenderer.transform.localPosition;
            _babyBagBlowingInitialLocalPosition = _babyBagBlowingSpriteRenderer.transform.localPosition;
            _seedInitialLocalPosition = _seed.localPosition;

            _stork.transform.position = Camera.main.ViewportToWorldPoint(_storkSpawnPositionViewportCoord);
            _stork.gameObject.SetActive(false);

            Length = _flytrapWaggleClip.length + _heartsClip.length + 3f * _fadeInGroundTime - _shortenFlytrapWaggleAnimationTime + 
                _growFlowerAnimationClip.length + _seedFallTime + _seedIncubationTime;


            //AudioManager.LoadClip(AudioClips.BgGetBusy);
            //AudioManager.PlayTrack(AudioClips.BgGetBusy);
            //AudioManager.Crossfade(AudioClips.BgNormal, AudioClips.BgGetBusy);
            //this.InvokeAfterTime(_bgNormalFadeTime, () => AudioManager.Crossfade(AudioClips.BgGetBusy, AudioClips.BgNormal));

            this.InvokeAfterTime(_flytrapWaggleClip.length - _shortenFlytrapWaggleAnimationTime, FadeInHearts);
        }


        private void FadeInHearts() {
            TweenUtils.TweenAlpha(_heartsSpriteRenderer, 1f, _fadeInGroundTime, _fadeInGroundEasing);
            _heartsAnimator.speed = 1f;
            this.InvokeAfterTime(_heartsClip.length - _shortenHeartsAnimationTime, FadeOutHearts);
        }


        private void FadeOutHearts() {
            _flytrapWaggleSpriteRenderer.color = new Color(1f, 1f, 1f, 0f);
            TweenUtils.TweenAlpha(_heartsSpriteRenderer, 0f, _fadeInGroundTime, _fadeInGroundEasing, FadeInGround);
        }


        private void FadeInGround() {
            TweenUtils.TweenAlpha(_growFlowerSpriteRenderer, 1f, _fadeInGroundTime, _fadeInGroundEasing, PlayStorkAnimation);
        }


        private void PlayStorkAnimation() {
            _stork.gameObject.SetActive(true);

            TweenUtils.TweenWorldPosition(_stork, Camera.main.ViewportToWorldPoint(_storkFlyPositionViewportCoord), _storkFlyTime, _storkFlyEasing);
            this.InvokeAfterTime(_seedStartFallingAfterStorkSpawnTime, PlaySeedFallAnimation);
        }


        private void PlaySeedFallAnimation() {          
            TweenUtils.TweenColor(_seedSpriteRenderer, Color.white, _bagUnfoldingTime);
            _babyBagIdleSpriteRenderer.enabled = false;
            _babyBagUnfoldingSpriteRenderer.enabled = true;

            this.InvokeAfterTime(_bagUnfoldingTime, () => {
                _babyBagUnfoldingSpriteRenderer.enabled = false;
                _babyBagBlowingSpriteRenderer.enabled = true;
                TweenUtils.TweenWorldPosition(_babyBagIdleSpriteRenderer.transform, Camera.main.ViewportToWorldPoint(_storkSpawnPositionViewportCoord), _bagBlowingTime, _bagBlowingEasing);

                _seed.SetParent(_growFlowerSpriteRenderer.transform, true);
                TweenUtils.TweenLocalPosition(_seed, _seedFallPositionRelativeToFlower, _seedFallTime, _seedFallEasing);
                TweenUtils.TweenLocalRotation(_seed, _seedFallRotation, _seedFallTime, _seedFallEasing);

                this.InvokeAfterTime(_seedFallTime + _seedIncubationTime, PlayFlowerGrowAnimation);
            });
        }


        private void PlayFlowerGrowAnimation() {
            _growFlowerAnimator.speed = 1f;
            this.InvokeAfterTime(_growFlowerAnimationClip.length, Cleanup);
        }



        private void Cleanup() {
            _stork.gameObject.SetActive(false);

            _babyBagIdleSpriteRenderer.enabled = true;
            _babyBagUnfoldingSpriteRenderer.enabled = false;
            _babyBagBlowingSpriteRenderer.enabled = false;

            _seed.SetParent(_babyBagIdleSpriteRenderer.transform, true);

            _babyBagIdleSpriteRenderer.transform.localPosition = _babyBagIdleInitialLocalPosition;
            _babyBagUnfoldingSpriteRenderer.transform.localPosition = _babyBagUnfoldingInitialLocalPosition;
            _babyBagBlowingSpriteRenderer.transform.localPosition = _babyBagBlowingInitialLocalPosition;
            _seed.localPosition = _seedInitialLocalPosition;


            RaiseFinishedEvent();
        }
    }
}
