using UnityEngine;
using UnityEngine.Events;

namespace Game.Object
{
    public class Emody : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private string cryTrigger = "Cry";
        [SerializeField] private string eatTrigger = "Eat";
        [SerializeField] private string throwTrigger = "Throw";
        [SerializeField] private string cryLoopTrigger = "CryLoop";
        [SerializeField] private string kneadTrigger = "Knead";
        public UnityEvent OnAnimationEnd { get; } = new();
        
        public void SetEatAnimation()
        {
            animator.SetTrigger(eatTrigger);
        }

        public void SetThrowAnimation()
        {
            animator.SetTrigger(throwTrigger);
        }

        public void SetCryAnimation()
        {
            animator.SetTrigger(cryTrigger);
        }

        public void SetCryLoopAnimation()
        {
            animator.SetTrigger(cryLoopTrigger);
        }

        public void ResetToKneadAnimation()
        {
            animator.SetTrigger(kneadTrigger);
        }
        
        public void NotifyAnimationEnd()
        {
            OnAnimationEnd.Invoke();
        }
        
    }
}