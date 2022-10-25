using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseInfo:MonoBehaviour
{
    private MGameManager gameManager;
    private PauseText text;
    [SerializeField] GameObject Text2;
    [SerializeField] GameObject Text3;
    [SerializeField] GameObject Text4;
    Text text2, text3, text4;

    public void Init(MGameManager manager)
    {
        gameManager = manager;
        text2 = Text2.GetComponent<Text>();
        text3 = Text3.GetComponent<Text>();
        text4 = Text4.GetComponent<Text>();
        text = PauseText.CONTINUE;
    }

    public void Check()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            switch (text)
            {
                case PauseText.CONTINUE:
                    gameManager.GameResume();
                    break;
                case PauseText.GOSTART:
                    gameManager.GoStart();
                    break;
                case PauseText.GOBACK:
                    gameManager.GoBack();
                    break;
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            switch (text)
            {
                case PauseText.CONTINUE:
                    text = PauseText.GOBACK;
                    break;
                case PauseText.GOSTART:
                    text = PauseText.CONTINUE;
                    break;
                case PauseText.GOBACK:
                    text = PauseText.GOSTART;
                    break;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            switch (text)
            {
                case PauseText.CONTINUE:
                    text = PauseText.GOSTART;
                    break;
                case PauseText.GOSTART:
                    text = PauseText.GOBACK;
                    break;
                case PauseText.GOBACK:
                    text = PauseText.CONTINUE;
                    break;
            }
        }
        text2.color = Color.yellow;
        text3.color = Color.yellow;
        text4.color = Color.yellow;
        switch (text)
        {
            case PauseText.CONTINUE:
                text2.color = Color.black;
                break;
            case PauseText.GOSTART:
                text3.color = Color.black;
                break;
            case PauseText.GOBACK:
                text4.color = Color.black;
                break;
        }
    }
}

public enum PauseText
{
    CONTINUE,GOSTART,GOBACK
}
