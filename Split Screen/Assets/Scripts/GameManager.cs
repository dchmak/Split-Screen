/*
* Created by Daniel Mak
*/

using System;
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

    public event Action ReadyToStartEvent;

    private bool readyToStart;

    private void Update () {
        if (!readyToStart) {
            CheckIsReady();
            if (readyToStart) {
                if (ReadyToStartEvent != null) ReadyToStartEvent();
            }
        }
    }

    private void CheckIsReady() {
        readyToStart = true;
        foreach (PlayerInfo info in playersInfo) {
            if ((info.player.transform.position - info.spawn.transform.position).magnitude > info.spawn.size) {
                //print(info.player.name + " is outside the spawn!");
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