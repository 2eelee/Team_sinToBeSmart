using UnityEngine;

public class LookAtARCamera : MonoBehaviour
{
    public Transform arCameraTransform;

    void LateUpdate()
    {
        if (arCameraTransform == null) return;

        Vector3 target = arCameraTransform.position;
        target.y = transform.position.y;
        transform.LookAt(target);
    }
}
