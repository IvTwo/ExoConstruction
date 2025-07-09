using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

/// <summary>
/// Takes a transform, and moves it along a path made of bezier knots
/// </summary>
public class PathMover : MonoBehaviour
{
    [SerializeField] private Animator animator; // TODO: temp test, will refactor if we need to use this
                                                // for more than the virtual instructor
    [SerializeField] private InstructorAnim animatorInstructor;

    public void MoveAlongPath(Transform mover, List<BezierKnot> path, float moveSpeed) {
        StartCoroutine(MovePathCoroutine(mover, path, moveSpeed));
    }

    private IEnumerator MovePathCoroutine(Transform mover, List<BezierKnot> path, float moveSpeed) {
        animator.SetBool("isWalking", true);
        animatorInstructor.StopRotate();

        foreach (BezierKnot b in path) {
            // look towards next point in path
            Vector3 lookPosition = b.Position;
            lookPosition.y = mover.position.y;
            mover.LookAt(lookPosition);

            // move towards current point
            while (Vector3.Distance(mover.position, b.Position) > 0.05f) {
                mover.position = Vector3.MoveTowards(mover.position, b.Position, moveSpeed * Time.deltaTime);
                yield return null;
            }
            mover.position = b.Position;    // ensure snap at position
            yield return null;
        }

        animatorInstructor.StartRotate();
        animator.SetBool("isWalking", false);
    }
}
