/*
* Created by Daniel Mak
*/

using UnityEngine;

public class GameManager : MonoBehaviour {

    #region Singleton
    public static GameManager instance;
    private void Awake() {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    #endregion

    [System.Serializable]
    public class PlayerInfo {
        [ReadOnly] public string index;
        public GameObject player;
        public SpawnManager spawn;
    }

    public PlayerInfo[] playersInfo;

    [HideInInspector] public bool readyToStart;

    private void Update () {
        readyToStart = true;
        foreach (PlayerInfo info in playersInfo) {
            ParticleSystem.MainModule main = info.spawn.GetComponent<ParticleSystem>().main;
            if ((info.player.transform.position - info.spawn.transform.position).magnitude > info.spawn.size) {
                print(info.player.name + " is outside the spawn!");
                readyToStart = false;
            }
        }
    }

    private void OnValidate() {
        name = "[Game Manager]";

        foreach (PlayerInfo info in playersInfo) {
            info.index = info.player.name;
        }
    }
}