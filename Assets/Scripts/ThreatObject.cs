using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreatObject : MonoBehaviour
{
    public float StartVelocity;
    public float StartAngularVelocity;

    new Rigidbody2D rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = transform.right * StartVelocity;
        rigidbody.angularVelocity = StartAngularVelocity;
    }

    private void FixedUpdate()
    {
        float angle = Mathf.Atan2(rigidbody.velocity.y, rigidbody.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Balloon"))
            GameManager.instance.GameOver();
        else if (collision.gameObject.CompareTag("Border"))
            ThreatManager.instance.DestroyThreatObject(gameObject);
    }

}
