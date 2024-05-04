using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset;

    private float speed = 10f;

    public Transform Target { get => target; set => target = value; }

    private void Start()
    {
        CameraManager.instance.SetCameraFollow(this);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(Target != null)
        {
            Vector3 pos = Target.position;
            transform.position = Vector3.Lerp(transform.position + offset, pos, speed * Time.fixedDeltaTime);
        }
        
    }
}
