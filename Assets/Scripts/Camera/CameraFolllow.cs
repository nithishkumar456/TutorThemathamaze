using UnityEngine;

public class CameraFolllow : MonoBehaviour
{
    public Transform playerTransform; 
    [SerializeField] private Vector3 offset = new Vector3 (0, 8, -15);
    [SerializeField] private float followSpeed = 6f;
    private Vector3 cameraTransform; 

    void LateUpdate()
    {
        cameraTransform = playerTransform.position + offset;
        transform.position = Vector3.Lerp(transform.position, cameraTransform, followSpeed * Time.deltaTime);
    }
}
