using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.XR;

public class nightvision : MonoBehaviour

{
    public PostProcessVolume postProcessVolume;
    private ColorGrading colorGrading;
    private Vignette vignette;
    private bool nightVisionOn = false;

    void Start()
    {
        // Get the PostProcessing effects using GetSetting() for the PostProcessProfile
        colorGrading = postProcessVolume.profile.GetSetting<ColorGrading>();
        vignette = postProcessVolume.profile.GetSetting<Vignette>();

        // Default to night vision off
        SetNightVision(false);
    }

    void Update()
    {
        // Check for VR controller input (e.g., trigger button)
        if (Input.GetButtonDown("Fire1")) // Replace "Fire1" with your VR button mapping
        {
            nightVisionOn = !nightVisionOn;
            SetNightVision(nightVisionOn);
        }
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