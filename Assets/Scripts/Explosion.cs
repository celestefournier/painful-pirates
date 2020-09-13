using System.Collections;
using UnityEngine;

public class Explosion : StateMachineBehaviour {
  public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    Destroy(animator.gameObject, stateInfo.length);
  }
}
