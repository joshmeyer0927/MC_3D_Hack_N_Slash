using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICharacterSelectionPanel : MonoBehaviour
{
    [SerializeField] Character characterPrefab;

    public Character CharacterPrefab { get { return characterPrefab; } }
}
