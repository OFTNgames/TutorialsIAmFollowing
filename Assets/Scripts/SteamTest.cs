using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamTest : MonoBehaviour
{
    void Start()
    {
        if (!SteamManager.Initialized) { return; }

        string name = Steamworks.SteamFriends.GetPersonaName();
        Debug.Log(name);
    }
}
