using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PositionController : MonoBehaviour
{
    public Text obsTimeText;
    public CSVParsing csvDataProvider;
    public LineRenderer orbitLineRenderer;

    public float positionScaleFactor = 0.001f;
    public int recordSkipValue = 5;
    public int orbitPositionInterval = 100;
    //public float speed = 1.0f;

    private int curIndex = 1;

    void Start()
    {
    }

    void AddPointToOrbit(Vector3 newPosition)
    {
        Vector3[] orbitPositionsArray = new Vector3[orbitLineRenderer.positionCount];
        orbitLineRenderer.GetPositions(orbitPositionsArray);
        Vector3[] updatedOrbitPositionsArray = new Vector3[orbitPositionsArray.Length + 1];
        orbitPositionsArray.CopyTo(updatedOrbitPositionsArray, 0);
        updatedOrbitPositionsArray[orbitPositionsArray.Length] = newPosition;
        orbitLineRenderer.positionCount = updatedOrbitPositionsArray.Length;
        orbitLineRenderer.SetPositions(updatedOrbitPositionsArray);
    }

    void Update()
    {
        try
        {
            string[] record = csvDataProvider.GetRecord(curIndex);
            if (record.Length == 4)
            {
                obsTimeText.text = record[0];
                Vector3 newPosition = new Vector3(float.Parse(record[1]),
                    float.Parse(record[2]), float.Parse(record[3])) * positionScaleFactor;

                // add a position to the orbit LineRenderer after an interval 
                // to build up the orbit as the object goes through first orbit
                if (curIndex > orbitLineRenderer.positionCount * orbitPositionInterval)
                {
                    AddPointToOrbit(newPosition);
                }

                transform.position = newPosition;

                // this is an example of interpolating between time stamps towards a target position
                // but really only makes sense when you have a few updates between target position
                // updates
                // comment out the transform update adove and uncomment speed when experimenting
                //transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * speed);
            }

            curIndex = (curIndex + recordSkipValue) % csvDataProvider.GetNumRecords();
            if (curIndex == 0)
            {
                curIndex = 1;
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error fetching record " + curIndex + " " + ex);
        }
    }
}
