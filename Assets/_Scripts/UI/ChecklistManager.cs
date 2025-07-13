using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

/// <summary>
/// Something I scrapped together for the YarnDialogue to Call.
/// Makes the Checklist appear for the user
/// </summary>
public class ChecklistManager : MonoBehaviour
{
    [SerializeField] private GameObject questHeader;
    [SerializeField] private GameObject checklistItem1;

    [YarnCommand("activate_checklist")]
    public void ActivateChecklist() {
        questHeader.SetActive(true);
        Invoke("ActivateCheckListItem1", 2.0f);
    }

    public void ActivateCheckListItem1() {
        checklistItem1.SetActive(true);
    }
}
