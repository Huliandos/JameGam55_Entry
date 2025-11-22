using UnityEngine;

public class StoryController : MonoBehaviour
{
    [SerializeField] GameObject introSpeech;
    [SerializeField] GameObject lostSpeech;
    [SerializeField] GameObject wonSpeech;
    [SerializeField] GameObject startButton;
    [SerializeField] Animator kickAnimator;
    [SerializeField] Animator talkAnimator;





    public void PlayIntro()
    {
        introSpeech.SetActive(true);
        lostSpeech.SetActive(false);
        wonSpeech.SetActive(false);

        startButton.SetActive(false);
    }

    private void PlayLost()
    {
        introSpeech.SetActive(false);
        lostSpeech.SetActive(true);
        wonSpeech.SetActive(false);
    }

    private void PlayWon()
    {
        introSpeech.SetActive(false);
        lostSpeech.SetActive(false);
        wonSpeech.SetActive(true);
    }

    public void PlayGameEnd()
    {
        if (Beautify.collectedItems == 5)
            PlayWon();
        else
            PlayLost();
    }

}
