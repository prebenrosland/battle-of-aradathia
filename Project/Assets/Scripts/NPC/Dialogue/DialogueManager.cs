using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using AS;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public CameraHandler cameraHandler;

    public Animator animator;
    public PlayerLocomotion playerLocomotion;

    private Queue<string> sentences;
    private DialogueTrigger dialogueTrigger;

    public void StartDialogue(Dialogue dialogue, DialogueTrigger trigger)
    {
        cameraHandler.ToggleCameraRotation(false);
        animator.SetBool("IsOpen", true);

        playerLocomotion.rigidbody.velocity = Vector3.zero; //stops all movement

        nameText.text = dialogue.name;
        sentences = new Queue<string>(dialogue.sentences);

        DisplayNextSentence();
    }

    public void DisplayNextSentence () {
        if (sentences.Count == 0) {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter.ToString();
            yield return null;
        }
    }

    void EndDialogue()
    {
        cameraHandler.ToggleCameraRotation(true);
        animator.SetBool("IsOpen", false);
    }
}
