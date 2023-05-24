using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoCustom : MonoBehaviour
{
    public static MonoCustom Instance;
    public Dictionary<string, Coroutine> _repeatingBase = new Dictionary<string, Coroutine>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void StartCoroutineWithArg<T>(Action<T> currentAction, float timeToActionDo, T arg) => StartCoroutine(CustomInvoke(currentAction, timeToActionDo, arg));
    public void StartCoroutineWithOUTArg(Action currentAction, float timeToActionDo) => StartCoroutine(CustomInvoke(currentAction, timeToActionDo));
    public void StartRepeatingCoroutineWithOUTArg(Action currentAction, float timeToActionDo, string repeatingNameMethod) { if (!_repeatingBase.ContainsKey(repeatingNameMethod)) { _repeatingBase.Add(repeatingNameMethod, StartCoroutine(CustomInvokeRepeating(currentAction, timeToActionDo))); } }

    public void StopRepeatingCoroutineWithOUTArg(string repeatingNameMethod) { if (_repeatingBase.ContainsKey(repeatingNameMethod)) { StopCoroutine(_repeatingBase[repeatingNameMethod]); _repeatingBase.Remove(repeatingNameMethod); } }

    private IEnumerator CustomInvokeRepeating(Action currentAction, float timeToActionDo) // customReInvoke with actions 
    {
        while (true)
        {
            currentAction?.Invoke();
            yield return new WaitForSeconds(timeToActionDo);
        }
    }

    private IEnumerator CustomInvoke(Action currentAction, float timeToActionDo) // customInvoke with actions 
    {
        yield return new WaitForSeconds(timeToActionDo);
        currentAction?.Invoke();
    }

    private IEnumerator CustomInvoke<T>(Action<T> currentAction, float timeToActionDo, T arg) // customInvoke with actions 
    {
        yield return new WaitForSeconds(timeToActionDo);
        currentAction?.Invoke(arg);
    }
}
