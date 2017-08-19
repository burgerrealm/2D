using System.Collections;

using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))] // Good way to make sure we always have a sprite renderer.!!
public class Tilling : MonoBehaviour
{
    public int offsetX = 2; // the offset on x so we dont get any weird errors.
    // These are used to check if we need to instantiate any stuff.
    public bool hasRightBuddy = false;
    public bool hasLeftBuddy = false;

    public bool reverseScale = false; // used if the object is not tillable.
    private float spriteWidth = 0f;   // the width of our texture(element).
    // these are for performance reasons.
    private Camera cam;
    private Transform myTransform;
    // best place to reference.
    void Awake()
    {
        cam = Camera.main;
        myTransform = transform;
    }
    // Use this for initialization.
    void Start()
    {
        SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
        spriteWidth = sRenderer.sprite.bounds.size.x;
    }

    void Update()
    {
        // Does it need any buddies?!?
        if (hasLeftBuddy == false || hasRightBuddy == false)
        {
            // calculate the camera extend(half the width) of what the camera can see in world coordinates.
            float cameraHorizontalExt = cam.orthographicSize * Screen.width / Screen.height;

            // calculate the x position where the camera can see the edge of the sprite(element).
            float edgeVisiblePositionRight = (myTransform.position.x + spriteWidth / 2) - cameraHorizontalExt;
            float edgeVisiblePositionLeft = (myTransform.position.x - spriteWidth / 2) + cameraHorizontalExt;

            // checking if we can see the edge of the element and call MakeNewBuddy if we can.
            if (cam.transform.position.x >= edgeVisiblePositionRight - offsetX && hasRightBuddy == false)
            {
                MakeNewBuddy(1);
                hasRightBuddy = true;
            }
            else if (cam.transform.position.x <= edgeVisiblePositionLeft + offsetX && hasLeftBuddy == false)
            {
                MakeNewBuddy(-1);
                hasLeftBuddy = true;
            }
        }
    }
    void MakeNewBuddy(int rightOrLeft)
    {
        // calculating the new position for our buddy.
        Vector3 newPos = new Vector3(myTransform.position.x + spriteWidth * rightOrLeft, myTransform.position.y, myTransform.position.z);
        Transform newBuddy = Instantiate(myTransform, newPos, myTransform.rotation) as Transform;
        // if not tillable let's just reverse the x size of our object to get rid of the ugly seams.
        if(reverseScale)
        {
            newBuddy.localScale = new Vector3(newBuddy.localScale.x * -1, newBuddy.localScale.y, newBuddy.localScale.z);
        }
        newBuddy.parent = myTransform.parent;
        if(rightOrLeft>0)
        {
            newBuddy.GetComponent<Tilling>().hasLeftBuddy = true;
        }
        else
        {
            newBuddy.GetComponent<Tilling>().hasRightBuddy = true;
        }
    }
}