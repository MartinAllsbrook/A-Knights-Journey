using System;
using JetBrains.Annotations;
using UnityEngine;

class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    private @InputActions controls;

    // Archery
    public static event Action<float> Archery_OnMove = delegate {};
    public static event Action Archery_OnFire = delegate {};

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        controls = new @InputActions();

        LinkEvents();
    }

    void OnEnable() 
    {
        controls.Enable();
    }

    void OnDisable() 
    {
        controls.Disable();
    }

    void LinkEvents()
    {
        controls.Archery.Move.performed += ctx => Archery_OnMove.Invoke(ctx.ReadValue<float>());
        controls.Archery.Move.canceled += ctx => Archery_OnMove.Invoke(0f);

        controls.Archery.Fire.performed += ctx => Archery_OnFire.Invoke();
    }
}