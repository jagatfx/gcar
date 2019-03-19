using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
 
public class CSVParsing : MonoBehaviour
{
public TextAsset csvFile; // Reference of CSV file
public float scaleFactor;
private char lineSeperator = '\n'; // It defines line seperate character
private char fieldSeperator = ','; // It defines field seperate chracter
private string[] records;
private int curIndex = 1;
public Text obsTime;
void Start ()
{
readData ();
}
// Read data from CSV file
private void readData(){
    records = csvFile.text.Split(lineSeperator);
}

private void Update(){
    string[] row = records[curIndex].Split(fieldSeperator);
    //Debug.Log("Testing rows " + row[1]);
    transform.position = new Vector3(float.Parse(row[1]) * scaleFactor, 
    float.Parse(row[2]) * scaleFactor, float.Parse(row[3]) * scaleFactor);
    //Time.timeScale = 10f;
    obsTime.text = row[0];
    curIndex = (curIndex + 5) % records.Length;
    
}
 
}