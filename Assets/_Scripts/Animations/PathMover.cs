using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMover : MonoBehaviour
{
    public void MoveAlongPath(Transform mover, List<Transform> pathPoints, float moveSpeed, float pauseDuration) {
        StartCoroutine(MovePathCoroutine(mover, pathPoints, moveSpeed, pauseDuration));
    }

    private IEnumerator MovePathCoroutine(Transform mover, List<Transform> pathPoints, float moveSpeed, float pauseDuration) {
        if (pathPoints == null || pathPoints.Count == 0)
            yield break;

        foreach (Transform targetPoint in pathPoints) {
            // Move toward the current point
            while (Vector3.Distance(mover.position, targetPoint.position) > 0.01f) {
                mover.position = Vector3.MoveTowards(mover.position, targetPoint.position, moveSpeed * Time.deltaTime);
                yield return null;
            }

            mover.position = targetPoint.position; // Snap to exact position

            // Pause at the point
            yield return new WaitForSeconds(pauseDuration);
        }
    }
}
