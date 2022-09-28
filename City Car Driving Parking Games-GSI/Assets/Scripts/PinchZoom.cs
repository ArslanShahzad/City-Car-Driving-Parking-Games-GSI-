// DecompilerFi decompiler from Assembly-CSharp.dll class: PinchZoom
using UnityEngine;
using NWH.VehiclePhysics;
public class PinchZoom : MonoBehaviour
{
    public Camera nGuiCam;

    public float touchWhell;

    private void Update()
    {
        if (UnityEngine.Input.touchCount == 2)
        {
            Touch touch = UnityEngine.Input.GetTouch(0);
            Touch touch2 = UnityEngine.Input.GetTouch(1);
            if (SteeringControl.wheelBeingHeld == false && CarsController.instance.Accelerate == false)
            {
                Vector2 a = touch.position - touch.deltaPosition;
                Vector2 b = touch2.position - touch2.deltaPosition;
                float magnitude = (a - b).magnitude;
                float magnitude2 = (touch.position - touch2.position).magnitude;
                float num = touchWhell = magnitude2 - magnitude;
            }
        }
        else
        {
            touchWhell = 0f;
        }
    }


}
