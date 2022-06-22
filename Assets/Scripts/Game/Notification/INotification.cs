using UnityEngine;
using UnityEngine.Events;

namespace Game.Notification
{
    public interface INotification
    {
        UnityEvent OnShowNotificationEndEvent { get; }
        void Show();
    }

    public abstract class Notification : MonoBehaviour, INotification
    {
        public UnityEvent OnShowNotificationEndEvent { get; } = new();

        public abstract void Show();
        
        public void EndNotification()
        {
            OnShowNotificationEndEvent.Invoke();
        }
    }
}