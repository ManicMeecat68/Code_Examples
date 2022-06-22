using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ITPerformanceChart : MonoBehaviour
{
    [Header("Round 1")]
    public Text NS1;
    public Text NR1;
    public Text DF1;
    public Text DR1;
    public Text FD1;
    public Text CI1;
    public Text PC1;
    public Text ICBD1;
    public Text A1;
    //public Text I1;
    public Text MTRS1;
    public Text RI1;
    public Text B1;
    public Text O1;
    public Text SI1;
    public Text AR1;

    [Header("Round 2")]
    public Text NS2;
    public Text NR2;
    public Text DF2;
    public Text DR2;
    public Text FD2;
    public Text CI2;
    public Text PC2;
    public Text ICBD2;
    public Text A2;
    //public Text I2;
    public Text MTRS2;
    public Text RI2;
    public Text B2;
    public Text O2;
    public Text SI2;
    public Text AR2;

    [Header("Round 3")]
    public Text NS3;
    public Text NR3;
    public Text DF3;
    public Text DR3;
    public Text FD3;
    public Text CI3;
    public Text PC3;
    public Text ICBD3;
    public Text A3;
    //public Text I3;
    public Text MTRS3;
    public Text RI3;
    public Text B3;
    public Text O3;
    public Text SI3;
    public Text AR3;

    [Header("Round 4")]
    public Text NS4;
    public Text NR4;
    public Text DF4;
    public Text DR4;
    public Text FD4;
    public Text CI4;
    public Text PC4;
    public Text ICBD4;
    public Text A4;
    //public Text I4;
    public Text MTRS4;
    public Text RI4;
    public Text B4;
    public Text O4;
    public Text SI4;
    public Text AR4;

    public List<float> availability = new List<float>();

    private List<int> incidents = new List<int>();

    public int round = 1;
    public int avilLength;
    public int facBugs;
    public float facCodes;
    public int facSecurity;
    public int facAccess;
    public int platConflicts;
    private int lastInt;
    private int releases;

    public float availAdd;

    private float temp;
    public float startingServices;
    public int startingSegments;
    private float deplyedSegments;
    private int servicesActive;
    public float developedServices;    

    private float servInactiveTime;
    private float outages;
    private float servicesNotActive;
    private float capacityOutages;

    private float prevFacs;
    private float prevFacAccess;
    private float prevFacBugs;
    private float prevBugs;
    private float prevBadCodes;
    private float prevGameBadCodes;
    private float prevCapacities;
    private float prevFacSecure;
    private float prevNewReleases;
    private float prevNewServices;
    private float prevRepeatIncidents;
    private float prevOutages;
    private float prevActive;
    private float prevInActive;
    private int prevReleases;
    private int prevServReleases;
    private int prevInstalls;
    private int prevCapUpgrades;
    private int prevPlatConflicts;

    public void SetText()
    {

        temp = 0;

        switch (round)
        {
            case 1:                

                foreach (ServiceCard service in FindObjectsOfType<ServiceCard>())
                {
                    deplyedSegments += service.developedSegments;
                    releases += service.releases;
                    if (service.developed)
                    {
                        developedServices += 1;
                    }
                    servicesActive += service.timeActive;
                    servicesNotActive += service.timeInactive;                   
                    outages += service.outages;
                    capacityOutages += service.capacityOutage;
                    platConflicts += service.platConflicts;
                }

                prevPlatConflicts = platConflicts;

                NS1.text = (developedServices - startingServices).ToString();

                NR1.text = FindObjectOfType<ControlHandler>().GetComponent<ControlHandler>().releases.ToString(); 

                if ((FindObjectOfType<ControlHandler>().GetComponent<ControlHandler>().releases) > 0)
                {
                    DF1.text = ((28 * 24) / (FindObjectOfType<ControlHandler>().GetComponent<ControlHandler>().releases)).ToString("00.0/Hours");
                }
                else
                {
                    DF1.text = "00.0/Hours";
                }               

                temp = FindObjectOfType<ControlHandler>().segBugs + FindObjectOfType<GameplayHandler>().badCodes;                

                deplyedSegments = (deplyedSegments + FindObjectOfType<ControlHandler>().badCodes) - startingSegments;
                if (releases != 0)
                {
                    temp = ((FindObjectOfType<ControlHandler>().bugs  / FindObjectOfType<ControlHandler>().releases) * 100);
                    DR1.text = temp.ToString("00.0");
                   
                    temp = FindObjectOfType<ControlHandler>().badCodes + FindObjectOfType<GameplayHandler>().serverCapacities + FindObjectOfType<ControlHandler>().GetComponent<ControlHandler>().securities;
                    temp = (temp / FindObjectOfType<ControlHandler>().GetComponent<ControlHandler>().installs) * 100;
                    if (temp <= 100)
                    {
                        FD1.text = temp.ToString("00.0");
                    }
                    else
                    {
                        FD1.text = "00.0";
                    }
                    temp = (FindObjectOfType<GameplayHandler>().badCodes + FindObjectOfType<GameplayHandler>().serverCapacities + FindObjectOfType<ControlHandler>().bugs);

                    if (facCodes != 0)
                    {
                        ICBD1.text = (temp / (temp + facCodes) * 100).ToString("00.0");
                    }
                    else if (temp != 0)
                    {
                        ICBD1.text = "100.0";
                    }
                    else
                    {
                        ICBD1.text = "00.0";
                    }
                }
                else
                {
                    DR1.text = "00.0";
                    FD1.text = "00.0";
                    ICBD1.text = "00.0";
                }

                CI1.text = capacityOutages.ToString();
                PC1.text = platConflicts.ToString();

                foreach (float available in availability)
                {
                    avilLength += 1;
                    availAdd += available;
                }
                temp = (Mathf.Round(availAdd / avilLength) * 10) / 10;
                A1.text = temp.ToString("00.0");
                //I1.text = string.Format("({0} / {1})", (FindObjectOfType<GameplayHandler>().serverCapacities + FindObjectOfType<ControlHandler>().bugs), facCodes);

                if (servicesNotActive != 0)
                {
                    temp = 0;
                    if (FindObjectOfType<GameplayHandler>().outages != 0)
                    {
                        temp = servicesNotActive / (FindObjectOfType<GameplayHandler>().outages - prevOutages);
                        MTRS1.text = temp.ToString("00.0 Hours");
                    }
                    else
                    {
                        MTRS1.text = "672 Hours";
                    }
                }
                else
                {
                    MTRS1.text = "00.0 Hours";
                }
                prevOutages = Mathf.RoundToInt(FindObjectOfType<GameplayHandler>().outages);
                incidents = FindObjectOfType<ControlHandler>().GetComponent<ControlHandler>().incidents;
                incidents.Sort();
                temp = 0;
                foreach (int incident in incidents)
                {
                    if (lastInt != incident)
                    {
                        lastInt = incident;
                    }
                    else if (lastInt == incident)
                    {
                        temp += 1;
                    }
                }
                RI1.text = temp.ToString();
                prevRepeatIncidents = Mathf.RoundToInt(temp);

                B1.text = string.Format("({0} / {1})", FindObjectOfType<ControlHandler>().bugs, facBugs);

                O1.text = (outages + capacityOutages).ToString();

                SI1.text = facSecurity.ToString();

                AR1.text = facAccess.ToString();

                foreach (ServiceCard service in FindObjectsOfType<ServiceCard>())
                {
                    service.outages = 0;
                }

                PrevRound();
                break;
            case 2:

                Clear();

                foreach (ServiceCard service in FindObjectsOfType<ServiceCard>())
                {
                    deplyedSegments += service.developedSegments;
                    releases += service.releases;
                    if (service.developed)
                    {
                        developedServices += 1;
                    }
                    servicesActive += service.timeActive;
                    servicesNotActive += service.timeInactive;
                    outages += service.outages;
                    capacityOutages += service.capacityOutage;
                    platConflicts += service.platConflicts;
                }

                prevPlatConflicts = platConflicts;

                NS2.text = (developedServices - startingServices).ToString();

                NR2.text = FindObjectOfType<ControlHandler>().GetComponent<ControlHandler>().releases.ToString();

                if ((FindObjectOfType<ControlHandler>().GetComponent<ControlHandler>().releases) > 0)
                {
                    DF2.text = ((28 * 24) / (FindObjectOfType<ControlHandler>().GetComponent<ControlHandler>().releases)).ToString("00.0/Hours");
                }
                else
                {
                    DF2.text = "00.0/Hours";
                }

                //temp = FindObjectOfType<ControlHandler>().segBugs + FindObjectOfType<GameplayHandler>().GetComponent<GameplayHandler>().badCodes;
                deplyedSegments = (deplyedSegments + FindObjectOfType<ControlHandler>().badCodes) - startingSegments;
                if (releases != 0)
                {
                    DR2.text = ((FindObjectOfType<ControlHandler>().bugs  / FindObjectOfType<ControlHandler>().releases) * 100).ToString("00.0");

                    temp = FindObjectOfType<ControlHandler>().badCodes + FindObjectOfType<GameplayHandler>().serverCapacities;

                    temp = (temp / (FindObjectOfType<ControlHandler>().GetComponent<ControlHandler>().installs)) * 100;

                    if (developedServices - startingServices != 0 && temp <= 100)
                    {                        
                    FD2.text = temp.ToString("00.0");
                    }
                    else
                    {
                        FD2.text = "00.0";
                    }
                    temp = (FindObjectOfType<GameplayHandler>().badCodes + FindObjectOfType<GameplayHandler>().serverCapacities + FindObjectOfType<ControlHandler>().bugs);
                    if (facCodes != 0)
                    {
                        ICBD2.text = (temp / (temp + facCodes) * 100).ToString("00.0");
                    }
                    else if (temp != 0)
                    {
                        ICBD2.text = "100.0";
                    }
                    else
                    {
                        ICBD2.text = "00.0";
                    }
                }
                else
                {
                    DR2.text = "00.0";
                    FD2.text = "00.0";
                    ICBD2.text = "00.0";
                }

                CI2.text = capacityOutages.ToString();
                PC2.text = platConflicts.ToString();

                foreach (float available in availability)
                {
                    avilLength += 1;
                    availAdd += available;
                }
                temp = (Mathf.Round(availAdd / avilLength) * 10) / 10;
                A2.text = temp.ToString("00.0");
                
                //I2.text = string.Format("({0} / {1})", FindObjectOfType<GameplayHandler>().serverCapacities + FindObjectOfType<ControlHandler>().bugs, facCodes);

                servicesNotActive -= prevInActive;

                if (servicesNotActive != 0)
                {
                    temp = 0;
                    if (FindObjectOfType<GameplayHandler>().outages != 0)
                    {
                        temp = servicesNotActive / (FindObjectOfType<GameplayHandler>().outages - prevOutages);
                        MTRS2.text = temp.ToString("00.0 Hours");
                    }
                    else
                    {
                        MTRS2.text = "672 Hours";
                    }
                }
                else
                {
                    MTRS2.text = "00.0 Hours";
                }
                prevOutages = Mathf.RoundToInt(FindObjectOfType<GameplayHandler>().outages);
                incidents = FindObjectOfType<ControlHandler>().GetComponent<ControlHandler>().incidents;
                incidents.Sort();
                temp = 0;
                foreach (int incident in incidents)
                {
                    if (lastInt != incident)
                    {
                        lastInt = incident;
                    }
                    else if (lastInt == incident)
                    {
                        temp += 1;
                    }
                }
                temp -= prevRepeatIncidents;
                RI2.text = temp.ToString();
                prevRepeatIncidents += Mathf.RoundToInt(temp);

                B2.text = string.Format("({0} / {1})", FindObjectOfType<ControlHandler>().bugs, facBugs); ;

                O2.text = (outages + capacityOutages).ToString();

                SI2.text = facSecurity.ToString();

                AR2.text = facAccess.ToString();

                foreach (ServiceCard service in FindObjectsOfType<ServiceCard>())
                {
                    service.outages = 0;
                }
                PrevRound();
                break;
            case 3:

                Clear();

                foreach (ServiceCard service in FindObjectsOfType<ServiceCard>())
                {
                    deplyedSegments += service.developedSegments;
                    releases += service.releases;
                    if (service.developed)
                    {
                        developedServices += 1;
                    }
                    servicesActive += service.timeActive;
                    servicesNotActive += service.timeInactive;
                    outages += service.outages;
                    capacityOutages += service.capacityOutage;
                    platConflicts += service.platConflicts;
                }

                prevPlatConflicts = platConflicts;

                NS3.text = (developedServices - startingServices).ToString();

                NR3.text = FindObjectOfType<ControlHandler>().GetComponent<ControlHandler>().releases.ToString();

                if ((FindObjectOfType<ControlHandler>().GetComponent<ControlHandler>().releases) > 0)
                {
                    DF3.text = ((28 * 24) / (FindObjectOfType<ControlHandler>().GetComponent<ControlHandler>().releases)).ToString("00.0/Hours");
                }
                else
                {
                    DF3.text = "00.0/Hours";
                }

                //temp = FindObjectOfType<ControlHandler>().segBugs + FindObjectOfType<GameplayHandler>().GetComponent<GameplayHandler>().badCodes;
                deplyedSegments = (deplyedSegments + FindObjectOfType<ControlHandler>().badCodes) - startingSegments;
                if (releases != 0)
                {

                    DR3.text = ((FindObjectOfType<ControlHandler>().bugs / FindObjectOfType<ControlHandler>().installs) * 100).ToString("00.0");

                    temp = FindObjectOfType<ControlHandler>().badCodes + FindObjectOfType<GameplayHandler>().serverCapacities;
                    temp = (temp / (FindObjectOfType<ControlHandler>().GetComponent<ControlHandler>().installs)) * 100;
                    if (developedServices - startingServices != 0 && temp <= 100)
                    {
                        FD3.text = temp.ToString("00.0");
                    }
                    else
                    {
                        FD3.text = "00.0";
                    }
                    temp = (FindObjectOfType<GameplayHandler>().badCodes + FindObjectOfType<GameplayHandler>().serverCapacities + FindObjectOfType<ControlHandler>().bugs);
                    if (facCodes != 0)
                    {
                        ICBD3.text = (temp / (temp + facCodes) * 100).ToString("00.0");
                    }
                    else if (temp != 0)
                    {
                        ICBD3.text = "100.0";
                    }
                    else
                    {
                        ICBD3.text = "00.0";
                    }
                }
                else
                {
                    DR3.text = "00.0";
                    FD3.text = "00.0";
                    ICBD3.text = "00.0";
                }

                CI3.text = capacityOutages.ToString();
                PC3.text = platConflicts.ToString();

                foreach (float available in availability)
                {
                    avilLength += 1;
                    availAdd += available;
                }
                temp = (Mathf.Round(availAdd / avilLength) * 10) / 10;
                A3.text = temp.ToString();

                //I3.text = string.Format("({0} / {1})", (FindObjectOfType<GameplayHandler>().serverCapacities + FindObjectOfType<ControlHandler>().bugs), facCodes);

                servicesNotActive -= prevInActive;

                if (servicesNotActive != 0)
                {
                    temp = 0;
                    if ((FindObjectOfType<GameplayHandler>().outages - prevOutages) != 0)
                    {
                        temp = servicesNotActive / (FindObjectOfType<GameplayHandler>().outages - prevOutages);
                        MTRS3.text = temp.ToString("00.0 Hours");
                    }
                    else
                    {
                        MTRS3.text = "672 Hours";
                    }
                }
                else
                {
                    MTRS3.text = "00.0 Hours";
                }
                prevOutages = Mathf.RoundToInt(FindObjectOfType<GameplayHandler>().outages);
                incidents = FindObjectOfType<ControlHandler>().GetComponent<ControlHandler>().incidents;
                incidents.Sort();
                temp = 0;
                foreach (int incident in incidents)
                {
                    if (lastInt != incident)
                    {
                        lastInt = incident;
                    }
                    else if (lastInt == incident)
                    {
                        temp += 1;
                    }
                }
                temp -= prevRepeatIncidents;
                RI3.text = temp.ToString();
                prevRepeatIncidents += Mathf.RoundToInt(temp);

                B3.text = string.Format("({0} / {1})", FindObjectOfType<ControlHandler>().bugs, facBugs);

                O3.text = (outages + capacityOutages).ToString();

                SI3.text = facSecurity.ToString();

                AR3.text = facAccess.ToString();

                foreach (ServiceCard service in FindObjectsOfType<ServiceCard>())
                {
                    service.outages = 0;
                }
                PrevRound();
                break;
            case 4:

                Clear();

                foreach (ServiceCard service in FindObjectsOfType<ServiceCard>())
                {
                    deplyedSegments += service.developedSegments;
                    releases += service.releases;
                    if (service.developed)
                    {
                        developedServices += 1;
                    }
                    servicesActive += service.timeActive;
                    servicesNotActive += service.timeInactive;
                    outages += service.outages;
                    capacityOutages += service.capacityOutage;
                    platConflicts += service.platConflicts;
                }

                prevPlatConflicts = platConflicts;

                NS4.text = (developedServices - startingServices).ToString();

                NR4.text = FindObjectOfType<ControlHandler>().GetComponent<ControlHandler>().releases.ToString();

                if ((FindObjectOfType<ControlHandler>().GetComponent<ControlHandler>().releases) > 0)
                {
                    DF4.text = ((28 * 24) / (FindObjectOfType<ControlHandler>().GetComponent<ControlHandler>().releases)).ToString("00.0/Hours");
                }
                else
                {
                    DF4.text = "00.0/Hours";
                }

                //temp = FindObjectOfType<ControlHandler>().segBugs + FindObjectOfType<GameplayHandler>().GetComponent<GameplayHandler>().badCodes;
                deplyedSegments = (deplyedSegments + FindObjectOfType<ControlHandler>().badCodes) - startingSegments;
                if (releases != 0)
                {

                    DR4.text = ((FindObjectOfType<ControlHandler>().bugs  / FindObjectOfType<ControlHandler>().releases) * 100).ToString("00.0");

                    temp = FindObjectOfType<ControlHandler>().badCodes + FindObjectOfType<GameplayHandler>().serverCapacities;
                    temp = (temp / (FindObjectOfType<ControlHandler>().GetComponent<ControlHandler>().installs)) * 100;
                    if (developedServices - startingServices != 0 && temp <= 100)
                    {
                        FD4.text = temp.ToString("00.0");
                    }
                    else
                    {
                        FD4.text = "00.0";
                    }
                    temp = (FindObjectOfType<GameplayHandler>().badCodes + FindObjectOfType<GameplayHandler>().serverCapacities + FindObjectOfType<ControlHandler>().bugs);
                    if (facCodes != 0)
                    {
                        ICBD4.text = (temp / (temp + facCodes) * 100).ToString("00.0");
                    }
                    else if (temp != 0)
                    {
                        ICBD4.text = "100.0";
                    }
                    else
                    {
                        ICBD4.text = "00.0";
                    }
                }
                else
                {
                    DR4.text = "00.0";
                    FD4.text = "00.0";
                    ICBD4.text = "00.0";
                }

                CI4.text = capacityOutages.ToString();
                PC4.text = platConflicts.ToString();

                foreach (float available in availability)
                {
                    avilLength += 1;
                    availAdd += available;
                }
                temp = (Mathf.Round(availAdd / avilLength) * 10) / 10;
                A4.text = temp.ToString("00.0");

                //I4.text = string.Format("({0} / {1})", (FindObjectOfType<GameplayHandler>().serverCapacities + FindObjectOfType<ControlHandler>().bugs), facCodes);

                servicesNotActive -= prevInActive;

                if (servicesNotActive != 0)
                {
                    temp = 0;
                    if (FindObjectOfType<GameplayHandler>().outages != 0)
                    {
                        temp = servicesNotActive / (FindObjectOfType<GameplayHandler>().outages - prevOutages);
                        MTRS4.text = temp.ToString("00.0 Hours");
                    }
                    else
                    {
                        MTRS4.text = "672 Hours";
                    }
                }
                else
                {
                    MTRS4.text = "00.0 Hours";
                }
                prevOutages = Mathf.RoundToInt(FindObjectOfType<GameplayHandler>().outages);
                incidents = FindObjectOfType<ControlHandler>().GetComponent<ControlHandler>().incidents;
                incidents.Sort();
                temp = 0;

                foreach (int incident in incidents)
                {
                    if (lastInt != incident)
                    {
                        lastInt = incident;
                    }
                    else if (lastInt == incident)
                    {
                        temp += 1;
                    }
                }
                temp -= prevRepeatIncidents;
                RI4.text = temp.ToString();
                prevRepeatIncidents += Mathf.RoundToInt(temp);

                B4.text = (string.Format("({0} / {1})", FindObjectOfType<ControlHandler>().bugs, facBugs));

                O4.text = (outages + capacityOutages).ToString();

                SI4.text = facSecurity.ToString();

                AR4.text = facAccess.ToString();

                foreach (ServiceCard service in FindObjectsOfType<ServiceCard>())
                {
                    service.outages = 0;
                }
                PrevRound();
                break;
        }

        round += 1;

    }

    void PrevRound()
    {
        prevFacs = Mathf.RoundToInt(facCodes);
        prevFacAccess = Mathf.RoundToInt(facAccess);
        prevFacBugs = Mathf.RoundToInt(facBugs);
        prevFacSecure = Mathf.RoundToInt(facSecurity);
        prevNewReleases = Mathf.RoundToInt(FindObjectOfType<ServiceInfoUI>().GetComponent<ServiceInfoUI>().totalNewServices);
        prevNewServices = Mathf.RoundToInt(developedServices);
        prevBugs = FindObjectOfType<ControlHandler>().bugs;
        prevGameBadCodes = FindObjectOfType<GameplayHandler>().badCodes;
        prevBadCodes = FindObjectOfType<ControlHandler>().badCodes;
        prevCapacities = FindObjectOfType<GameplayHandler>().serverCapacities;
        prevReleases = FindObjectOfType<ControlHandler>().GetComponent<ControlHandler>().releases;
        prevInstalls = FindObjectOfType<ControlHandler>().GetComponent<ControlHandler>().installs;
        prevCapUpgrades = FindObjectOfType<ControlHandler>().GetComponent<ControlHandler>().serverUpgrades;
        FindObjectOfType<ControlHandler>().segBugs = 0;
        startingSegments = 0;
        prevServReleases += releases;
        platConflicts -= prevPlatConflicts;
        foreach (ServiceCard service in FindObjectsOfType<ServiceCard>())
        {
            startingSegments += service.developedSegments;
        }
        availability.Clear();
    }

    void Clear()
    {
        FindObjectOfType<ServiceInfoUI>().GetComponent<ServiceInfoUI>().totalNewServices -= prevNewReleases;
        FindObjectOfType<GameplayHandler>().badCodes -= prevGameBadCodes;
        FindObjectOfType<ControlHandler>().badCodes -= prevBadCodes;
        FindObjectOfType<GameplayHandler>().serverCapacities -= prevCapacities;
        FindObjectOfType<ControlHandler>().bugs -= prevBugs;
        FindObjectOfType<ControlHandler>().GetComponent<ControlHandler>().securities = 0;
        FindObjectOfType<ControlHandler>().GetComponent<ControlHandler>().releases -= prevReleases;
        FindObjectOfType<ControlHandler>().GetComponent<ControlHandler>().installs -= prevInstalls;
        FindObjectOfType<ControlHandler>().GetComponent<ControlHandler>().serverUpgrades -= prevCapUpgrades;
        //FindObjectOfType<GameplayHandler>().outages -= prevOutages;
        releases -= prevServReleases;
        avilLength = 0;        
        facBugs -= Mathf.RoundToInt(prevFacBugs);
        facCodes -= prevFacs;
        facSecurity -= Mathf.RoundToInt(prevFacSecure);
        facAccess -= Mathf.RoundToInt(prevFacAccess);
        prevActive -= servicesActive;
        prevInActive += Mathf.RoundToInt(servicesNotActive);
        lastInt = 0;
        availAdd = 0;
        temp = 0;
        developedServices = 0;
        startingServices = prevNewServices;        
        deplyedSegments = 0;
        servicesActive = 0;        
        servInactiveTime = 0;
        outages = 0;
        capacityOutages = 0;
        servicesNotActive = 0;
        releases = 0;
    }
}
