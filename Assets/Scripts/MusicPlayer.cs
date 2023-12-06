using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{

    [Obsolete]
    void Awake()
    {
        SetUpSingleton();
    }

    [Obsolete]
    private void SetUpSingleton()
    {
        //FindObjectsOfType(GetType()) == FindObjectsOfType<MusicPlayer>()
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

        if (FindObjectOfType(GetType()).name == "Mars")
        {
            Debug.Log(FindObjectOfType(GetType()).name);
        }
    }
}
