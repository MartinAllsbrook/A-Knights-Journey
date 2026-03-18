using System;
using UnityEngine;

class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    private @InputActions controls;

    // Village
    public static event Action<Vector2> Village_OnMove = delegate {};

    // Archery
    public static event Action<float> Archery_OnMove = delegate {};
    public static event Action Archery_OnFire = delegate {};

    // Swordplay
    public static event Action Swordplay_Attack = delegate {};
    public static event Action<Vector2> Swordplay_OnMove = delegate {};

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
        // Village
        controls.Village.Move.performed += ctx => Village_OnMove.Invoke(ctx.ReadValue<Vector2>());
        controls.Village.Move.canceled += ctx => Village_OnMove.Invoke(Vector2.zero);

        // Archery
        controls.Archery.Move.performed += ctx => Archery_OnMove.Invoke(ctx.ReadValue<float>());
        controls.Archery.Move.canceled += ctx => Archery_OnMove.Invoke(0f);

        controls.Archery.Fire.performed += ctx => Archery_OnFire.Invoke();

        // Swordplay
        controls.Swordplay.Attack.performed += ctx => Swordplay_Attack.Invoke();

        controls.Swordplay.Move.performed += ctx => Swordplay_OnMove.Invoke(ctx.ReadValue<Vector2>());
        controls.Swordplay.Move.canceled += ctx => Swordplay_OnMove.Invoke(Vector2.zero);

        // Riding
        controls.Riding.Move.performed += ctx => Riding_OnMove.Invoke(ctx.ReadValue<Vector2>());
        controls.Riding.Move.canceled += ctx => Riding_OnMove.Invoke(Vector2.zero);
    }
}