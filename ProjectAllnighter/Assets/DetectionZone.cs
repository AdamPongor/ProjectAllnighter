using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    public string tagTarget = "Player";
    public List<Collider2D> detectedObjs = new List<Collider2D>();
    public Collider2D dCollider;
    // Start is called before the first frame update
    void Start()
    {
        dCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == tagTarget)
            detectedObjs.Add(collision);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == tagTarget)
            detectedObjs.Remove(other);
    }
}
