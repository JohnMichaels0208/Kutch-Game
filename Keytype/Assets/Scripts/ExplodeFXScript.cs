using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeFXScript : MonoBehaviour
{
    public void OnFXAnimationEnd()
    {
        Destroy(transform.parent.gameObject);
    }
}
