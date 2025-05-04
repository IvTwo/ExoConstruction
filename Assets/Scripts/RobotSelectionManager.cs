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
        if (previewSet == null)
        {
            Debug.LogWarning("No suit previewed to apply.");
            return;
        }

        var stage = ProgressManager.Instance?.CurrentStage ?? ProgressManager.Stage.StartWalkthrough;

        // Restrict suit application by current stage
        switch (stage)
        {
            case ProgressManager.Stage.RobotCarpentry:
                if (previewSet.suitType != ExosuitType.Back)
                {
                    Debug.LogWarning("Only the Back (passive) suit can be applied for Carpentry.");
                    return;
                }
                ProgressManager.Instance.AdvanceToStage(ProgressManager.Stage.RobotCarpentryActivitySelect);
                break;

            case ProgressManager.Stage.RobotConstructionLabor:
                if (previewSet.suitType != ExosuitType.FullBody)
                {
                    Debug.LogWarning("Only the Full Body suit can be applied for Construction Labor.");
                    return;
                }
                ProgressManager.Instance.AdvanceToStage(ProgressManager.Stage.RobotConstructionLaborActivitySelect);
                break;

            case ProgressManager.Stage.RobotDrywall:
                if (previewSet.suitType != ExosuitType.Shoulder)
                {
                    Debug.LogWarning("Only the Shoulder suit can be applied for Drywall.");
                    return;
                }
                ProgressManager.Instance.AdvanceToStage(ProgressManager.Stage.RobotDrywallActivitySelect);
                break;

            case ProgressManager.Stage.RobotElectrician:
                if (previewSet.suitType != ExosuitType.Shoulder)
                {
                    Debug.LogWarning("Only the Shoulder suit can be applied for Electrician.");
                    return;
                }
                ProgressManager.Instance.AdvanceToStage(ProgressManager.Stage.RobotElectricianActivitySelect);
                break;

            case ProgressManager.Stage.RobotMasonry:
                if (previewSet.suitType != ExosuitType.Back)
                {
                    Debug.LogWarning("Only the Back (passive) suit can be applied for Masonry.");
                    return;
                }
                ProgressManager.Instance.AdvanceToStage(ProgressManager.Stage.RobotMasonryActivitySelect);
                break;
        }

        // Apply suit
        equippedSet = previewSet;
        Debug.Log("Applied suit: " + equippedSet.suitType);
        onSuitApplied.Invoke(equippedSet);
    }

    public void UnequipSuit()
    {
        equippedSet = null;
        previewSet = null;
        onSuitApplied.Invoke(null);
    }

    public bool HasEquippedSuit() => equippedSet != null;
}