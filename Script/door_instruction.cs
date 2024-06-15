using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class door_instruction : MonoBehaviour
{
    public Transform playerTransform;
    public float radius = 5.0f;
    public string message = "Middle for the norm, left for the unstable, right for the stable";
    public TextMeshProUGUI uiText; 
    private TypingEffect typingEffect;

    private bool isTyping = false;

    void Start()
    {
        if (uiText != null)
        {
            uiText.text = "";
        }

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
