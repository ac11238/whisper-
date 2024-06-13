using System.Collections;
using UnityEngine;
using TMPro;

public class bol_instruct : MonoBehaviour
{
    public Transform playerTransform;
    public float radius = 5.0f;
    public string message = "Where you are. Who you are. I do not have an answer.";
    public TextMeshProUGUI uiText; // Shared TextMeshProUGUI component
    private TypingEffect typingEffect;

    private bool isTyping = false;

    void Start()
    {
        if (uiText != null)
        {
            uiText.text = "";
        }

        // Attach TypingEffect dynamically if not set
        typingEffect = gameObject.AddComponent<TypingEffect>();
    }

    private void Update()
    {
        float distance = Vector3.Distance(playerTransform.position, transform.position);

        if (distance <= radius)
        {
            if (!isTyping && uiText != null)
            {
                isTyping = true;
                typingEffect.StartTyping(uiText, message);
            }
        }
        else
        {
            if (isTyping && uiText != null)
            {
                typingEffect.StopTyping(uiText);
                isTyping = false;
            }
        }
    }
}
