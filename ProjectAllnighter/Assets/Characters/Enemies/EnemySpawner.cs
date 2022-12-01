using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    private List<Enemy> enemies = new List<Enemy>();

    // Start is called before the first frame update
    void Start()
    {
        enemies.AddRange(GetComponentsInChildren<Enemy>());
    }

    public void ResetEnemies()
    {
        foreach (Enemy e in enemies)
        {
            e.ResetPosition();
        }
    }

    public void RespawnEnemies()
    {
        foreach (Enemy e in enemies)
        {
            e.Respawn();
        }
    }
}
