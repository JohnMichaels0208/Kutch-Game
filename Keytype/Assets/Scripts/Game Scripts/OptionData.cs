using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OptionData : MonoBehaviour
{
    public float volume;

    public OptionData(Option options)
    {
        this.volume = options.volume;
    }
}
