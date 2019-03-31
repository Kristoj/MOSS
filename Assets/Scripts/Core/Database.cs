using System.Collections.Generic;
using UnityEngine;

public class Database : MonoBehaviour {
    
    [SerializeField] private Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();


    private static Database _instance;
    public static Database Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType(typeof(Database)) as Database;
            }
            return _instance;
        }
        set {
            _instance = value;
        }
    }


    void Awake() {
        /* !!! This stuff should be moved somewhere else !!! */
        SoundSystem.Awake();
    }

    public void AssingAudioClips(Object[] objs) {
        audioClips = new Dictionary<string, AudioClip>();
        int counter = 0;
        for (int i = 0; i < objs.Length; i++)  {
            Debug.Log("Sound name: " + objs[i].name);
            audioClips.Add(objs[i].name, (objs[i] as AudioClip));
            counter++;
        }
        Debug.Log("Succesfully assigned: " + counter + " sound files.");
    }

    public AudioClip GetAudioClip(string soundName) {
        AudioClip clip;
        audioClips.TryGetValue(soundName, out clip);
        return clip;
    }

}
