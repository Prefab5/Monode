/*
Description: Allows movement of the camera for testing purposes.
Created: 6/26/2016
*/

using UnityEngine;
using System.Collections;

public class PlaceholderMovement : MonoBehaviour
{
    public new bool enabled = false;
    public float moveSpeed = 4;

    void Update()
    {
        /*If enabled allows us to move the camera in 1 unit increments
        along the x-axis utilizing the left and right arrows or 'a' and 'd' keys.*/
        if (enabled)
        {
            //Controls movement in right direction.
            if (Input.GetButton("Horizontal") && Input.GetAxisRaw("Horizontal") > 0)
            {
                transform.Translate(new Vector2(moveSpeed * Time.deltaTime, 0));
            }
            //Controls movement in left direction.
            else if (Input.GetButton("Horizontal") && Input.GetAxisRaw("Horizontal") < 0)
            {
                transform.Translate(new Vector2(-1 * moveSpeed * Time.deltaTime, 0));
            }
        }
    }
}
