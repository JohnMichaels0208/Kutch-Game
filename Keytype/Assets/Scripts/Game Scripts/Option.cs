using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option : MonoBehaviour
{
    public float volume;

    public void SaveOption()
    {
        SaveSystemScript.SaveOption(this);
    }

    public void LoadOption()
    {

    }
}
