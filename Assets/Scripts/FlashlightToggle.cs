using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlashlightToggle : MonoBehaviour, PlayerInputActions.IPlayerActions
{
    public FlashlightFlicker flickerController;
    public Light flashlight; // Direct reference to Light

    private PlayerInputActions inputActions;
    private bool flashlightOn = false;
    private bool isFlickering = true;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.Player.SetCallbacks(this);
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    public void OnToggleFlashlight(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            flashlightOn = !flashlightOn;

            if (flashlightOn)
            {
                // Always start flickering when turning on
                isFlickering = true;
                flickerController.StartFlickering();
            }
            else
            {
                // Turn off light and stop flicker
                isFlickering = false;
                flickerController.StopFlickering();
                flashlight.enabled = false;
            }
        }
    }

    private void Update()
    {
        if (Keyboard.current.gKey.wasPressedThisFrame && flashlightOn && isFlickering)
        {
            isFlickering = false;
            flickerController.StopFlickering();

            // Make sure light stays ON
            flashlight.enabled = true;
        }
    }
}
