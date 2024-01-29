using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ResetDeviceFindings : MonoBehaviour
{
    [SerializeField] private InputActionAsset _inputactions;
    [SerializeField] private string _targetControlScheme;

    public void ResetAllBindings()
    {
        foreach (InputActionMap map in _inputactions.actionMaps)
        {
            map.RemoveAllBindingOverrides();
        }
    }

    public void ResetControlSchemeBinding()
    {
        foreach (InputActionMap map in _inputactions.actionMaps)
        {
            foreach (InputAction action in map.actions)
            {
                action.RemoveBindingOverride(InputBinding.MaskByGroup(_targetControlScheme));
            }
        }
    }
}
