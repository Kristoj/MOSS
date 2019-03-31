using System.Collections;
using UnityEngine;

public static class SoundSystem {

    private static GameObject sourceRoot;

    private static float masterVolume = .3f;
    private static float sfxVolume = .5f;

    public static void Awake() {
        AutomaticAssignerScript.AssignAudioClips();
        sourceRoot = new GameObject("Sound Source Root");
        sourceRoot.transform.SetParent(Database.Instance.transform);
    }

    public static void PlaySound(string soundName, Vector3 soundPosition) {
        PlaySoundClip(soundName, soundPosition);
    }

    public static void PlaySound2D(string soundName) {
        PlaySoundClip(soundName);
    }

    static void PlaySoundClip(string soundName, Vector3 soundPosition = default(Vector3)) {

        // Is the sound clip we want to play valid ?
        AudioClip clip = Database.Instance.GetAudioClip(soundName);
        if (clip == null) {
            Debug.Log("Could not find sound file named: " + soundName);
            return;
        }

        // Create new sound object and attach audio source to it
        GameObject soundObject = new GameObject("Sound Object");
        soundObject.transform.SetParent(sourceRoot.transform);
        AudioSource soundSource = soundObject.AddComponent<AudioSource>();

        // Change sound objects properties
        soundObject.transform.position = soundPosition;

        // Change audio source settings
        soundSource.clip = clip;
        soundSource.volume = masterVolume * sfxVolume;

        // Playe audio clip
        soundSource.Play();
        Database.Instance.StartCoroutine(DestroySoundObject(soundObject, clip.length));
    }

    static IEnumerator DestroySoundObject(GameObject soundObject, float delay){
        yield return new WaitForSeconds(delay);
        Object.Destroy(soundObject);
    }

}
