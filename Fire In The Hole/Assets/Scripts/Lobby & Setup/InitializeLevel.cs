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

        for(int i = 0; i<playerScores.Length; i++)//turn off player leaderboard UI so only the correct ones get turned on
        {
            playerScores[i].gameObject.SetActive(false);
        }
        var playerConfigs = JoinPlayer.Instance.GetPlayerConfigs().ToArray();
        for (int i = 0; i < playerConfigs.Length; i++)
        {
            //only show player scoreboard if a player is assined to it
            playerScores[i].gameObject.SetActive(true);
            playerScores[i].gameObject.GetComponent<ScoreTracker>().HideScoreLeader(); 
            GameTimer.gameTimer.playerScoreboards[i] = playerScores[i];
            LeaderboardManager.leaderboardManager.playerScoreboards[i] = playerScores[i];
            var player = Instantiate(playerPrefab, playerSpawns[i].position, playerSpawns[i].rotation, gameObject.transform);
            player.GetComponentInChildren<PlayerInputHandler>().InitializePlayer(playerConfigs[i]);
            player.GetComponentInChildren<PlayerScore>().myScore = playerScores[i];
            player.GetComponentInChildren<PlayerScore>().mySprite = playerConfigs[i].PlayerSprite;
            player.GetComponentInChildren<PlayerScore>().SetSprite();
            player.GetComponentInChildren<PlayerScore>().HideScoreLeader();
            player.GetComponentInChildren<scr_meleeSwing>().outlineColor = playerConfigs[i].PlayerColor;
            player.GetComponentInChildren<PlayerConfigInfo>().GetPlayerConfig(playerConfigs[i]);
            LeaderboardManager.leaderboardManager.players[i] = player;
            GameTimer.gameTimer.players[i] = player;

        }
    }

}
