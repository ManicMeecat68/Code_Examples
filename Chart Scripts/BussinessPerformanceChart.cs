using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BussinessPerformanceChart : MonoBehaviour
{
    [Header("Round 1")]
    public Text OM1;
    public Text LP1;
    public Text CS1;
    public Text OTP1;
    public Text LF1;
    public Text MRPH1;
    public Text ARPH1;
    public Text ITS1;
    public Text OP1;
    public Text C1;
    public Text S1;

    [Header("Round 2")]
    public Text OM2;
    public Text LP2;
    public Text CS2;
    public Text OTP2;
    public Text LF2;
    public Text MRPH2;
    public Text ARPH2;
    public Text ITS2;
    public Text OP2;
    public Text C2;
    public Text S2;

    [Header("Round 3")]
    public Text OM3;
    public Text LP3;
    public Text CS3;
    public Text OTP3;
    public Text LF3;
    public Text MRPH3;
    public Text ARPH3;
    public Text ITS3;
    public Text OP3;
    public Text C3;
    public Text S3;

    [Header("Round 4")]
    public Text OM4;
    public Text LP4;
    public Text CS4;
    public Text OTP4;
    public Text LF4;
    public Text MRPH4;
    public Text ARPH4;
    public Text ITS4;
    public Text OP4;
    public Text C4;
    public Text S4;

    public List<float> availability = new List<float>();

    private List<int> incidents = new List<int>();
    private List<float> curRevUpdate = new List<float>();

    public int round = 1;
    public int avilLength;

    private float serviceCount;
    private float installedInternal;
    private float installedCloud;
    private float installedSaas;
    private int lastInt;
    private int positionCount;
    
    public float availAdd;

    private float temp;
    private float averageRev;
    private float servCost;

    private ServiceDatabase database;

    public void SetText()
    {        
        temp = 0;

        database = FindObjectOfType<ServiceDatabase>().GetComponent<ServiceDatabase>();

        switch (round)
        {
            case 1:
                installedCloud = 0;
                installedInternal = 0;
                installedSaas = 0;
                serviceCount = 0;
                servCost = 0;
                foreach (ScorboardCompany company in FindObjectOfType<ScoreboardPlacement>().scoreboardCompanies)
                {
                    positionCount += 1;
                    if (company.playerCompany)
                    {
                        temp = company.operatingMargin;
                        positionCount = FindObjectOfType<ScoreboardPlacement>().scoreboardCompanies.IndexOf(company) + 1;
                        break;
                    }
                }
                foreach (ServiceCard service in FindObjectsOfType<ServiceCard>())
                {
                    if (service.developed)
                    {
                        serviceCount += 1;
                        servCost += service.cost;
                    }
                }

                foreach (Servers server in FindObjectsOfType<Servers>())
                {
                    if (server.curCapacity > 0)
                    {
                        switch (server.webFirewallID)
                        {
                            case 1:
                                installedInternal += 1;
                                break;
                            case 2:
                                installedInternal += 1;
                                break;
                            case 3:
                                installedCloud += 1;
                                break;
                            case 4:
                                installedSaas += 1;
                                break;
                            default:
                                Debug.Log("ERROR : UNKOWN INSTALL LOCATION");
                                break;
                        }
                    }
                }

                OM1.text = temp.ToString("00.0");

                LP1.text = positionCount.ToString();

                CS1.text = (Mathf.Round(FindObjectOfType<ServiceInfoUI>().GetComponent<ServiceInfoUI>().satVal * 10) / 10).ToString();

                OTP1.text = Mathf.Round(FindObjectOfType<OnTimePerformanceUI>().GetComponent<OnTimePerformanceUI>().bar.GetComponent<Image>().fillAmount * 100).ToString();

                LF1.text = Mathf.Round(FindObjectOfType<LoadFactorUI>().GetComponent<LoadFactorUI>().bar.GetComponent<Image>().fillAmount * 100).ToString();

                MRPH1.text = (FindObjectOfType<ServiceInfoUI>().GetComponent<ServiceInfoUI>().maxRev * 1000000).ToString("$0,000,000");

                foreach (float rev in curRevUpdate)
                {
                    averageRev += rev;
                }
                averageRev /= curRevUpdate.Count;
                ARPH1.text = (averageRev * 1000000).ToString("$0,000,000");
                
                ITS1.text = servCost.ToString("$000,000");

                OP1.text = ((installedInternal/serviceCount) * 100).ToString("00.0");

                C1.text = ((installedCloud / serviceCount) * 100).ToString("00.0");

                S1.text = ((installedSaas / serviceCount) * 100).ToString("00.0");

                break;
            case 2:
                positionCount = 0;
                installedCloud = 0;
                installedInternal = 0;
                installedSaas = 0;
                serviceCount = 0;
                servCost = 0;
                foreach (ScorboardCompany company in FindObjectOfType<ScoreboardPlacement>().scoreboardCompanies)
                {
                    positionCount += 1;
                    if (company.playerCompany)
                    {
                        temp = company.operatingMargin;
                        positionCount = FindObjectOfType<ScoreboardPlacement>().scoreboardCompanies.IndexOf(company) + 1;
                        break;
                    }
                }
                foreach (ServiceCard service in FindObjectsOfType<ServiceCard>())
                {
                    if (service.developed)
                    {
                        serviceCount += 1;
                        servCost += service.cost;
                    }
                }

                foreach (Servers server in FindObjectsOfType<Servers>())
                {
                    if (server.curCapacity > 0)
                    {
                        switch (server.webFirewallID)
                        {
                            case 1:
                                installedInternal += 1;
                                break;
                            case 2:
                                installedInternal += 1;
                                break;
                            case 3:
                                installedCloud += 1;
                                break;
                            case 4:
                                installedSaas += 1;
                                break;
                            default:
                                Debug.Log("ERROR : UNKOWN INSTALL LOCATION");
                                break;
                        }
                    }
                }
                OM2.text = temp.ToString("00.00");

                LP2.text = positionCount.ToString();

                CS2.text = (Mathf.Round(FindObjectOfType<ServiceInfoUI>().GetComponent<ServiceInfoUI>().satVal * 10) / 10).ToString();

                OTP2.text = Mathf.Round(FindObjectOfType<OnTimePerformanceUI>().GetComponent<OnTimePerformanceUI>().bar.GetComponent<Image>().fillAmount * 100).ToString();

                LF2.text = Mathf.Round(FindObjectOfType<LoadFactorUI>().GetComponent<LoadFactorUI>().bar.GetComponent<Image>().fillAmount * 100).ToString();

                MRPH2.text = (FindObjectOfType<ServiceInfoUI>().GetComponent<ServiceInfoUI>().maxRev * 1000000).ToString("$0,000,000");

                averageRev = 0;
                foreach (float rev in curRevUpdate)
                {
                    averageRev += rev;
                }
                averageRev /= curRevUpdate.Count;
                ARPH2.text = (averageRev * 1000000).ToString("$0,000,000");

                ITS2.text = servCost.ToString("$000,000");

                OP2.text = ((installedInternal / serviceCount) * 100).ToString("00.0");

                C2.text = ((installedCloud / serviceCount) * 100).ToString("00.0");

                S2.text = ((installedSaas / serviceCount) * 100).ToString("00.0");

                break;
            case 3:
                positionCount = 0;
                installedCloud = 0;
                installedInternal = 0;
                installedSaas = 0;
                serviceCount = 0;
                servCost = 0;
                foreach (ScorboardCompany company in FindObjectOfType<ScoreboardPlacement>().scoreboardCompanies)
                {
                    positionCount += 1;
                    if (company.playerCompany)
                    {
                        temp = company.operatingMargin;
                        positionCount = FindObjectOfType<ScoreboardPlacement>().scoreboardCompanies.IndexOf(company) + 1;
                        break;
                    }
                }
                foreach (ServiceCard service in FindObjectsOfType<ServiceCard>())
                {
                    if (service.developed)
                    {
                        serviceCount += 1;
                        servCost += service.cost;
                    }
                }

                foreach (Servers server in FindObjectsOfType<Servers>())
                {
                    if (server.curCapacity > 0)
                    {
                        switch (server.webFirewallID)
                        {
                            case 1:
                                installedInternal += 1;
                                break;
                            case 2:
                                installedInternal += 1;
                                break;
                            case 3:
                                installedCloud += 1;
                                break;
                            case 4:
                                installedSaas += 1;
                                break;
                            default:
                                Debug.Log("ERROR : UNKOWN INSTALL LOCATION");
                                break;
                        }
                    }
                }
                OM3.text = temp.ToString("00.0");

                LP3.text = positionCount.ToString();

                CS3.text = (Mathf.Round(FindObjectOfType<ServiceInfoUI>().GetComponent<ServiceInfoUI>().satVal * 10) / 10).ToString();

                OTP3.text = Mathf.Round(FindObjectOfType<OnTimePerformanceUI>().GetComponent<OnTimePerformanceUI>().bar.GetComponent<Image>().fillAmount * 100).ToString();

                LF3.text = Mathf.Round(FindObjectOfType<LoadFactorUI>().GetComponent<LoadFactorUI>().bar.GetComponent<Image>().fillAmount * 100).ToString();

                MRPH3.text = (FindObjectOfType<ServiceInfoUI>().GetComponent<ServiceInfoUI>().maxRev * 1000000).ToString("$0,000,000");

                averageRev = 0;
                foreach (float rev in curRevUpdate)
                {
                    averageRev += rev;
                }
                averageRev /= curRevUpdate.Count;
                ARPH3.text = (averageRev * 1000000).ToString("$0,000,000");

                ITS3.text = servCost.ToString("$000,000");

                OP3.text = ((installedInternal / serviceCount) * 100).ToString("00.0");

                C3.text = ((installedCloud / serviceCount) * 100).ToString("00.0");

                S3.text = ((installedSaas / serviceCount) * 100).ToString("00.0");

                break;
            case 4:
                positionCount = 0;
                installedCloud = 0;
                installedInternal = 0;
                installedSaas = 0;
                serviceCount = 0;
                servCost = 0;
                foreach (ScorboardCompany company in FindObjectOfType<ScoreboardPlacement>().scoreboardCompanies)
                {
                    positionCount += 1;
                    if (company.playerCompany)
                    {
                        temp = company.operatingMargin;
                        positionCount = FindObjectOfType<ScoreboardPlacement>().scoreboardCompanies.IndexOf(company) + 1;
                        break;
                    }
                }
                foreach (ServiceCard service in FindObjectsOfType<ServiceCard>())
                {
                    if (service.developed)
                    {
                        serviceCount += 1;
                        servCost += service.cost;
                    }
                }

                foreach (Servers server in FindObjectsOfType<Servers>())
                {
                    if (server.curCapacity > 0)
                    {
                        switch (server.webFirewallID)
                        {
                            case 1:
                                installedInternal += 1;
                                break;
                            case 2:
                                installedInternal += 1;
                                break;
                            case 3:
                                installedCloud += 1;
                                break;
                            case 4:
                                installedSaas += 1;
                                break;
                            default:
                                Debug.Log("ERROR : UNKOWN INSTALL LOCATION");
                                break;
                        }
                    }
                }
                OM4.text = temp.ToString("00.0");

                LP4.text = positionCount.ToString();

                CS4.text = (Mathf.Round(FindObjectOfType<ServiceInfoUI>().GetComponent<ServiceInfoUI>().satVal * 10) / 10).ToString();

                OTP4.text = Mathf.Round(FindObjectOfType<OnTimePerformanceUI>().GetComponent<OnTimePerformanceUI>().bar.GetComponent<Image>().fillAmount * 100).ToString();

                LF4.text = Mathf.Round(FindObjectOfType<LoadFactorUI>().GetComponent<LoadFactorUI>().bar.GetComponent<Image>().fillAmount * 100).ToString();

                MRPH4.text = (FindObjectOfType<ServiceInfoUI>().GetComponent<ServiceInfoUI>().maxRev * 1000000).ToString("$0,000,000");

                averageRev = 0;
                foreach (float rev in curRevUpdate)
                {
                    averageRev += rev;
                }
                averageRev /= curRevUpdate.Count;
                ARPH4.text = (averageRev * 1000000).ToString("$0,000,000");

                ITS4.text = servCost.ToString("$000,000");

                OP4.text = ((installedInternal / serviceCount) * 100).ToString("00.0"); ;
                
                C4.text = ((installedCloud / serviceCount) * 100).ToString("00.0");

                S4.text = ((installedSaas / serviceCount) * 100).ToString("00.0");

                

                break;
        }

        Clear();
        round += 1;

    }
    public void HourlyUpdate()
    {
        curRevUpdate.Add(FindObjectOfType<ServiceInfoUI>().GetComponent<ServiceInfoUI>().addRev);       
    }
    void Clear()
    {
        curRevUpdate.Clear();
    }

}
