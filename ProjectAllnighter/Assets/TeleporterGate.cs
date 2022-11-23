using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterGate : MonoBehaviour
{
    public GameObject Destination;
    public PlayerData player;

    public void Teleport()
    {
        Vector3 d = Destination.transform.position;
        player.Teleport(new Vector3(d.x, d.y-0.2f, d.z));
    }
}
