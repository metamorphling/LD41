using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour {

    [SerializeField]
    InputField textField;
    [SerializeField]
    HeroController hero;

    string text;
    Dictionary<string, GameText.Commands> CommandDictionary;
    GameText.Commands toSend = GameText.Commands.Stop;

    void Start() {
        CommandDictionary = new Dictionary<string, GameText.Commands>();
        CommandDictionary.Add("left", GameText.Commands.Left);
        CommandDictionary.Add("right", GameText.Commands.Right);
        CommandDictionary.Add("stop", GameText.Commands.Stop);
        CommandDictionary.Add("jump", GameText.Commands.Jump);
    }

    void Update() {
        if (textField.isFocused == false)
        {
            textField.ActivateInputField();
        }
    }

    void CleanField()
    {
        textField.text = "";
    }

    void ReadText()
    {
        text = textField.text;
    }

    public void Submit()
    {
        ReadText();

        if (CommandDictionary.TryGetValue(text.ToLower(), out toSend))
        {
            hero.ExecuteCommand(toSend);
        }

        CleanField();
    }
}
