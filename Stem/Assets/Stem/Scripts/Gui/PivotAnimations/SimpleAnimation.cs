using Assets.Stem.Scripts.Extensions;
using Assets.Stem.Scripts.Utils;
using UnityEngine;


namespace Assets.Stem.Scripts.Gui.PivotAnimations {
    public class SimpleAnimation : PivotAnimation {

        [SerializeField] private AnimationClip _animationClip;

  
        private void OnEnable() {
            Length = _animationClip.length;
            this.InvokeAfterTime(Length, RaiseFinishedEvent);
        }


    }
}
