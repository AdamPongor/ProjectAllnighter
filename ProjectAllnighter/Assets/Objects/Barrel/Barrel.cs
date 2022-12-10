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
        Vector2 Dir = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
        ItemWorld.DropItem(gameObject.transform.position, item, Dir, 0);
        gameObject.SetActive(false);
    }
}
