﻿using UnityEngine;

public class NetworkPlayer : MonoBehaviour
{
    [SerializeField] private MonoBehaviour[] localPlayerScripts = null;
    public Rigidbody2D RigidBody { get; private set; }
    public Player Player { get; private set; }
    public bool IsLocalPlayer { get; private set; }
    public ulong PlayerId { get; private set; }

    public static NetworkPlayer CreateNetworkPlayer(bool isLocalPlayer, ulong playerId)
    {
        GameObject player = Instantiate(PrefabHelper.Instance.PlayerPrefab);
        NetworkPlayer networkPlayer = player.GetComponent<NetworkPlayer>();
        networkPlayer.InitializePlayer(isLocalPlayer, playerId);
        return networkPlayer;
    }

    private void InitializePlayer(bool isLocalPlayer, ulong playerId)
    {
        IsLocalPlayer = isLocalPlayer;
        PlayerId = playerId;
        RigidBody = GetComponent<Rigidbody2D>();
        Player = GetComponent<Player>();
        Player.Initialize();

        if (!isLocalPlayer)
        {
            foreach (var script in localPlayerScripts)
            {
                script.enabled = false;
            }
        }
    }
}
