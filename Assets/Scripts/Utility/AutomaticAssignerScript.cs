using UnityEngine;

public class AutomaticAssignerScript : MonoBehaviour {
    
    public static void AssignAudioClips() {
        Object[] objs = Resources.LoadAll("Audio", typeof(AudioClip));
        Database.Instance.AssingAudioClips(objs);
    }

}
