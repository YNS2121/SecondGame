using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject diyalogButton;
    [SerializeField] private GameObject devamButton;
    [SerializeField] private GameObject diyalogbalonu;
    public bool oyundurdumu = false;
    public Button _diyalogButton;
    public Button _devamButton;

    public Queue<string> sentences;

    void Start()
    {
        oyunudurdur();

        _diyalogButton.onClick.AddListener(ShowDevamButton);
        sentences = new Queue<string>();
    }

    void ShowDevamButton()
    {
        _devamButton.gameObject.SetActive(true);
    }


    public void StartDialogue(Dialogue dialogue)
    {  
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        
        DisplayNextSentence();
    }


    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    void EndDialogue()
    {
        Destroy(diyalogbalonu);
        Destroy(devamButton);
        Destroy(diyalogButton);
        oyunudurdur();
        //Debug.Log(oyundurdumu);
    }

    void oyunudurdur()
    {
        if (oyundurdumu == true)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0f;
            oyundurdumu = true;
            
        }
    }
}
