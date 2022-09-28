// DecompilerFi decompiler from Assembly-CSharp.dll class: garageOrbit
using UnityEngine;
using NWH.VehiclePhysics;

public class garageOrbit : MonoBehaviour
{
    public Camera nGuiCam;

    public Transform target;

    public float distance;

    public float xSpeed = 120f;

    public float ySpeed = 120f;

    public float yMinLimit = -20f;

    public float yMaxLimit = 80f;

    public float distanceMin = 0.5f;

    public float distanceMax = 15f;

    public float smoothTime = 2f;

    private float rotationYAxis;

    private float rotationXAxis;

    private float velocityX;

    private float velocityY;

    private float tDistance = 15f;

    private bool isStartDelay;

    public PinchZoom touchZoom;

    public bool enorbit = true;


    private void Start()
    {
        //   target = GameObject.Find("Car_11_pysics(Clone)").transform;


        Vector3 eulerAngles = base.transform.eulerAngles;
        rotationYAxis = 0;
        rotationXAxis = 20;
        tDistance = 10;
        //UnityEngine.Debug.Log(tDistance);

    }

    private void LateUpdate()
    {
        if (!enorbit || !target)
        {
            target = GameObject.FindGameObjectWithTag("PlayerFollow").transform;
            return;
        }


#if UNITY_ANDROID
        if (UnityEngine.Input.touchCount == 1 && !CarsController.instance.Right && !CarsController.instance.Left && !CarsController.instance.Accelerate && SteeringControl.wheelBeingHeld == false && UnityEngine.Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            float num = velocityX;
            if (GamePlayTutManager.instance && PlayerPrefs.GetInt("AfterCamRotate") != 1 && GameStats.Instance.CurrentLevel == 4)
                GamePlayTutManager.instance.SpeedActionCall();

            Vector2 deltaPosition = UnityEngine.Input.GetTouch(0).deltaPosition;
            velocityX = num + deltaPosition.x * 0.025f;
            float num2 = velocityY;
            Vector2 deltaPosition2 = UnityEngine.Input.GetTouch(0).deltaPosition;
            velocityY = num2 + deltaPosition2.y * 0.015f;
        }

#endif

#if UNITY_EDITOR

        if (Input.GetMouseButton(0))
        {
            if (GamePlayTutManager.instance && PlayerPrefs.GetInt("AfterCamRotate") != 1 && GameStats.Instance.CurrentLevel == 4)
                GamePlayTutManager.instance.SpeedActionCall();
            velocityX += xSpeed * UnityEngine.Input.GetAxis("Mouse X") * 0.02f;
            velocityY += ySpeed * UnityEngine.Input.GetAxis("Mouse Y") * 0.02f;
        }
#endif
        rotationYAxis += velocityX;
        rotationXAxis -= velocityY;
        rotationXAxis = ClampAngle(rotationXAxis, yMinLimit, yMaxLimit);
        Vector3 localEulerAngles = base.transform.localEulerAngles;
        float x = localEulerAngles.x;
        Vector3 localEulerAngles2 = base.transform.localEulerAngles;
        Quaternion quaternion = Quaternion.Euler(x, localEulerAngles2.y, 0f);
        Quaternion quaternion2 = Quaternion.Euler(rotationXAxis, rotationYAxis, 0f);
        Quaternion quaternion3 = quaternion2;

#if UNITY_EDITOR

        tDistance = Mathf.Clamp(tDistance - UnityEngine.Input.GetAxis("Mouse ScrollWheel") * 5f, distanceMin, distanceMax);
        distance = Mathf.Lerp(distance, tDistance, Time.deltaTime * 0.5f);
#endif

#if UNITY_ANDROID

        tDistance = Mathf.Clamp(tDistance - touchZoom.touchWhell * 0.05f, distanceMin, distanceMax);
        distance = Mathf.Lerp(distance, tDistance, Time.deltaTime * 5f);
#endif
        Vector3 point = new Vector3(0f, 0f, 0f - distance);
        Vector3 localPosition = quaternion3 * point;
        base.transform.localRotation = quaternion3;
        base.transform.localPosition = localPosition;
        velocityX = Mathf.Lerp(velocityX, 0f, Time.deltaTime * smoothTime);
        velocityY = Mathf.Lerp(velocityY, 0f, Time.deltaTime * smoothTime);
    }


    private void OnDisable()
    {
        PlayerPrefs.SetFloat("rotationYAxis", rotationYAxis);
        PlayerPrefs.SetFloat("rotationXAxis", rotationXAxis);
        PlayerPrefs.SetFloat("distance", tDistance);
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f)
        {
            angle += 360f;
        }
        if (angle > 360f)
        {
            angle -= 360f;
        }
        return Mathf.Clamp(angle, min, max);
    }

}


