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
        
        public void NotifyAnimationEnd()
        {
            OnAnimationEnd.Invoke();
        }
        
    }
}