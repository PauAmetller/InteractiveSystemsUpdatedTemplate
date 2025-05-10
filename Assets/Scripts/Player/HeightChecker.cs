using UnityEngine;

public class HeightChecker : MonoBehaviour
{
    private float lastHeight;
    private float peakHeight;
    private float fallStartTime;
    private float lastShootTime = -Mathf.Infinity;

    private bool isMoving = false;
    private bool hasTriggered = false;

    private float shootCooldown = 1f;
    public float minDistance = 0.5f;
    public float maxDuration = 0.5f;

    public AudioManager audioManager;
    public GameObject bulletPrefab;

    void Start()
    {
        lastHeight = transform.position.y;
        peakHeight = lastHeight;
    }

    void Update()
    {
        bool fastFall = DetectFastMovement(-1, minDistance, maxDuration);

        if (fastFall && Time.time - lastShootTime >= shootCooldown)
        {
            shootBullet();
            lastShootTime = Time.time;
        }

        lastHeight = transform.position.y;
    }

    bool DetectFastMovement(int direction, float minDistance, float maxDuration)
    {
        float currentHeight = transform.position.y;
        float deltaY = currentHeight - lastHeight;
        float currentTime = Time.time;

        bool isMovingInDesiredDirection = (direction == -1 && deltaY < -0.01f) || (direction == 1 && deltaY > 0.01f);
        bool isOppositeDirection = (direction == -1 && deltaY > 0.01f) || (direction == 1 && deltaY < -0.01f);

        if (isOppositeDirection)
        {
            isMoving = false;
            hasTriggered = false;
            return false;
        }

        if (!isMoving && isMovingInDesiredDirection) 
        {
            hasTriggered = false;
            isMoving = true;
            peakHeight = lastHeight;
            fallStartTime = currentTime;
            return false;
        }

        if (isMoving)
        {
            float distance = Mathf.Abs(currentHeight - peakHeight);
            float duration = currentTime - fallStartTime;

            if (!hasTriggered && distance >= minDistance && duration <= maxDuration)
            {
                hasTriggered = true;
                return true;
            }

            if (Mathf.Abs(deltaY) < 0.01f)
            {
                isMoving = false;
                hasTriggered = false;
                return false;
            }
        }

        return false;
    }

    public void shootBullet()
    {
        Vector3 transformPosition = transform.position;
        transformPosition.y = 0.0f;
        Quaternion flatRotation = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);

        GameObject bullet = Instantiate(bulletPrefab, transformPosition, flatRotation);
        bullet.GetComponent<Renderer>().material = GetComponent<Renderer>().material;
    }
}
