using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCloud : MonoBehaviour
{



    [Header("GFX")]
    public Transform Mouth;
    public Transform EyeLeft;
    public Transform EyeRight;
    public float EyeRadius;

    
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 mouthDir = mousePos - Mouth.position;
        Mouth.rotation = Quaternion.FromToRotation(Vector2.right, mouthDir);

        Vector2 leftEyeDir = mousePos - EyeLeft.position;
        EyeLeft.localPosition = leftEyeDir.normalized * EyeRadius;

        Vector2 rightEyeDir = mousePos - EyeRight.position;
        EyeRight.localPosition = rightEyeDir.normalized * EyeRadius;
    }
}
