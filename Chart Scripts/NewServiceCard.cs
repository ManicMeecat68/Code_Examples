using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class NewServiceCard : MonoBehaviour
{
    public int ServiceID;
    public Text ServiceName;
    public Text Benefit;
    public Text StageA;
    public Image StageAColour;
    public Text StageB;
    public Image StageBColour;
    public Text StageC;
    public Image StageCColour;
    public Text StageD;
    public Image StageDColour;
    public Text StageE;
    public Image StageEColour;

    public void GenerateServiceInfo(int serviceID ,string serviceName, float benefit, int stageA, Color stageAColour, int stageB, Color stageBColour, int stageC, Color stageCColour, int stageD, Color stageDColour, int stageE, Color stageEColour)
    {
        ServiceName.text = serviceName;
        ServiceID = serviceID;
        Benefit.text = benefit.ToString("$000");

        if (StageA.text.Length <= 4 && stageA >= 0 && stageAColour != Color.clear && stageA > 0)
        {
            StageA.text = "Day " + stageA;
            StageAColour.color = stageAColour;
        }
        if (StageB.text.Length <= 4 && stageB >= 0 && stageBColour != Color.clear && stageB > 0)
        {
            StageB.text = "Day " + stageB;
            StageBColour.color = stageBColour;
        }
        if (StageC.text.Length <= 4 && stageC >= 0 && stageCColour != Color.clear && stageC > 0)
        {
            StageC.text = "Day " + stageC;
            StageCColour.color = stageCColour;            
        }
        if (StageD.text.Length <= 4 && stageD >= 0 && stageDColour != Color.clear && stageD > 0)
        {
            StageD.text = "Day " + stageD;
            StageDColour.color = stageDColour;
            Debug.Log(stageD);
        }
        if (StageE.text.Length <= 4 && stageE >= 0 && stageEColour != Color.clear && stageE > 0)
        {
            StageE.text = "Day " + stageE;
            StageEColour.color = stageEColour;
        }
    }
}
