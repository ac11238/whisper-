using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypingEffect : MonoBehaviour
{
    public float typingSpeed = 0.05f;
    private bool isTyping;

    public void StartTyping(TextMeshProUGUI uiText, string message)
    {
        if (!isTyping)
        {
            StartCoroutine(TypeText(uiText, message));
        }
    }

    private IEnumerator TypeText(TextMeshProUGUI uiText, string textToType)
    {
        isTyping = true;
        uiText.text = "";
        foreach (char letter in textToType)
        {
            uiText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
    }

    public void StopTyping(TextMeshProUGUI uiText)
    {
        StopAllCoroutines();
        uiText.text = "";
        isTyping = false;
    }
}
