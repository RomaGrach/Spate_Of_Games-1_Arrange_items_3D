using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Sounds : MonoBehaviour
{
    public AudioClip[] sounds;
    public SoundArrays[] randSound;

    private AudioSource audioSrc => GetComponent<AudioSource>();

    public void PlaySound(int i, float volume = 1f, bool random = false, bool destroyed = false, float p1 = 0.85f, float p2 = 1.2f)
    {
        AudioClip clip = random ? randSound[i].soundArray[Random.Range(0, randSound[i].soundArray.Length)] : sounds[i];
        audioSrc.pitch = Random.Range(p1, p2);
        if (destroyed)
            AudioSource.PlayClipAtPoint(clip, transform.position);
        else
            audioSrc.PlayOneShot(clip, volume);
    }

    public void PlayS()
    {
        PlaySound(0, 0.6f, true);
    }

    [System.Serializable]
    public class SoundArrays
    {
        public AudioClip[] soundArray;
    }
}