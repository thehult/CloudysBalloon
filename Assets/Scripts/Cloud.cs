using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{

    [Header("GFX")]
    public Transform Mouth;
    public Transform EyeLeft;
    public Transform EyeRight;
    public float EyeRadius;

    [Header("GamePlay")]
    public float MaxBlowLength;
    public float MaxBlowStrength;
    public float BlowTorque;

    float blowK;
    GameObject target;
    Rigidbody2D targetRB;

    private void Awake()
    {
        blowK = -MaxBlowStrength / MaxBlowLength;
    }

    private void Update()
    {
        if(GameManager.instance.IsPlaying())
        {
            GameObject balloon = GetClosestBalloon();

            if(target != balloon)
            {
                target = balloon;
                targetRB = balloon.GetComponent<Rigidbody2D>();
            }
            

            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePos;

            Vector2 mouthDir = target.transform.position - Mouth.position;
            Mouth.rotation = Quaternion.FromToRotation(Vector2.right, mouthDir);

            Vector2 leftEyeDir = target.transform.position - EyeLeft.position;
            EyeLeft.localPosition = leftEyeDir.normalized * EyeRadius;

            Vector2 rightEyeDir = target.transform.position - EyeRight.position;
            EyeRight.localPosition = rightEyeDir.normalized * EyeRadius;
        }
    }

    private void FixedUpdate()
    {
        if(target != null)
        {
            Vector2 mouthDir = target.transform.position - Mouth.position;
            if (mouthDir.magnitude <= MaxBlowLength)
            {
                float force = MaxBlowStrength + blowK * mouthDir.magnitude;
                targetRB.AddForce(mouthDir.normalized * force);
                targetRB.AddTorque(-Mathf.Sign(mouthDir.x) * BlowTorque);
            }
        }
    }

    GameObject GetClosestBalloon()
    {
        float maxDist = float.MaxValue;
        GameObject balloon = null;
        foreach(GameObject o in GameManager.instance.Balloons)
        {
            float dist = Vector3.Distance(transform.position, o.transform.position);
            if (dist < maxDist)
            {
                maxDist = dist;
                balloon = o;
            }
        }
        return balloon;
    }
}
