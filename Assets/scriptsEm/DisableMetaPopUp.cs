using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DisableMetaPopUp : MonoBehaviour
{
    private const string TelemetryEnabledKey = "OVRTelemetry.TelemetryEnabled";

    [ContextMenu("DisablePopup")]
    public void DisablePopup()
    {
        EditorPrefs.SetBool(TelemetryEnabledKey, false);
    }
}
