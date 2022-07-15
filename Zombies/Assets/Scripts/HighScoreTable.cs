using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class HighScoreTable : MonoBehaviour
{

    private Transform entryContainer; 
    private Transform entryTemplate; 
    private List<Transform> highscoreEntryTransformList;

    private void Awake(){
        entryContainer = transform.Find("HighScoreContainer");
        entryTemplate = entryContainer.Find("HighscoreEntryTemplate");

        entryTemplate.gameObject.SetActive(false);

       AddHighScoreEntry(1000,"bruh");

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);


        for (int i = 0; i < highscores.highscoreEntryList.Count; i++) { 
            for(int j = i + 1; j < highscores.highscoreEntryList.Count; j++){
                if(highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score){
                    HighscoreEntry tmp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = tmp;
                }
            }
        }

        highscoreEntryTransformList = new List<Transform>();
        foreach(HighscoreEntry highscoreEntry in highscores.highscoreEntryList) { 
            createHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
        }

       


        
    }

    private void createHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList )
    {   
            float templateHeight = 60f;
            Transform entryTransform = Instantiate(entryTemplate, container);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
            entryTransform.gameObject.SetActive(true);
            

            int rank = transformList.Count + 1; 
            string rankString = "";
            switch(rank){
                default: 
                    rankString = rank + "TH"; break;

                case 1: rankString = "1ST"; break; 
                case 2: rankString = "2ND"; break;
                case 3: rankString = "3RD"; break;
            }

            int score = highscoreEntry.score;
            string name = highscoreEntry.name;


            entryTransform.Find("PositionText").GetComponent<Text>().text = rankString;
            entryTransform.Find("ScoreText").GetComponent<Text>().text = score.ToString();
            entryTransform.Find("NameText").GetComponent<Text>().text = name;

            transformList.Add(entryTransform);

    }

    private void AddHighScoreEntry(int score, string name){
        HighscoreEntry highscoreEntry = new HighscoreEntry {score = score, name = name};

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        highscores.highscoreEntryList.Add(highscoreEntry);

        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }

    private class Highscores {
        public List<HighscoreEntry> highscoreEntryList;
    }

    [System.Serializable]
    private class HighscoreEntry {
        public int score; 
        public string name;
    }

    
    
}
