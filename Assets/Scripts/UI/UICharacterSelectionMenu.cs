using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UICharacterSelectionMenu : MonoBehaviour
{
    [SerializeField] UICharacterSelectionPanel leftPanel;
    [SerializeField] UICharacterSelectionPanel rightPanel;
    [SerializeField] TextMeshProUGUI startGameText;

    public UICharacterSelectionPanel LeftPanel { get { return leftPanel; } }
    public UICharacterSelectionPanel RightPanel { get { return rightPanel; } }

    UICharacterSelectionMarker[] markers;
    bool startEnabled;

    void Awake()
    {
        markers = GetComponentsInChildren<UICharacterSelectionMarker>();
    }

    void Update()
    {
        int playerCount = 0;
        int lockedCount = 0;

        foreach(var marker in markers)
        {
            if (marker.IsPlayerIn)
                playerCount++;

            if (marker.IsLockedIn)
                lockedCount++;
        }

        startEnabled = (playerCount > 0) && (playerCount == lockedCount);
        startGameText.gameObject.SetActive(startEnabled);
    }

    public void TryStartGame()
    {
        if(startEnabled)
        {
            GameManager.Instance.Begin();
            gameObject.SetActive(false);
        }
    }
}
