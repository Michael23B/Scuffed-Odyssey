using UnityEngine;

public class NetworkPlayer : MonoBehaviour
{
    [SerializeField] private MonoBehaviour[] localPlayerScripts = null;
    public bool IsLocalPlayer { get; private set; }
    public ulong PlayerId { get; private set; }

    public void InitializePlayer(bool isLocalPlayer, ulong playerId)
    {
        IsLocalPlayer = isLocalPlayer;
        PlayerId = playerId;

        if (!isLocalPlayer)
        {
            foreach (var script in localPlayerScripts)
            {
                script.enabled = false;
            }
        }
    }

    public static NetworkPlayer CreateNetworkPlayer(bool isLocalPlayer, ulong playerId)
    {
        GameObject player = Instantiate(PrefabHelper.Instance.PlayerPrefab);
        NetworkPlayer networkPlayer = player.GetComponent<NetworkPlayer>();
        networkPlayer.InitializePlayer(isLocalPlayer, playerId);
        return networkPlayer;
    }
}
