using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

/// <summary>
///  will be scrapped for a better system later. This is just to show what works
/// </summary>
public class AnimTEST : MonoBehaviour
{
    [SerializeField] private DialogueRunner dialogueRunner;
    [SerializeField] private DoorAnim doorAnim;

    void Start() {
        StartCoroutine(TestRun());
    }

    IEnumerator TestRun() {
        yield return new WaitForSeconds(5);
        doorAnim.Play();
        yield return new WaitForSeconds(3);
        dialogueRunner.StartDialogue("Start");
    }
}
