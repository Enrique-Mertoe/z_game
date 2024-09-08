using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManger : MonoBehaviour
{
    public static InteractionManger Instance { get; set; }

    public void Awake()
    {
        if (!Instance && Instance != this)
        {
            Destroy(gameObject);
        }
        else Instance = this;
    }
}
