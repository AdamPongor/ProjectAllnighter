using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    public Vector2 Direction { get; set; }
    public SpriteRenderer characterRenderer;
    public SpriteRenderer weaponRenderer;
    private void Update() {
        transform.right = Direction;

        if (Direction.y > 0)
        {
            weaponRenderer.sortingOrder = characterRenderer.sortingOrder - 1;
        }
        else
        {
            weaponRenderer.sortingOrder = characterRenderer.sortingOrder + 1;
        }

    }
}
