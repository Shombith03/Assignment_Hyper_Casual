using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform[] waypoints; // Waypoints the platform will follow
    public float speed = 2f; // Speed of the platform
    private int currentWaypointIndex = 0; // Index of the current waypoint
    private bool movingForward = true; // Flag to indicate direction of movement

    private void Update()
    {
        MovePlatform();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.parent = gameObject.transform;
            GameManager.Instance.ShowTrapName("Moving Platforms");
        }
    }

    private void MovePlatform()
    {
        if (waypoints.Length == 0)
            return;

        // Calculate the direction towards the current waypoint
        Vector3 targetPosition = waypoints[currentWaypointIndex].position;
        Vector3 moveDirection = targetPosition - transform.position;

        // Move towards the current waypoint
        transform.Translate(moveDirection.normalized * speed * Time.deltaTime, Space.World);

        // If the platform reaches the current waypoint, switch to the next one
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            if (movingForward)
            {
                currentWaypointIndex++;
                if (currentWaypointIndex >= waypoints.Length)
                {
                    currentWaypointIndex = waypoints.Length - 2;
                    movingForward = false;
                }
            }
            else
            {
                currentWaypointIndex--;
                if (currentWaypointIndex < 0)
                {
                    currentWaypointIndex = 1;
                    movingForward = true;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.parent = null;
        }
    }
}