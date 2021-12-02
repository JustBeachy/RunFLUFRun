using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadFluf : MonoBehaviour
{
    public static string playerName;
    public static int flufID;
    public Text scoreBoard;
    public static float score=0;
    public static bool start = false;
    public static bool clearHazards = false;
    public static List<string[]> FlufData = new List<string[]>();
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        TextAsset fileData = Resources.Load<TextAsset>("FlufGame");//System.IO.File.ReadAllText();
        string[] lines = fileData.text.Replace("\"","").Split("\n"[0]);
        for (int i = 0; i < lines.Length; i++)
        {
            FlufData.Add( (lines[i].Trim()).Split(","[0]));
        }


        
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            if(!GetComponent<AudioSource>().isPlaying)
                GetComponent<AudioSource>().Play();

            score += Time.deltaTime * 10;
            scoreBoard.text = "Score: " + score.ToString("#");
        }
        else
            GetComponent<AudioSource>().Stop();
    }
}
