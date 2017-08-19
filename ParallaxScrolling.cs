using UnityEngine;

using System.Collections;

public class ParallaxScrolling : MonoBehaviour
{
    public Transform[] backgrounds; // Array(list) of all back and fore grounds to be parallaxed.
    private float[] parallaxScales; // The proportion of the camera's movement to move the backgrounds by.
    public float smoothing=1f; // How smooth the parallax will be. Make sure > 0.

    private Transform cam; // Reference to the mainCamera's transform.
    private Vector3 previousCamPos; // The position of the camera in the previous frame.

    // Is called before Start(). Great for references.
    void Awake()    
    {
        // set up the camera reference.
        cam = Camera.main.transform;
    }
    // Use this for initialization.
    void Start()
    {
        // The previous frame had the current frame's camera position.
        previousCamPos = cam.position;
        // Assigning corresponding parallax scales.
        parallaxScales = new float[backgrounds.Length];
        for(int i=0;i<backgrounds.Length; ++i)
        {
            parallaxScales[i] = backgrounds[i].position.z * -1;
        }
    }

    void Update()
    {
        // for each background.
        for(int i=0;i<backgrounds.Length;++i)
        {
            // parallax is the opposite of the camera movement because the previous frame multiplied by parallaxScale;
            float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];

            // set a target x position which is the current position + parallax.
            float BackgroundTargetPosX = backgrounds[i].position.x + parallax;
            // Create a target position == Vector3 which has the x position equal to BackgroundTargetPosX.
            Vector3 BackgroundTargetPos = new Vector3(BackgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);
            // Note: We can do the same with the y position if needed.
            // Fade between the current position and the target position using Lerp.
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, BackgroundTargetPos, smoothing * Time.deltaTime);
        }
        // Set the previous cam position to the actual cam position at the end of the frame.
        previousCamPos = cam.position;
    }
}