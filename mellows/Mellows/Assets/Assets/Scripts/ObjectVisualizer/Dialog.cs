using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Dialog : MonoBehaviour
{
    /// <summary>
    /// a Bool
    /// </summary>
    public bool a = false;

    /// <summary>
    /// GameController
    /// </summary>
    [SerializeField] private GameController c;

    /// <summary>
    /// Text Display
    /// </summary>
    public TextMeshProUGUI textDisplay;

    /// <summary>
    /// Sentenctes String Array
    /// </summary>
    public string[] sentenctes;

    /// <summary>
    /// Index Int
    /// </summary>
    private int index;

    /// <summary>
    /// Typing Speed Float
    /// </summary>
    public float typingSpeed;

    /// <summary>
    /// Continue Button Object
    /// </summary>
    public GameObject continueButton;

    /// <summary>
    /// Running Bool
    /// </summary>
    public bool running = false;

    void Update(){
        if(textDisplay.text == sentenctes[index]){
            continueButton.SetActive(true);
        }
    }

    /// <summary>
    /// Sowly Display Each Letter
    /// </summary>
    /// <returns></returns>
    public IEnumerator Type(){
        running = true;
        foreach(char letter in sentenctes[index].ToCharArray()){
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        running = false;
    }

    /// <summary>
    /// Scycle Through To The Next Sentence
    /// </summary>
    public void NextSentence(){
        if (running) return;
        continueButton.SetActive(false);
        if(index < sentenctes.Length -1){
            index++;
            textDisplay.text="";
            StartCoroutine(Type());
        } else {
            textDisplay.text="";
            continueButton.SetActive(false);

            if (!a) {
                c.HideDialog();
            } else {
                c.HideDialog2();
            }
        }
    }
}
