using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UserInput : MonoBehaviour
{
    public static UserInput instance{get; private set;}
    public Vector2 MoveInput{get; private set;}
    public bool BasicAttackInput{get; private set;}
    public bool HeavyAttackInput{get; private set;}
    public bool ChargeAttackInput{get; private set;}
    public bool DashInput{get; private set;}
    public bool PauseGameInput{get; private set;}

    private PlayerInput pi;
    private InputAction ma, baa, haa, caa, da, pa;

    private void Awake()
    {
        if(instance == null){instance = this;}

        pi = GetComponent<PlayerInput>();
        SetUpInputActions();
    }
    private void Update()
    {
        UpdateInputs();
    }
    private void SetUpInputActions()
    {
        ma = pi.actions["Move"];
        baa = pi.actions["BasicAttack"];
        haa = pi.actions["heavyAttack"];
        caa = pi.actions["ChargeAttack"];
        da = pi.actions["Dash"];
        pa = pi.actions["PauseGame"];
    }
    private void UpdateInputs()
    {
        MoveInput = ma.ReadValue<Vector2>();
        BasicAttackInput = baa.IsPressed();
        HeavyAttackInput = haa.WasPressedThisFrame();
        ChargeAttackInput = caa.WasPressedThisFrame();
        DashInput = da.WasPressedThisFrame();
        PauseGameInput = pa.WasPressedThisFrame();
    }
}
