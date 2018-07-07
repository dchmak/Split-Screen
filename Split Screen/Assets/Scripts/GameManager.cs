/*
* Created by Daniel Mak
*/

using System;
using System.Collections;
using TMPro;
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
        public Player player;
        public SpawnManager spawn;
        public Popup popup;
    }

    public event Action ReadyToStartEvent;
    [HideInInspector] public bool canShoot = false;

    public PlayerInfo[] playersInfo;
    [Space]
    public int startCountDown = 3;
    public TextMeshProUGUI text;

    private bool readyToStart = false;
    private float deadPlayer = 0f;

    private void Update() {
        if (!readyToStart) {
            CheckIsReady();
            if (readyToStart) {
                if (ReadyToStartEvent != null) {
                    StartCoroutine(CountDown(startCountDown));
                    ReadyToStartEvent();
                }
            }
        }

        int alive = playersInfo.Length;
        Popup alivePopup = null;
        foreach (PlayerInfo info in playersInfo) {
            if (info.player.health.IsDead()) {
                info.player.movement.enabled = false;
                info.player.shoot.enabled = false;
                info.popup.Display("Dead");
                alive--;
            } else {
                alivePopup = info.popup;
            }
        }
        if (alive == 1 && alivePopup != null) {
            alivePopup.Display("Win");
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

    private IEnumerator CountDown(int count) {
        while(count > 0) {
            text.text = count.ToString();
            count--;
            yield return new WaitForSeconds(1);
        }

        text.text = "";
        canShoot = true;
    }

    private void OnValidate() {
        name = "[Game Manager]";
    }
}