using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UIPlayerText : MonoBehaviour
{
    TextMeshProUGUI tmText;

    void Awake()
    {
        tmText = GetComponent<TextMeshProUGUI>();
    }

    internal void HandlePlayerInitialized()
    {
        tmText.text = "Player Joined!";

        StartCoroutine(ClearTextAfterDelay());
    }

    IEnumerator ClearTextAfterDelay()
    {
        yield return new WaitForSeconds(2);
        tmText.text = "";
    }
}
