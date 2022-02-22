using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    Player[] players;

    void Awake()
    {
        Instance = this;
        players = FindObjectsOfType<Player>();
    }

    public void AddPlayerToGame(Controller controller)
    {
        var firstNonActivePlayer = players
            .OrderBy(t => t.PlayerNumber)
            .FirstOrDefault(t => t.HasController == false);

        firstNonActivePlayer.InitializePlayer(controller);
    }

    public void SpawnPlayerCharacters()
    {
        foreach (var player in players)
        {
            if (player.HasController && player.CharacterPrefab != null)
                player.SpawnCharacter();
        }
    }
}
