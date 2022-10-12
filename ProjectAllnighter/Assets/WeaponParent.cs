using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    public Vector2 Direction { get; set; }

    private void Update() {
        transform.right = (Direction - (Vector2)transform.position).normalized;
    }
}
