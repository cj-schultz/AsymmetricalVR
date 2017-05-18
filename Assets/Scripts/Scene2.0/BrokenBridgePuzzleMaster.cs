using UnityEngine;

public class BrokenBridgePuzzleMaster : MonoBehaviour
{    
    [SerializeField]
    private MissingBridgeBlock missingBridgeBlock;

    [Space(3)]
    [SerializeField]
    private MeshRenderer redBlockMesh;
    [SerializeField]
    private MeshRenderer greenBlockMesh;
    [SerializeField]
    private Material redMat;
    [SerializeField]
    private Material greenMat;

    void OnEnable()
    {
        ColorAnswerPuzzleMaster.ColorsAnswered += HandleColorsAnswered;
    }

    void OnDisable()
    {
        ColorAnswerPuzzleMaster.ColorsAnswered -= HandleColorsAnswered;
    }

    private void HandleColorsAnswered()
    {
        redBlockMesh.material = redMat;
        greenBlockMesh.material = greenMat;

        missingBridgeBlock.GetComponent<ColorAnswerButton>().isActive = false;

        missingBridgeBlock.readyForInteraction = true;
    }
}
