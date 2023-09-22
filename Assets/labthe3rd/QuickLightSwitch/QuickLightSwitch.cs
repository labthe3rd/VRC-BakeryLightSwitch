
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class QuickLightSwitch : MonoBehaviour
{
    //private Light[] targetLights;
    [Header("Lights you want to exclude from turning off, leave empty if you have none")]
    public Light[] excludedLights;

    private bool ignoreLight;
    public void RealTimeLightsOn()
    {
        Light[] lights = (Light[])GameObject.FindObjectsOfType(typeof(Light));

        foreach (Light light in lights)
        {
            if (excludedLights.Length > 0)
            {
                for (int i = 0; i < excludedLights.Length; i++)
                {
                    if (excludedLights[i] == light)
                    {
                        ignoreLight = true;
                        break;
                    }
                }
                if (!ignoreLight)
                {
                    light.enabled = true;
                }
            }
            else
            {
                light.enabled = false;
            }
            ignoreLight = false;
        }

    }

    public void RealTimeLightsOff()
    {
        Light[] lights = (Light[])GameObject.FindObjectsOfType(typeof(Light));

        foreach (Light light in lights)
        {


            if (excludedLights.Length > 0)
            {
                for (int i = 0; i < excludedLights.Length; i++)
                {
                    if (excludedLights[i] == light)
                    {
                        ignoreLight = true;
                        break;
                    }
                }
                if (!ignoreLight)
                {
                    light.enabled = false;
                }
            }
            else
            {
                light.enabled = false;
            }
            ignoreLight = false;


        }
    }

}

[CustomEditor(typeof(QuickLightSwitch))]
public class QuickLightSwitchEditor : Editor
{

    public override void OnInspectorGUI()
    {
        // Draws the default convert to UdonBehaviour button, program asset field, sync settings, etc.
        DrawDefaultInspector();

        QuickLightSwitch inspectorBehavior = (QuickLightSwitch)target;
        EditorApplication.QueuePlayerLoopUpdate(); //force the editor to update so you can see in real time the changes you make to the objects
        EditorGUI.BeginChangeCheck();

        if (GUILayout.Button("Set All Realtime Lights To Active"))
        {
            inspectorBehavior.RealTimeLightsOn();
        }

        if (GUILayout.Button("Set All Realtime Lights To Inactive"))
        {
            inspectorBehavior.RealTimeLightsOff();
        }

    }

}

#endif