using UnityEngine;

namespace Game.Notification
{
    public class CommonNotification : Notification
    {
        [SerializeField] private Animator animator;
        [SerializeField] private string showTriggerName = "Show"; 
        
        public override void Show()
        {
            animator.SetTrigger(showTriggerName);            
        }
    }
}