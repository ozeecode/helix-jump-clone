using UnityEngine;
public class CameraFollow : MonoBehaviour
{

    public float CurrentY
    {
        get { return currentY; }
        set
        {
            currentY = value;
            targetPosition.y = value + offset;
        }
    }

    [SerializeField] Transform ballTransform;
    [SerializeField] float followSpeed = 10f;

    float currentY;
    float offset;
    Vector3 targetPosition;

    private void Awake()
    {
        targetPosition = transform.position;
        offset = transform.position.y;
        CurrentY = 0;
    }

    private void LateUpdate()
    {
        if (CurrentY > ballTransform.position.y)
        {
            CurrentY -= GameSettings.DISTANCE_BETWEEN_PLATFORMS;
        }
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * followSpeed);
    }
}
