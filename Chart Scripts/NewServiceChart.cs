using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class NewServiceChart : MonoBehaviour
{
    public GameObject serviceHolder;
    public GameObject serviceCard;

    private Color roundColour;

    private ServiceInfoUI info;

    private int count;

    private bool exists;

    private Colours colour;

    private void Start()
    {
        colour = FindObjectOfType<Colours>();
    }

    public void PopulateTable(int roundInt)
    {
        info = FindObjectOfType<ServiceInfoUI>().GetComponent<ServiceInfoUI>();

        //roundColour = Color.clear;

        if (roundInt == 1)
        {
            roundColour = colour.newServiceRoundOne;
        }
        else if (roundInt == 2)
        {
            roundColour = colour.newServiceRoundTwo;
        }
        if (roundInt == 3)
        {
            roundColour = colour.newServiceRoundThree;
        }
        else if (roundInt == 4)
        {
            roundColour = colour.newServiceRoundFour;
        }
        //else
        //{
        //    roundColour = sxpDeepBlue; Debug.Log("BACKON" + roundColour);
        //}
        foreach (ServiceCard service in FindObjectsOfType<ServiceCard>())
        {
            if (service.developedSegments > 0)
            {
                exists = false;
                Debug.Log("Developed");
                foreach (NewServiceCard card in serviceHolder.GetComponentsInChildren<NewServiceCard>())
                {
                    if (service.installedServiceID == card.ServiceID)
                    {
                        count += 1;
                        exists = true;
                        card.GenerateServiceInfo(service.installedServiceID, service.serviceName + " " + service.benefitString, Mathf.Round(service.revEffect * (info.addRev * 10000)),
                        service.codeADay, roundColour,
                        service.codeBDay, roundColour,
                        service.codeCDay, roundColour,
                        service.codeDDay, roundColour,
                        service.codeEDay, roundColour);
                    }
                }
                if (count < 1 && service.codeADay != 0 || !exists && service.codeADay != 0)
                {
                    var tempa = Instantiate(serviceCard, serviceHolder.transform.position, serviceHolder.transform.rotation, serviceHolder.transform);
                    tempa.GetComponent<NewServiceCard>().GenerateServiceInfo(service.installedServiceID, service.serviceName + " " + service.benefitString, Mathf.Round(service.revEffect * (info.addRev * 10000)),
                    service.codeADay, roundColour,
                    service.codeBDay, roundColour,
                    service.codeCDay, roundColour,
                    service.codeDDay, roundColour,
                    service.codeEDay, roundColour);
                }
                else if (count < 1 && service.codeDDay > 0 && service.initialServiceID > 0 || count < 1 && service.codeEDay > 0 && service.initialServiceID > 0 || !exists && service.codeDDay > 0 && service.initialServiceID > 0 || !exists && service.codeEDay > 0 && service.initialServiceID > 0)
                {
                    var tempb = Instantiate(serviceCard, serviceHolder.transform.position, serviceHolder.transform.rotation, serviceHolder.transform);
                    tempb.GetComponent<NewServiceCard>().GenerateServiceInfo(service.installedServiceID, service.serviceName + " " + service.benefitString, Mathf.Round(service.revEffect * (info.addRev * 10000)),
                    service.codeADay, roundColour,
                    service.codeBDay, roundColour,
                    service.codeCDay, roundColour,
                    service.codeDDay, roundColour,
                    service.codeEDay, roundColour);                    
                }
            }
        }
    }
}
