using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameMaster : MonoBehaviour {
    
    public static GameMaster gm;

    void Start() {
        if(gm == null) {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        }
        respawnAudio = GetComponent<AudioSource>();
    }

    public Transform playerPrefab;
    public Transform spawnPoint;
    public int spawnDelay = 2;
    public Transform spawnPrefab;
    public AudioSource respawnAudio;



    public IEnumerator RespawnPlayer () {
        respawnAudio.Play();
        yield return new WaitForSeconds(spawnDelay);
        
        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        Transform clone = Instantiate(spawnPrefab, spawnPoint.position, spawnPoint.rotation);
        Destroy(clone.gameObject,3f);
    }


    public static void KillPlayer(Player player) {
        Destroy(player.gameObject);
        gm.StartCoroutine(gm.RespawnPlayer());
    }
    public static void KillEnemy(Enemy enemy)
    {
        Destroy(enemy.gameObject);
    }
}
