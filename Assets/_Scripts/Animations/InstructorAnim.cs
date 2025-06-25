using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Moves the Virtual Instructor
/// </summary>
public class InstructorAnim : MonoBehaviour
{
    [SerializeField] private Transform character;
    [SerializeField] private List<Transform> pathPoints; //TODO: make class that holds a series of
                                                        // waypoints (sequences to call)

    public void Play() {
        PathMover pathMover = GetComponent<PathMover>();
        pathMover.MoveAlongPath(character, pathPoints, moveSpeed: 2f, pauseDuration: 0);
    }
}
