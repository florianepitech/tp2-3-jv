using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Reference to the sphere's Transform
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public bool ok = false;
    
    
    void LateUpdate()
    {
        target = GameObject.Find("Player(Clone)").transform;
        if (target == null)
            return;
        //i want third person camera with mouse rotation
        offset = new Vector3(0, 2, -5);
        //calculate the offset position
        
        
        
        
        if (!ok)
        {
            transform.position = target.position + offset;
            transform.LookAt(target);
            ok = true;
        }

        
        float horizontalInput = Input.GetAxis("Mouse X"); 
        //arrow keys

        if (horizontalInput == 0)
            horizontalInput = KeyboardEvent.ArrowKeysHorizontal();
         
         transform.RotateAround(target.position, Vector3.up, horizontalInput);
    }
}