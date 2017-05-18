using UnityEngine;
using MMVR;

public class ColorAnswerButton : InteractableButton
{
    [SerializeField]
    private bool secretElevatorButton = false;

    [SerializeField]
    private ColorAnswerPuzzleMaster answerMaster;
    [SerializeField]
    private ColorAnswerPuzzleMaster.ColorAnswerType answerColor;

    private bool enabledAsElevatorButton = false;

    public override void Awake()
    {
        base.Awake();
        isActive = true;
    }

    void OnEnable()
    {
        ColorAnswerPuzzleMaster.ColorsAnswered += HandleColorsAnswered;
    }

    void OnDisable()
    {
        ColorAnswerPuzzleMaster.ColorsAnswered -= HandleColorsAnswered;
    }

    public override void ButtonPressedEvent()
    {
        if(enabledAsElevatorButton)
        {
            base.ButtonPressedEvent(); // We can use base event to call elevator's Lift()
        }
        else
        {
            answerMaster.ProcessColorInput(answerColor);
        }        
    }

    private void HandleColorsAnswered()
    {
        if (secretElevatorButton) enabledAsElevatorButton = true;
        isActive = false;
    }
}

