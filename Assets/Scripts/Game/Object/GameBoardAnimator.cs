using UnityEngine;
using UnityEngine.Events;

namespace Game.Object
{
    public class GameBoardAnimator : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private string nextStageTrigger = "Next";

        public UnityEvent OnBoardReadyForInit { get; } = new();
        public UnityEvent OnNextStageAnimationEnd { get; } = new();

        public void SetNextStageAnimation()
        {
            animator.SetTrigger(nextStageTrigger);
        }
        
        public void NotifyBoardReadyForInit()
        {
            OnBoardReadyForInit.Invoke();
        }
        
        public void NotifyNextStageAnimationEnd()
        {
            OnNextStageAnimationEnd.Invoke();
        }

    }
}