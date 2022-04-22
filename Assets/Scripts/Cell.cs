using System;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    [SerializeField] Cell up;
    [SerializeField] Cell down;
    [SerializeField] Cell left;
    [SerializeField] Cell right;
    [SerializeField] GameObject fillPrefab;

    private void slideUp() 
    {
        Cell currentCell = this;
        Cell nextCell = currentCell.up;
        if (SpawnFilled(currentCell))
        {
            if (nextCell != null){
                nextCell.slideUp();
                if(SpawnFilled(nextCell) && SpawnFilled(currentCell))
                {
                    tryCombine(nextCell, currentCell);
                }
                else 
                {
                    GameObject temp = Instantiate(fillPrefab, nextCell.transform);
                    temp.transform.GetChild(0).gameObject.GetComponent<Text>().text 
                        = currentCell.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text;
                    DestroyImmediate(currentCell.transform.GetChild(0).gameObject);    
                }
                nextCell.slideUp();
            }
        }
    }

    private void slideDown()
    {
        Cell currentCell = this;
        Cell nextCell = currentCell.down;
        if (SpawnFilled(currentCell))
        {
            if(nextCell != null)
            {
                nextCell.slideDown();
                if(SpawnFilled(nextCell) && SpawnFilled(currentCell))
                {
                    tryCombine(nextCell, currentCell);
                }
                else 
                {
                    GameObject temp = Instantiate(fillPrefab, nextCell.transform);
                    temp.transform.GetChild(0).gameObject.GetComponent<Text>().text 
                        = currentCell.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text;
                    DestroyImmediate(currentCell.transform.GetChild(0).gameObject);    
                }
                nextCell.slideDown();
            }
        }
    }

    private void slideLeft()
    {
        Cell currentCell = this;
        Cell nextCell = currentCell.left;
        if (SpawnFilled(currentCell))
        {
            if(nextCell != null)
            {
                nextCell.slideLeft();
                if(SpawnFilled(nextCell) && SpawnFilled(currentCell))
                {
                    tryCombine(nextCell, currentCell);
                }
                else 
                {
                    GameObject temp = Instantiate(fillPrefab, nextCell.transform);
                    temp.transform.GetChild(0).gameObject.GetComponent<Text>().text 
                        = currentCell.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text;
                    DestroyImmediate(currentCell.transform.GetChild(0).gameObject);    
                }
                nextCell.slideLeft();
            }
        }
    }

    private void slideRight()
    {
        Cell currentCell = this;
        Cell nextCell = currentCell.right;
        if (SpawnFilled(currentCell))
        {
            if(nextCell != null){
                nextCell.slideRight();
                if (SpawnFilled(nextCell) && SpawnFilled(currentCell))
                {
                    tryCombine(nextCell, currentCell);
                }
                else {
                    GameObject temp = Instantiate(fillPrefab, nextCell.transform);
                    temp.transform.GetChild(0).gameObject.GetComponent<Text>().text 
                        = currentCell.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text;
                    DestroyImmediate(currentCell.transform.GetChild(0).gameObject);    
                }
                nextCell.slideRight();
            }
        }
    }

    private bool SpawnFilled(Cell cell)
    {
        return cell.transform.childCount != 0;
    }
    private void tryCombine(Cell combineIn, Cell combineOut)
    {
        Text Celltext_in = combineIn.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>();
        Text Celltext_out = combineOut.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>();
        if(Celltext_in.text.Equals(Celltext_out.text))
        {
            Celltext_in.text = (int.Parse(Celltext_out.text) + int.Parse(Celltext_in.text)).ToString();
            DestroyImmediate(combineOut.transform.GetChild(0).gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable() 
    {
        GameController.slide += OnSlide;
        GameController.checkDeath += CheckDeath;
    }

    private void OnDisable() 
    {
        GameController.slide -= OnSlide;
        GameController.checkDeath -= CheckDeath;
    }
    
    private void OnSlide(string command)
    {
        if(command.Equals("w"))
            slideUp();
        else if(command.Equals("s"))
            slideDown();
        else if(command.Equals("a"))
            slideLeft();
        else if(command.Equals("d"))
            slideRight();
    }
    private void CheckDeath() 
    {
        if(this.up != null)
        {
            if(this.transform.childCount == 0 || this.up.transform.childCount == 0)
                return;
            if(this.up.transform.childCount != 0) 
            {
                String thisValue = this.transform.GetChild(0).GetChild(0).GetComponent<Text>().text;
                String upValue = this.up.transform.GetChild(0).GetChild(0).GetComponent<Text>().text;
                if(thisValue.Equals(upValue))
                    return;
            }   
        }
        if(this.down != null)
        {
            if(this.transform.childCount == 0 || this.down.transform.childCount == 0)
                return;
            if(this.down.transform.childCount != 0) 
            {
                String thisValue = this.transform.GetChild(0).GetChild(0).GetComponent<Text>().text;
                String upValue = this.down.transform.GetChild(0).GetChild(0).GetComponent<Text>().text;
                if(thisValue.Equals(upValue))
                    return;
            }   
        }
        if(this.left != null)
        {
            if(this.transform.childCount == 0 || this.left.transform.childCount == 0)
                return;
            if(this.left.transform.childCount != 0) 
            {
                String thisValue = this.transform.GetChild(0).GetChild(0).GetComponent<Text>().text;
                String upValue = this.left.transform.GetChild(0).GetChild(0).GetComponent<Text>().text;
                if(thisValue.Equals(upValue))
                    return;
            }   
        }
        if(this.right != null)
        {
            if(this.transform.childCount == 0 || this.right.transform.childCount == 0)
                return;
            if(this.right.transform.childCount != 0) {
                String thisValue = this.transform.GetChild(0).GetChild(0).GetComponent<Text>().text;
                String upValue = this.right.transform.GetChild(0).GetChild(0).GetComponent<Text>().text;
                if(thisValue.Equals(upValue))
                    return;
            }   
        }
        GameController.instance.gameOverCheck();
    }
}
