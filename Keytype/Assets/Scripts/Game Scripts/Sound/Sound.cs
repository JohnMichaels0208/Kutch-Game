using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "Sound", fileName = "New Sound", order = 1)]
public class Sound : ScriptableObject
{
    public AudioClip clip;

    public AudioMixerGroup group;
}
