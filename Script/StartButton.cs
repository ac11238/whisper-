using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    private void Start()
    {
        Button button = GetComponent<Button>();

        button.onClick.AddListener(OpenGameScene);
    }

    public void OpenGameScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
