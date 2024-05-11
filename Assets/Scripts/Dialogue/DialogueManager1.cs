using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class DialogueManager1 : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public GameObject dialoguePanel;
    public string[] sentences;
    public int index;
    public string nextScene;
    public GameObject playerImage;
    public GameObject shopkeeperImage;
    public GameObject boosImage;
    public float typingSpeed = 0.02f;

    void Awake()
    {
        playerImage.SetActive(false);
        shopkeeperImage.SetActive(false);
        boosImage.SetActive(true);
        dialogueText.color = Color.red;
        dialogueText = transform.Find("DialogueText").GetComponent<TextMeshProUGUI>();
        dialoguePanel = transform.Find("DialoguePanel").gameObject;
        StartDialogue();
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && dialoguePanel.activeInHierarchy)
        {
            UnityEngine.Debug.Log(index);
            if (dialogueText.text == sentences[index])
            {
                NextSentence();
            }
        }
    }


    public IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void StartDialogue()
    {
        dialoguePanel.SetActive(true);
        StartCoroutine(TypeSentence(sentences[index]));
    }

    public void NextSentence()
    {
        if (index < sentences.Length - 1)
        {
            index++;
            StartCoroutine(TypeSentence(sentences[index]));
        }
        else
        {
            dialogueText.text = "";
            dialoguePanel.SetActive(false);
            index = 0;
            NextScene();
        }
    }

    public void NextScene()
    {
        if (nextScene != null)
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
