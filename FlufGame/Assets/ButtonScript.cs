using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public GameObject startScreen, LeaderboardScreen, screenToDestroy;
    public InputField flufID, playerName;
    public GameObject player;
    public GameObject Hypno;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartPressed()
    {
        if ((flufID.text == "" && playerName.text == "")||int.Parse(flufID.text) >= 0 && int.Parse(flufID.text) < 10000)
        {
            if(flufID.text == "" && playerName.text == "")
            {
                flufID.text = "1";
                playerName.text = "Anonymous" + Random.Range(1, 10000).ToString();
            }

            LoadFluf.flufID = int.Parse(flufID.text);
            LoadFluf.playerName = playerName.text;

            if(LoadFluf.FlufData[int.Parse(flufID.text)][3]=="true")
            {
                Hypno.SetActive(true);
            }
            if (LoadFluf.FlufData[int.Parse(flufID.text)][1] == "Female")
            {
                
            }
            var spawnPlayer = Instantiate(player);
            spawnPlayer.GetComponent<PlayerMovement>().SetSkin(LoadFluf.FlufData[int.Parse(flufID.text)][2]);
            LoadFluf.start = true;
            Destroy(startScreen);
        }
    }

    public void RestartGame()
    {
        
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        LoadFluf.clearHazards=true;
        LoadFluf.score = 0;
        LoadFluf.start = true;
        var spawnPlayer = Instantiate(player);
        spawnPlayer.GetComponent<PlayerMovement>().SetSkin(LoadFluf.FlufData[LoadFluf.flufID][2]);
        Destroy(startScreen);
    }

    public void LoadLeaderboard()
    {
        Instantiate(LeaderboardScreen, transform.parent);

    }

    public void SimpleDestroy()
    {
        Destroy(screenToDestroy);
    }


}
