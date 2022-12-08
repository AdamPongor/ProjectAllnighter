using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Barrel : MonoBehaviour
{

    public void Destroy()
    {
        Random rnd = new Random();
        Item item = new Coin(rnd.Next(1,20));
        Vector2 Dir = UtilsClass.GetRandomDir();
        ItemWorld.DropItem(gameObject.transform.position, item, Dir, 0);
        gameObject.SetActive(false);
    }
}
