using Assets.GGJ2015.Scripts.Extensions;
using Assets.GGJ2015.Scripts.Utils;
using UnityEngine;


namespace Assets.GGJ2015.Scripts.Gui.PivotAnimations {
    public class SimpleAnimation : PivotAnimation {

        [SerializeField] private AnimationClip _animationClip;

  
        private void OnEnable() {
            Length = _animationClip.length;
            this.InvokeAfterTime(Length, RaiseFinishedEvent);
        }


    }
}
