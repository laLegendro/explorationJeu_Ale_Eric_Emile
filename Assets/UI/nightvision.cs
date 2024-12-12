using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.XR;

public class NightVisionToggle : MonoBehaviour
{
    public PostProcessVolume postProcessVolume; // Reference to the PostProcessVolume
    public bool isNightVisionOn = false; // Toggle state for Night Vision

    private ColorGrading colorGrading;
    private Grain grain;
    private Vignette vignette;
    
    private XRNode inputSource = XRNode.RightHand; // Set input source (RightHand by default)

    void Start()
    {
        // Get the Color Grading effect from the Post Process Profile
        if (postProcessVolume.profile.TryGetSettings(out colorGrading))
        {
            colorGrading.enabled.value = isNightVisionOn;
        }

        // Get the Grain effect
        if (postProcessVolume.profile.TryGetSettings(out grain))
        {
            grain.enabled.value = isNightVisionOn;
        }

        // Get the Vignette effect
        if (postProcessVolume.profile.TryGetSettings(out vignette))
        {
            vignette.enabled.value = isNightVisionOn;
        }
    }

    void Update()
    {
        // Detect VR controller input to toggle night vision
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);

        // Example using the "primaryButton" on the controller (replace this with your preferred button)
        bool buttonPressed;
        if (device.TryGetFeatureValue(CommonUsages.primaryButton, out buttonPressed) && buttonPressed)
        {
            ToggleNightVision();
        }
    }

    void ToggleNightVision()
    {
        isNightVisionOn = !isNightVisionOn;

        // Toggle post-processing effects
        if (colorGrading != null)
        {
            colorGrading.enabled.value = isNightVisionOn;

            // Modify Color Grading for Night Vision (green tint, no saturation)
            if (isNightVisionOn)
            {
                colorGrading.saturation.value = -100; // Remove saturation (black and white)
                colorGrading.hueShift.value = 0.5f; // Greenish hue (night vision)
            }
            else
            {
                colorGrading.saturation.value = 0; // Reset saturation
                colorGrading.hueShift.value = 0f; // Reset hue shift to normal
            }
        }

        if (grain != null)
        {
            grain.enabled.value = isNightVisionOn;

            // Add grain effect for night vision
            if (isNightVisionOn)
            {
                grain.intensity.value = 0.5f; // Add grain
            }
            else
            {
                grain.intensity.value = 0f; // Remove grain
            }
        }

        if (vignette != null)
        {
            vignette.enabled.value = isNightVisionOn;

            // Apply vignette effect for night vision
            if (isNightVisionOn)
            {
                vignette.intensity.value = 0.4f; // Darken edges
                vignette.smoothness.value = 0.4f; // Adjust smoothness of vignette
            }
            else
            {
                vignette.intensity.value = 0f; // Remove vignette
                vignette.smoothness.value = 1f; // Reset smoothness
            }
        }
    }
}
