using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.IO;
using System;

public class Leaderboard : MonoBehaviour
{
    public GameObject content, scoreEntry;
    public bool postScore = false;

    private void Start()
    {
        if (postScore)
            GetComponent<Leaderboard>().PostScores(LoadFluf.playerName, (int)Math.Ceiling(LoadFluf.score));
        else
            GetComponent<Leaderboard>().RetrieveScores();
    }

    


    private const string highscoreURL = "https://rationalistic-swall.000webhostapp.com/leaderboard.php";

    public List<Score> RetrieveScores()
    {
        List<Score> scores = new List<Score>();
        StartCoroutine(DoRetrieveScores(scores));
        return scores;
    }

    public void PostScores(string name, int score)
    {
        StartCoroutine(DoPostScores(name, score));
    }

    IEnumerator DoRetrieveScores(List<Score> scores)
    {
        WWWForm form = new WWWForm();
        form.AddField("retrieve_leaderboard", "true");

        using (UnityWebRequest www = UnityWebRequest.Post(highscoreURL, form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
               
                string contents = www.downloadHandler.text;
                using (StringReader reader = new StringReader(contents))
                {
                    string line;
                    int count = 1;
                    while ((line = reader.ReadLine()) != null)
                    {
                        Score entry = new Score();
                        entry.name = line;
                        try
                        {
                            Debug.Log("Successfully retrieved scores!");
                            entry.score = Int32.Parse(reader.ReadLine());
                        }
                        catch (Exception e)
                        {
                            Debug.Log("Invalid score: " + e);
                            continue;
                        }

                        
                        scores.Add(entry);
                        GameObject scoreContent = Instantiate(scoreEntry, content.transform);
                        scoreContent.GetComponent<ScoreListing>().playerName.text = entry.name;
                        scoreContent.GetComponent<ScoreListing>().score.text = entry.score.ToString();
                        scoreContent.GetComponent<ScoreListing>().rank.text = "#"+count;
                        if (entry.name == LoadFluf.playerName)
                        {
                            scoreContent.GetComponent<Image>().color = Color.yellow;
                        }
                        count++;
                    }
                }
            }
        }
    }

    IEnumerator DoPostScores(string name, int score)
    {
        WWWForm form = new WWWForm();
        form.AddField("post_leaderboard", "true");
        form.AddField("name", name);
        form.AddField("score", score);

        using (UnityWebRequest www = UnityWebRequest.Post(highscoreURL, form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Successfully posted score!");
            }
        }
    }
}

public struct Score
{
    public string name;
    public int score;
}
