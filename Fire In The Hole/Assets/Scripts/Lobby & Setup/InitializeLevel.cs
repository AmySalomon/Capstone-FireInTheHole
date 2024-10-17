using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeLevel : MonoBehaviour
{
    [SerializeField] private Transform[] playerSpawns, playerScores;
    [SerializeField] private GameObject playerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        var playerConfigs = JoinPlayer.Instance.GetPlayerConfigs().ToArray();
        for (int i = 0; i < playerConfigs.Length; i++)
        {
            //only show player scoreboard if a player is assined to it
            playerScores[i].gameObject.SetActive(true);
            var player = Instantiate(playerPrefab, playerSpawns[i].position, playerSpawns[i].rotation, gameObject.transform);
            player.GetComponentInChildren<PlayerInputHandler>().InitializePlayer(playerConfigs[i]);
            player.GetComponentInChildren<PlayerScore>().myScore = playerScores[i];
        }
    }

}
