using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.XR;

public class ControllerButtonMapper : MonoBehaviour
{
    public PostProcessVolume postProcessVolume;
    private ColorGrading colorGrading;
    private Vignette vignette;
    private bool nightVisionOn = false;
    
    // Button mappings (map controller buttons to actions)
    private Dictionary<string, InputFeatureUsage<bool>> buttonMappings = new Dictionary<string, InputFeatureUsage<bool>>();
    
    private InputDevice rightController;  // Right controller
    private InputDevice leftController;   // Left controller

    void Start()
    {
        // Get the PostProcessing effects using GetSetting() for the PostProcessProfile
        colorGrading = postProcessVolume.profile.GetSetting<ColorGrading>();
        vignette = postProcessVolume.profile.GetSetting<Vignette>();

        // Default to night vision off
        SetNightVision(false);

        // Initialize VR controllers
        InitializeControllers();

        // Set up button mappings
        SetupButtonMappings();
    }

    // Initialize controllers
    void InitializeControllers()
    {
        List<InputDevice> devices = new List<InputDevice>();
        
        // Get the right controller
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, devices); 
        if (devices.Count > 0)
        {
            rightController = devices[0];
        }

        // Get the left controller
        devices.Clear();
        InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, devices); 
        if (devices.Count > 0)
        {
            leftController = devices[0];
        }
    }

    // Setup button mappings
    void SetupButtonMappings()
    {
        // Map actions to button presses
        buttonMappings["NightVision"] = CommonUsages.primaryButton; // Example: "primaryButton" for NightVision toggle
    }

    void Update()
    {
        // Check button presses for each controller
        CheckButtonPress(rightController);
        CheckButtonPress(leftController);
    }

    void CheckButtonPress(InputDevice controller)
    {
        // Check if the controller is valid
        if (controller.isValid)
        {
            // Loop through all button mappings to check input
            foreach (var buttonMapping in buttonMappings)
            {
                bool buttonPressed = false;
                
                if (controller.TryGetFeatureValue(buttonMapping.Value, out buttonPressed) && buttonPressed)
                {
                    // Trigger the mapped action (toggle night vision in this case)
                    if (buttonMapping.Key == "NightVision")
                    {
                        ToggleNightVision();
                    }
                }
            }
        }
    }

    void ToggleNightVision()
    {
        nightVisionOn = !nightVisionOn;
        SetNightVision(nightVisionOn);
    }

    void SetNightVision(bool enabled)
    {
        if (enabled)
        {
            // Enable the post-processing effects for night vision
            colorGrading.enabled.value = true;
            vignette.enabled.value = true;

            // Set specific values for night vision effect
            colorGrading.saturation.value = -100;  // Change to greenish tint
            vignette.intensity.value = 0.5f;       // Adjust intensity for dark edges
        }
        else
        {
            // Disable the post-processing effects for night vision
            colorGrading.enabled.value = false;
            vignette.enabled.value = false;
        }
    }
}
