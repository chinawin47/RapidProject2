using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ontrigger : MonoBehaviour
{
    // Start is called before the first frame update
    private void OntriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(this.transform);
        }
    }
    private void OntriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
