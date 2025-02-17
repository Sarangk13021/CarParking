using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICharacterRoaming : MonoBehaviour
{
    public Transform path; // Parent object containing waypoints
    public float speed = 0.8f; // Walking speed
    public float rotationSpeed = 5f; // Turning speed
    private List<Transform> waypoints = new List<Transform>();
    private int currentWaypoint = 0;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        foreach (Transform child in path)
        {
            if (child != transform)
                waypoints.Add(child);
        }

        if (waypoints.Count > 0)
            StartCoroutine(MoveToNextWaypoint());
    }

    IEnumerator MoveToNextWaypoint()
    {
        while (true)
        {
            Transform target = waypoints[currentWaypoint];
            Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, target.position.z);

            // Rotate while moving forward
            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                // Smooth rotation without stopping movement
                Vector3 direction = (targetPosition - transform.position).normalized;
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

                // Move forward
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                animator.SetBool("isWalking", true);

                yield return null;
            }

            animator.SetBool("isWalking", false);
            yield return new WaitForSeconds(1f);
            currentWaypoint = (currentWaypoint + 1) % waypoints.Count;
        }
    }
}