using UnityEngine;
using System.Collections;
using System.IO;

public class CSVParsing : MonoBehaviour
{
    public TextAsset csvFile;

    private char lineSeperator = '\n';
    private char fieldSeperator = ',';
    private string[] records;

    private void parseRecords()
    {
        records = csvFile.text.Split(lineSeperator);
    }

    private void Start()
    {
        parseRecords();
    }

    public string[] GetRecord(int index)
    {
        try
        {
            string[] row = records[index].Split(fieldSeperator);
            return row;
        }
        catch (System.Exception ex)
        {
            Debug.LogError(ex);
            string[] row = new string[0];
            return row;
        }
    }

    public int GetNumRecords()
    {
        return records.Length;
    }
}
