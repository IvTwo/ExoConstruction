using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Splines;
using Yarn.Saliency;
using Yarn.Unity;

/// <summary>
/// Moves the Virtual Instructor.
/// 
/// Called via Yarn Scripts
/// </summary>
public class InstructorAnim : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float rotateSpeed = 5f;

    [SerializeField] private PathMover pathMover;
    [SerializeField] private SplineContainer walkPath;
    private List<BezierKnot> knotList; // TODO: def a way to use arrays here but ._ .
    private int knotIndex = 0;

    private Transform t;
    private Transform lookTarget;
    private bool canRotate = false;

    void Start() {
        t = transform;
        pathMover = GetComponent<PathMover>();
        knotList = walkPath.Spline.Knots.ToList();
    }

    void Update() {
        if (canRotate) {
            RotateInstructor();
        }
    }

    /// <summary>
    /// Called in Yarn scripts. Moves the Instructor to the knot point specified.
    /// </summary>
    [YarnCommand("move_instructor")]
    public void MoveVirtualInstructor(int knotIndexToMoveTo) {
        List<BezierKnot> path = knotList.GetRange(knotIndex, knotIndexToMoveTo + 1);
        pathMover.MoveAlongPath(t, path, moveSpeed);
    }

    public void SetLookTarget(Transform target) {
       lookTarget = target;
    }

    public void StartRotate() { canRotate = true; }
    public void StopRotate() { canRotate = false; }

    private void RotateInstructor() {
        Vector3 direction = (lookTarget.position - t.position).normalized;  // calculate direction to target
        direction.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(direction);   // calc target rotation

        t.rotation = Quaternion.Slerp(t.rotation, lookRotation, rotateSpeed * Time.deltaTime);
    }
}
