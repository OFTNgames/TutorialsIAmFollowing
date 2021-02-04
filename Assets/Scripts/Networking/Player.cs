using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnHolaCountChange))]
    int holaCount = 0;
   private void HandleMovement()
   {
        if (isLocalPlayer) 
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(moveHorizontal * 0.1f, moveVertical * 0.1f, 0);
            transform.position = transform.position + movement;
        }
   }

    private void Update()
    {
        HandleMovement();
        if (isLocalPlayer && Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("Sending Hola to server");
            Hola();
        }
    }

    public override void OnStartServer()
    {
        Debug.Log("player has spawned on server");
    }

    [Command]
    private void Hola()
    {
        Debug.Log("Recieved Hola from the Client");
        holaCount += 1;
        ReplyHola();
    }
    [TargetRpc]
    private void ReplyHola()
    {
        Debug.Log("Recieved hola from Server");
    }

    [ClientRpc]
    private void TooHigh()
    {
        Debug.Log("Too High");
    }

    private void OnHolaCountChange(int oldCount, int newCount)
    {
        Debug.Log($"we had {oldCount} holas, now we have {newCount} holas");
    }
}
