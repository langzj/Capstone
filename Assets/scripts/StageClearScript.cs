using UnityEngine;
using System.Collections;


public class StageClearScript : MonoBehaviour
{

    public GUIText clearStageText;
    public int score;

    public static StageClearScript Instance;

    void Awake()
    {
        // Make sure only one script is initiation, not multiple
        if (Instance != null)
        {
            Debug.LogError("Multiple instances of scoreText!");
        }
        Instance = this;

        clearStageText.enabled = false;
    }


    //The following code was derived from: https://docs.unity3d.com/ScriptReference/WaitForSeconds.html
    public void nextStage()
    {
        StartCoroutine(StageClear());

    }

    public IEnumerator StageClear()
    {
        clearStageText.enabled = true;
        clearStageText.text = "FIRST STAGE CLEAR!";
        yield return new WaitForSeconds(5);
        clearStageText.text = "Prepare for NEXT STAGE...";
        yield return new WaitForSeconds(4);
        Application.LoadLevel("Stage2");
    }

    //The following code was derived from: https://docs.unity3d.com/ScriptReference/WaitForSeconds.html
    public void completeGame()
    {
        StartCoroutine(FinalStageClear());

    }

    public IEnumerator FinalStageClear()
    {
        clearStageText.enabled = true;
        clearStageText.text = "FINAL STAGE CLEAR!";
        yield return new WaitForSeconds(5);
        clearStageText.text = "Congratulations... You have saved the humankind.";
        yield return new WaitForSeconds(4);
        Application.LoadLevel("Menu");
    }
}

