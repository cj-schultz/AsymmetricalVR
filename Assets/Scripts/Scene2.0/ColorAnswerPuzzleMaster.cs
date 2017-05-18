using System.Collections.Generic;
using UnityEngine;

// Press 'P' to complete puzzle
public class ColorAnswerPuzzleMaster : MonoBehaviour
{
    public delegate void OnColorsAnswered();
    public static event OnColorsAnswered ColorsAnswered;

    public enum ColorAnswerType { Red, Green, Blue };

    public List<ColorAnswerType> answerKey;
    [Space(5)]
    [SerializeField]
    private MeshRenderer[] answerBlocks;

    [Space(5)]
    [SerializeField]
    private GameObject blockingDoor;

    [Space(5)]
    [SerializeField]
    private Material mat_Red;
    [SerializeField]
    private Material mat_Blue;
    [SerializeField]
    private Material mat_Green;
    [SerializeField]
    private Material mat_Blank;

    private bool answeredPuzzle = false;

    private List<ColorAnswerType> currentAnswer;

    void Update()
    {
        if (!answeredPuzzle && Input.GetKey(KeyCode.P))
        {
            ProcessCorrectAnswer();
        }
    }

    void Awake()
    {
        currentAnswer = new List<ColorAnswerType>();
    }

    public void ProcessColorInput(ColorAnswerType color)
    {
        if(answeredPuzzle)
        {
            return;
        }

        currentAnswer.Add(color);
        MeshRenderer currentBlock = answerBlocks[currentAnswer.Count - 1];

        switch(color)
        {
            case ColorAnswerType.Blue:
                currentBlock.material = mat_Blue;
                break;
            case ColorAnswerType.Red:
                currentBlock.material = mat_Red;
                break;
            case ColorAnswerType.Green:
                currentBlock.material = mat_Green;
                break;
        }

        bool correct = CheckAnswer();

        if(!correct && currentAnswer.Count == answerKey.Count)
        {
            ClearAnswer();
            return;
        }

        if (correct && currentAnswer.Count == answerKey.Count)
        {            
            ProcessCorrectAnswer();
        }
        
    }

    private void ClearAnswer()
    {
        currentAnswer = new List<ColorAnswerType>();

        foreach(MeshRenderer mr in answerBlocks)
        {
            mr.material = mat_Blank;
        }
    }

    private void ProcessCorrectAnswer()
    {
        answeredPuzzle = true;

        blockingDoor.SetActive(false);

        if(ColorsAnswered != null)
        {
            ColorsAnswered();
        }
    }

    private bool CheckAnswer()
    {
        bool correct = true;

        for (int i = 0; i < currentAnswer.Count; i++)
        {
            if(currentAnswer[i] != answerKey[i])
            {
                correct = false;
                break;
            }
        }

        return correct;
    }
}
