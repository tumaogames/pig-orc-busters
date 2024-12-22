using UnityEngine;

public class GlobeCam : MonoBehaviour
{
    public float targetAspect = 1920f / 1080f; // The reference aspect ratio (16:9)

    void Start()
    {
        AdjustCameraFOV();
    }

    void AdjustCameraFOV()
    {
        // Get the current aspect ratio
        float currentAspect = (float)Screen.width / Screen.height;

        // Calculate the adjustment factor
        float aspectRatioDifference = currentAspect / targetAspect;

        // Adjust the vertical FOV to maintain consistent horizontal FOV
        Camera.main.fieldOfView = 2f * Mathf.Atan(Mathf.Tan(Mathf.Deg2Rad * 60f / 2f) / aspectRatioDifference) * Mathf.Rad2Deg;
    }
}

