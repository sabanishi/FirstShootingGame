using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScene : MonoBehaviour
{
    [SerializeField] GameObject GameStart;
    [SerializeField] GameObject ScoreBoard;
    [SerializeField] GameObject Setting;
    [SerializeField] GameObject Sentence;
    [SerializeField] GameObject Close;
    Text gameStart, scoreBoard, setting, close,sentence;
    TextEnum textEnum;
    
    void Update()
    {
        if (gameStart == null)
        {
            gameStart = GameStart.GetComponent<Text>();
            scoreBoard = ScoreBoard.GetComponent<Text>();
            setting = Setting.GetComponent<Text>();
            close = Close.GetComponent<Text>();
            sentence = Sentence.GetComponent<Text>();
            textEnum = TextEnum.GAMESTART;
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            switch (textEnum)
            {
                case TextEnum.GAMESTART:
                    SceneManager.LoadScene("GameScene");
                    break;
                case TextEnum.SCORE:
                    break;
                case TextEnum.SETTING:
                    break;
                case TextEnum.CLOSE:
                    UnityEditor.EditorApplication.isPlaying = false;
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            switch (textEnum)
            {
                case TextEnum.GAMESTART:
                    textEnum = TextEnum.SCORE;
                    break;
                case TextEnum.SCORE:
                    textEnum = TextEnum.SETTING;
                    break;
                case TextEnum.SETTING:
                    textEnum = TextEnum.CLOSE;
                    break;
                case TextEnum.CLOSE:
                    textEnum = TextEnum.GAMESTART;
                    break;
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            switch (textEnum)
            {
                case TextEnum.GAMESTART:
                    textEnum = TextEnum.CLOSE;
                    
                    break;
                case TextEnum.SCORE:
                    textEnum = TextEnum.GAMESTART;
                  
                    break;
                case TextEnum.SETTING:
                    textEnum = TextEnum.SCORE;
                    
                    break;
                case TextEnum.CLOSE:
                    textEnum = TextEnum.SETTING;
                   
                    break;
            }
        }

        gameStart.color = Color.yellow;
        scoreBoard.color = Color.yellow;
        setting.color = Color.yellow;
        close.color = Color.yellow;
        switch (textEnum)
        {
            case TextEnum.GAMESTART:
                gameStart.color = Color.red;
                sentence.text = "ゲームを始めます";
                break;
            case TextEnum.SCORE:
                scoreBoard.color = Color.red;
                sentence.text = "スコアを見ます";
                break;
            case TextEnum.SETTING:
                setting.color = Color.red;
                sentence.text = "設定します";
                break;
            case TextEnum.CLOSE:
                close.color = Color.red;
                sentence.text = "終わります";
                break;
        }
    }

    public void ButtonClicked()
    {
        SceneManager.LoadScene("GameScene");
    }
}

public enum TextEnum
{
    GAMESTART,SCORE,SETTING,CLOSE
}
