using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Camera cam;
    bool yeah = false;

    float dist = 5f;

    // Start is called before the first frame update
    private void Update()
    {
       /*  if (!yeah)
        {
            Teleportation();
            yeah = true;
        }*/
    }


    public void Teleportation()
    {
        Vector3 newDirection = ChangePosition();
        gameObject.transform.position = cam.transform.position + newDirection * 3;
    }
    public Vector3 ChangePosition()
    {
        int side = Random.Range(1, 3);
        if (side == 1)
        {
            return LeftTeleport();
        }
        else
        {
            return RightTeleport();
        }
    }

    public Vector3 LeftTeleport()
    {
        RaycastHit hit;
        Vector3 direction = -cam.transform.forward - (cam.transform.right * 4 / 3);
        direction.Normalize();
        if (!Physics.Raycast(cam.transform.position, direction, out hit, dist))
        {
            Debug.DrawRay(cam.transform.position, direction, Color.black);
            return direction;
        }
        direction = -cam.transform.forward - (cam.transform.right * 6 / 2);
        direction.Normalize();
        if (!Physics.Raycast(cam.transform.position, direction, out hit, dist))
        {
            Debug.DrawRay(cam.transform.position, direction, Color.green);
            return direction;
        }
        direction = -cam.transform.right;
        direction.Normalize();
        if (!Physics.Raycast(cam.transform.position, direction, out hit, dist))
        {
            Debug.DrawRay(cam.transform.position, direction, Color.blue);
            return direction;
        }
        direction = cam.transform.forward - (cam.transform.right * 4 / 3);
        direction.Normalize();
        if (!Physics.Raycast(cam.transform.position, direction, out hit, dist))
        {
            Debug.DrawRay(cam.transform.position, direction, Color.magenta);
            return direction;
        }
        else
        {
            return RightTeleport();
        }
    }
    public Vector3 RightTeleport()
    {
        RaycastHit hit;
        Vector3 direction = -cam.transform.forward + (cam.transform.right * 4 / 3);
        direction.Normalize();
        if (!Physics.Raycast(cam.transform.position, direction, out hit, dist))
        {
            Debug.DrawRay(cam.transform.position, direction, Color.black);
            return direction;
        }
        direction = -cam.transform.forward + (cam.transform.right * 6 / 2);
        direction.Normalize();
        if (!Physics.Raycast(cam.transform.position, direction, out hit, dist))
        {
            Debug.DrawRay(cam.transform.position, direction, Color.green);
            return direction;
        }
        direction = cam.transform.right;
        direction.Normalize();
        if (!Physics.Raycast(cam.transform.position, direction, out hit, dist))
        {
            Debug.DrawRay(cam.transform.position, direction, Color.blue);
            return direction;
        }
        direction = cam.transform.forward + (cam.transform.right * 4 / 3);
        direction.Normalize();
        if (!Physics.Raycast(cam.transform.position, direction, out hit, dist))
        {
            Debug.DrawRay(cam.transform.position, direction, Color.magenta);
            return direction;
        }
        else
        {
            return LeftTeleport();
        }
    }
}
