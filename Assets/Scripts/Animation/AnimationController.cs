using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour
{
    private Animator _animator;
    private List<string> _stateNames;
    private string _currentState;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _stateNames = new List<string>();
        _currentState = "";

        findAnimationNames();
    }

    private void findAnimationNames()
    {
        var controller = _animator.runtimeAnimatorController;
        for (int i = 0; i < controller.animationClips.Length; i++)
        {
            _stateNames.Add(controller.animationClips[i].name);
        }
    }

    public void SetState(string stateName)
    {
        if(!_stateNames.Contains(stateName))
        {
            return;
        }
        foreach(var s in _stateNames)
        {
            _animator.ResetTrigger(s);
        }
        _animator.SetTrigger(stateName);
        _currentState = stateName;
    }

    public void SetFlag(string flagName, bool value)
    {
        _animator.SetBool(flagName, value);
        _currentState = flagName;
    }

    public string GetCurrentState()
    {
        return _currentState;
    }
}
