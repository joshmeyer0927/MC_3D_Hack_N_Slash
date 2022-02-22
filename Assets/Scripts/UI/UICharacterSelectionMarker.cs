using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterSelectionMarker : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Image markerImage;
    [SerializeField] Image lockImage;

    UICharacterSelectionMenu menu;
    bool initializing;
    bool initialized;

    public bool IsLockedIn { get; private set; }
    public bool IsPlayerIn { get { return player.HasController; } }

    void Awake()
    {
        menu = GetComponentInParent<UICharacterSelectionMenu>();

        markerImage.gameObject.SetActive(false);
        lockImage.gameObject.SetActive(false);
    }

    void Update()
    {
        if (IsPlayerIn == false)
            return;

        if (!initializing)
            StartCoroutine(Initialize());

        if (!initialized)
            return;

        if (!IsLockedIn)
        {
            if (player.Controller.horizontal > .5)
                MoveToCharacterPanel(menu.RightPanel);
            else if (player.Controller.horizontal < -.5)
                MoveToCharacterPanel(menu.LeftPanel);

            if (player.Controller.attackPressed)
            {
                StartCoroutine(LockCharacter());
            }
        }
        else
        {
            if(player.Controller.attackPressed)
            {
                menu.TryStartGame();
            }
        }
    }

    IEnumerator LockCharacter()
    {
        lockImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(.2f);

        IsLockedIn = true;
    }

    void MoveToCharacterPanel(UICharacterSelectionPanel panel)
    {
        transform.position = panel.transform.position;
        player.CharacterPrefab = panel.CharacterPrefab;
    }

    IEnumerator Initialize()
    {
        initializing = true;
        MoveToCharacterPanel(menu.LeftPanel);

        yield return new WaitForSeconds(.5f);
        markerImage.gameObject.SetActive(true);
        initialized = true;
    }
}
