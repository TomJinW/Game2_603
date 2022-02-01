using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;




public class TitleScreenOptionController : MonoBehaviour
{

    public TitleScreenTypes titleScreenType = TitleScreenTypes.Title;

    public TitleScreenOptions currentSelectedOption = TitleScreenOptions.Start;

    public Canvas titleScreenCanvas;
    public Canvas optionScreenCanvas;

    public void setOption() {
        AudioSource audioData = GetComponent<AudioSource>();
        audioData.Play(0);
        this.transform.localPosition = Constants.titleScreenOptionPositions[(int)currentSelectedOption];
    }
    public void setNextOption() {
        currentSelectedOption = currentSelectedOption.Next();
        setOption();
    }
    public void setPreviousOption() {
        currentSelectedOption = currentSelectedOption.Previous();
        setOption();
    }
    public void processSelection() {
        AudioSource audioData = GetComponent<AudioSource>();
        audioData.Play(0);
        switch (currentSelectedOption) {
            case TitleScreenOptions.Start:
                if (titleScreenType == TitleScreenTypes.Pause)
                {
                    Time.timeScale = 1;
                    titleScreenCanvas.gameObject.SetActive(false);
                }
                else
                {
                    SceneManager.LoadScene("Level Layout");
                }
          
                break;
            case TitleScreenOptions.Option:
                titleScreenCanvas.gameObject.SetActive(false);
                optionScreenCanvas.gameObject.SetActive(true);
                break;
            case TitleScreenOptions.Quit:
                if (titleScreenType == TitleScreenTypes.Pause)
                {
                    Time.timeScale = 1;
                    SceneManager.LoadScene("TitleScreen");
                }
                else {
                    Application.Quit();
                }
                break;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        this.transform.localPosition = Constants.titleScreenOptionPositions[(int)currentSelectedOption];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("d")) {
            setNextOption();
        }
        if (Input.GetKeyDown("a")){
            setPreviousOption();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            setNextOption();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            setPreviousOption();
        }
        if (Input.GetKeyDown(KeyCode.Return)) {
            processSelection();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            processSelection();
        }
    }
}
