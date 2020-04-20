using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBorder : MonoBehaviour
{

    void Start()
    {
        Vector3 point = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
        
        Vector2[] points = new Vector2[]
        {
            Camera.main.ViewportToWorldPoint(new Vector2(0, 1)) + Vector3.up * 100f + Vector3.left * 2f,
            Camera.main.ViewportToWorldPoint(new Vector2(0, 0)) + Vector3.down * 2f + Vector3.left * 2f,
            Camera.main.ViewportToWorldPoint(new Vector2(1, 0)) + Vector3.down * 2f + Vector3.right * 2f,
            Camera.main.ViewportToWorldPoint(new Vector2(1, 1)) + Vector3.up * 100f + Vector3.right * 2f
        };

        GetComponent<EdgeCollider2D>().points = points;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.attachedRigidbody.CompareTag("Balloon"))
            GameManager.instance.GameOver();
    }

}
