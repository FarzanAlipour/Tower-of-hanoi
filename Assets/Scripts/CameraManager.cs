using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Vector3 initialPosition;
    private void OnEnable()
    {
        initialPosition = transform.position;
    }

    public void FitToCamera(float diskRad)
    {
        float zPos = initialPosition.z - diskRad*2;
        transform.position = new Vector3(initialPosition.x, initialPosition.y, zPos);
    }
}
