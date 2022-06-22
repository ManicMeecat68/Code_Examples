using System.Collections;
using System.Collections.Generic;
using ChartAndGraph;
using UnityEngine;

public class ReleaseReport : MonoBehaviour
{
    public ServiceCharts[] services;

    public GameObject[] lines;

    public int day = 5;

    private void Start()
    {
        day = 5;
        //StartCoroutine(CreateLine());
    }

    public IEnumerator CreateLine()
    {
        day += 1;

        foreach (ServiceCharts service in services)
        {
            //to prevent slowdown
            yield return new WaitForSecondsRealtime(0.01f);
            service.marked = false;
            foreach (ServiceCard targService in FindObjectsOfType<ServiceCard>())
            {
                if (targService.installedServiceID == service.serviceID)
                {
                    if(targService.developed && targService.active)
                    {
                        if (day >= 5)
                        {
                            Instantiate(lines[3],service.transform);
                        }
                        else if (day < 5)
                        {
                            Instantiate(lines[2], service.transform);
                        }
                        service.marked = true;
                        break;
                    }
                    else if (targService.developed && !targService.active)
                    {
                        if (day >= 5)
                        {
                            Instantiate(lines[5], service.transform);
                        }
                        else if (day < 5)
                        {
                            Instantiate(lines[4], service.transform);
                        }
                        service.marked = true;
                        break;
                    }
                    else if (!targService.developed)
                    {
                        if (day >= 5)
                        {
                            Instantiate(lines[1], service.transform);
                        }
                        else if (day < 5)
                        {
                            Instantiate(lines[0], service.transform);
                        }
                        service.marked = true;
                        break;
                    }
                    //else if (!targService.inDevelopment)
                    //{
                    //    if (day >= 5)
                    //    {
                    //        day = 1;
                    //        Instantiate(lines[1], service.transform);
                    //    }
                    //    else if (day < 5)
                    //    {
                    //        Instantiate(lines[0], service.transform);
                    //    }
                    //}
                }
            }
            if (!service.marked)
            {
                if (day >= 5)
                {
                    Instantiate(lines[1], service.transform);
                }
                else if (day < 5)
                {
                    Instantiate(lines[0], service.transform);
                }
                service.marked = false;
            }
        }
        if (day >= 5)
        {
            day = 0;
        }
        if (FindObjectOfType<Timer>().GetComponent<Timer>().curentTimeDays >= 10000)
        {
            GetComponent<Canvas>().enabled = true;
            this.gameObject.SetActive(false);
        }
    }
}
