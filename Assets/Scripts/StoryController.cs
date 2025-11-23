using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StoryController : MonoBehaviour
{
    [SerializeField] GameObject introSpeech;
    [SerializeField] GameObject lostSpeech;
    [SerializeField] GameObject wonSpeech;

    [SerializeField] GameObject startButton;
    [SerializeField] GameObject replayButton;

    [SerializeField] GameObject collectedItemsCount, timeSpendCount;

    [SerializeField] TimeTrackerComponent _timer;

    [SerializeField] Animator kickAnimator;
    [SerializeField] Animator talkAnimator;
    [SerializeField] string talkAnimation = "GFtalk";
    [SerializeField] string kickAnimation = "GFkick";
    [SerializeField] GameObject GameWonVFX1;
    [SerializeField] GameObject GameWonVFX2;
    [SerializeField] int requiredCollectibles = 0;

    bool hasGameStarted = false;



    public void PlayIntro()
    {
        introSpeech.SetActive(true);
        lostSpeech.SetActive(false);
        wonSpeech.SetActive(false);

        talkAnimator.Play(talkAnimation, 0, 0.0f);
        kickAnimator.Play(kickAnimation, 0, 0.0f);

        startButton.SetActive(false);
        StartCoroutine(setGameStartBoolTrue(15));
    }

    private void PlayLost()
    {
        introSpeech.SetActive(false);
        lostSpeech.SetActive(true);
        wonSpeech.SetActive(false);

        talkAnimator.Play(talkAnimation, 0, 0.0f);
        kickAnimator.Play(kickAnimation, 0, 0.0f);

        //replayButton.SetActive(true);

        collectedItemsCount.SetActive(true); 
        timeSpendCount.SetActive(true);
    }
    
    private void PlayWon()
    {
        introSpeech.SetActive(false);
        lostSpeech.SetActive(false);
        wonSpeech.SetActive(true);

        talkAnimator.Play(talkAnimation, 0, 0.0f);

        StartCoroutine(gameWonWFX(3));
    }

    public void PlayGameEnd()
    {
        if (!hasGameStarted)
            return;
        _timer.Stop();

        if (Beautify.collectedItems >= requiredCollectibles)
            PlayWon();
        else
            PlayLost();
    }

    IEnumerator gameWonWFX(int secs)
    {
        yield return new WaitForSeconds(secs);
        GameWonVFX1.SetActive(false);
        GameWonVFX2.SetActive(true);
        replayButton.SetActive(true);
        
        collectedItemsCount.SetActive(true); 
        timeSpendCount.SetActive(true);
    }

    IEnumerator setGameStartBoolTrue(int secs)
    {
        yield return new WaitForSeconds(secs);
        hasGameStarted = true;
    }
    

    public void ReplayScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
