using System;
using UnityEngine;

class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    private @InputActions controls;

    // Archery
    public static event Action<float> Archery_OnMove = delegate {};
    public static event Action Archery_OnFire = delegate {};

    // Swordplay
    public static event Action Swordplay_AttackUp = delegate {};
    public static event Action Swordplay_AttackDown = delegate {};
    public static event Action Swordplay_AttackLeft = delegate {};
    public static event Action Swordplay_AttackRight = delegate {};

    // Riding
    public static event Action<Vector2> Riding_OnMove = delegate {};

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
        // Archery
        controls.Archery.Move.performed += ctx => Archery_OnMove.Invoke(ctx.ReadValue<float>());
        controls.Archery.Move.canceled += ctx => Archery_OnMove.Invoke(0f);

        controls.Archery.Fire.performed += ctx => Archery_OnFire.Invoke();

        // Swordplay
        controls.Swordplay.AttackUp.performed += ctx => Swordplay_AttackUp.Invoke();

        controls.Swordplay.AttackDown.performed += ctx => Swordplay_AttackDown.Invoke();

        controls.Swordplay.AttackLeft.performed += ctx => Swordplay_AttackLeft.Invoke();

        controls.Swordplay.AttackRight.performed += ctx => Swordplay_AttackRight.Invoke();


        // Riding
        controls.Riding.Move.performed += ctx => Riding_OnMove.Invoke(ctx.ReadValue<Vector2>());
        controls.Riding.Move.canceled += ctx => Riding_OnMove.Invoke(Vector2.zero);
    }
}