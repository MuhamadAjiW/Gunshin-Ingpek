using System;
using System.Collections;
using UnityEngine;

public abstract class Collectible : WorldObject 
{
    // Attributes
    [SerializeField] float TimeToLive;

    // Events
    private event Action OnCollectEvent;
    private event Action OnTimeoutEvent;

    // Constructor
    protected void Start()
    {
        OnCollectEvent += OnCollect;
        OnCollectEvent += OnCollectEnd;
        OnTimeoutEvent += OnTimeout;
        OnTimeoutEvent += OnTimeoutEnd;
        StartCoroutine(Timeout());
    }

    // Functions
    protected void OnTriggerEnter(Collider otherCollider)
    {
        OnCollectEvent?.Invoke();
    }

    protected virtual void OnCollectEnd()
    {
        Destroy(gameObject);
    }

    protected IEnumerator Timeout()
    {
        yield return new WaitForSeconds(TimeToLive);
        OnTimeoutEvent?.Invoke();
    }

    protected void OnTimeoutEnd()
    {
        Destroy(gameObject);
    }

    public void AddOnTimeout(Action onTimeout)
    {
        OnTimeoutEvent -= OnTimeoutEnd;
        OnTimeoutEvent += onTimeout;
        OnTimeoutEvent += OnTimeoutEnd;
    }

    public void RemoveOnTimeout(Action onTimeout)
    {
        OnTimeoutEvent -= OnTimeoutEnd;
        OnTimeoutEvent -= onTimeout;
        OnTimeoutEvent += OnTimeoutEnd;
    }

    // Abstract Functions
    protected abstract void OnTimeout();
    protected abstract void OnCollect();
}