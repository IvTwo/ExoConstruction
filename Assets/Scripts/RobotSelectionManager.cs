using UnityEngine;
using UnityEngine.Events;

public class RobotSelectionManager : MonoBehaviour
{
    public static RobotSelectionManager Instance { get; private set; }

    [Header("Equipped (confirmed) suit")]
    public ExosuitMaterialSet equippedSet;

    [Header("Currently previewing suit")]
    public ExosuitMaterialSet previewSet;

    // Event to notify buttons when a suit is applied / previewed
    public UnityEvent<ExosuitMaterialSet> onSuitApplied = new UnityEvent<ExosuitMaterialSet>();
    public UnityEvent<ExosuitMaterialSet> onSuitPreviewed = new UnityEvent<ExosuitMaterialSet>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PreviewSuit(ExosuitMaterialSet set)
    {
        previewSet = set;
        Debug.Log("Previewing suit: " + set.suitType);
        onSuitPreviewed.Invoke(previewSet);
    }

    public void ApplySuit()
    {
        if (previewSet != null)
        {
            equippedSet = previewSet;
            Debug.Log("Applied suit: " + equippedSet.suitType);
            onSuitApplied.Invoke(equippedSet); // Notify all ApplyToAvatarButtons
        }
        else
        {
            Debug.LogWarning("No suit previewed to apply.");
        }
    }

    public void UnequipSuit()
    {
        equippedSet = null;
        previewSet = null;
        onSuitApplied.Invoke(null);
    }

    public bool HasEquippedSuit() => equippedSet != null;
}