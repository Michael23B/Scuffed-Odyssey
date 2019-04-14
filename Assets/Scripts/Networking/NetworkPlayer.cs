using UnityEngine;

public class NetworkPlayer : MonoBehaviour
{
    [SerializeField] private MonoBehaviour[] localPlayerScripts = null;
    private bool isLocalPlayer;
    private int playerId; // TODO may not be an int

    public void InitializePlayer(bool isLocalPlayer, int playerId)
    {
        this.isLocalPlayer = isLocalPlayer;

        if (!isLocalPlayer)
        {
            foreach (var script in localPlayerScripts)
            {
                script.enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
