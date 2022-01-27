using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;




public class TitleScreenOptionController : MonoBehaviour
{

    TitleScreenOptions currentSelectedOption = TitleScreenOptions.Start;

    void setNextOption() {
        currentSelectedOption = currentSelectedOption.Next();
        this.transform.localPosition = Constants.titleScreenOptionPositions[(int)currentSelectedOption];
    }
    void setPreviousOption() {
        currentSelectedOption = currentSelectedOption.Previous();
        this.transform.localPosition = Constants.titleScreenOptionPositions[(int)currentSelectedOption];
    }
    void processSelection() {
        switch (currentSelectedOption) {
            case TitleScreenOptions.Start:
                SceneManager.LoadScene("SampleScene");
                break;
            case TitleScreenOptions.Option:
                break;
            case TitleScreenOptions.Quit:
                Application.Quit();
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
    }
}
