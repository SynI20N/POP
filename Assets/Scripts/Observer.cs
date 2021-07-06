using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;

public abstract class Observer : MonoBehaviour
{
    public abstract void OnNotify(object value, NotificationType notificationType);
}

public abstract class Subject : MonoBehaviour
{
    private List<Observer> _observers = new List<Observer>();

    public void RegisterObserver(Observer observer)
    {
        _observers.Add(observer);
    }

    public void Notify(object value, NotificationType notificationType)
    {
        foreach (var observer in _observers)
            observer.OnNotify(value, notificationType);
    }
}