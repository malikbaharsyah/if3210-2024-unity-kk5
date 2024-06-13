using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
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
        playerImage.SetActive(true);
        shopkeeperImage.SetActive(false);
        boosImage.SetActive(false);
        dialogueText = transform.Find("DialogueText").GetComponent<TextMeshProUGUI>();
        dialoguePanel = transform.Find("DialoguePanel").gameObject;
        StartDialogue();
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && dialoguePanel.activeInHierarchy)
        {
            if (dialogueText.text == sentences[index])
            {
                NextSentence();
            }
        }
    }


    public IEnumerator TypeSentence(string sentence)
    {
        if (index == 3 || index == 4)
        {
            playerImage.SetActive(false);
            shopkeeperImage.SetActive(true);
            dialogueText.color = Color.yellow;
        } else
        {
            playerImage.SetActive(true);
            shopkeeperImage.SetActive(false);
            dialogueText.color = Color.black;
        }
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
