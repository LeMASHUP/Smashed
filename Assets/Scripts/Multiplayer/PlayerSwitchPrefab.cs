using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSwitchPrefab : MonoBehaviour
{
    int index;
    [SerializeField] List<GameObject> players = new List<GameObject>();
    PlayerInputManager manager;

    private void Start()
    {
        int index = 0;
        manager = GetComponent<PlayerInputManager>();
        manager.playerPrefab = players[index];
    }

    public void SwitchNextCharacter(PlayerInput input)
    {
        index = 1;
        manager.playerPrefab = players[index];
    }
}
