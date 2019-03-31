using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AutomaticAssignerScript))]
public class AutomaticAssignerEditor : Editor {
    
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        AutomaticAssignerScript assigner = target as AutomaticAssignerScript;
        if (GUILayout.Button("Assign Files")) {
            AutomaticAssignerScript.AssignAudioClips();
        }
    }

}
