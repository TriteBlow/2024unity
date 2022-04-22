using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject fillPrefab;
    [SerializeField] Transform[] allCells;
    [SerializeField] GameObject GameOverPanel;
    [SerializeField] GameObject restartButton;
    public static Action<String> slide;
    public static Action checkDeath;
    public static GameController instance;
    public Color[] colors;
    private int GameOverCount = 0;

    public void fill()
    {
        float chance = UnityEngine.Random.Range(0f,1f);
        int whichSpawn = UnityEngine.Random.Range(0, allCells.Length);
        bool allfilled = true;
        for(int i=0;i<allCells.Length;i++)
        {
            if (allCells[i].childCount == 0) 
            {
                allfilled = false;
                break;
            }
        }
        if (allfilled)
            return;
        if(allCells[whichSpawn].childCount != 0)
        {
            fill();
            return;
        }
        if(chance < .7f)
        {
            GameObject temp = Instantiate(fillPrefab, allCells[whichSpawn]);
            Text fillText = temp.transform.GetChild(0).gameObject.GetComponent<Text>();
            fillText.text = "2";
        }
        else 
        {
            GameObject temp = Instantiate(fillPrefab, allCells[whichSpawn]);
            Text fillText = temp.transform.GetChild(0).gameObject.GetComponent<Text>();
            fillText.text = "4";
            Debug.Log(4);
        }
    }
    private void fillColor() 
    {
        foreach (Transform i in allCells)
        {
            if (i.childCount != 0)
            {
                Text myValue = i.GetChild(0).GetChild(0).gameObject.GetComponent<Text>();
                int colorIndex = getColorIndex(myValue.text);
                Color fillColor = colors[colorIndex];
                i.GetChild(0).GetComponent<Image>().color = fillColor;
            }     
        }     
    }
    private int getColorIndex(String myValue)
    {
        int index = 0;
        int value = int.Parse(myValue);
        while(value / 2 != 1){
            index += 1;
            value /= 2;
        }
        return index;
    }

    private void gameOver()
    {
        GameOverPanel.SetActive(true);
    }

    private void restart()
    {
        SceneManager.LoadScene(0);
    }
    
    public void gameOverCheck()
    {
        GameOverCount++;
        if(GameOverCount == allCells.Length) 
            gameOver();
    }
    void Start()
    {
        instance = this;
        GameOverPanel.SetActive(false);
        Button btn = restartButton.GetComponent<Button>();
        btn.onClick.AddListener(restart);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GameOverCount = 0;
            fill();
            checkDeath();

        }
        if(Input.GetKeyDown(KeyCode.A))
        {
            GameOverCount = 0;
            slide("a");
            checkDeath();
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            GameOverCount = 0;
            slide("s");
            checkDeath();
        } 
        if(Input.GetKeyDown(KeyCode.W))
        {
            GameOverCount = 0;
            slide("w");
            checkDeath();
        } 
        if(Input.GetKeyDown(KeyCode.D))
        {
            GameOverCount = 0;
            slide("d");
            checkDeath();
        }
        fillColor();
    }
}
