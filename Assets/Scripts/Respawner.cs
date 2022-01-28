using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    Vector3 respawnPosition;

    private void Awake() { respawnPosition = transform.position; }

    public void Respawn() { transform.position = respawnPosition; }
}
