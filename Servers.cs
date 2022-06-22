using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Servers : MonoBehaviour {

    [Header("IDs")]
    public int perimiterFirewallID;
    public int webFirewallID;
    public int switchID;
    public int serverID;
    public int crashID;

    [Header("Info")]
    public int maxCapacity = 5;
    public float curCapacity;
    public bool serverRunning = true;

    [HideInInspector] public float timeDown;
    [HideInInspector] public float timeUp;
    [HideInInspector] public int errorID;
    public int serverType;

    public bool crashed;

    private bool capacityCrash;

    public List<ServiceCard> services = new List<ServiceCard>();

    private void Start()
    {
        maxCapacity = 5;
        curCapacity = -1;
        foreach (ServiceCard card in FindObjectsOfType<ServiceCard>())
        {
            services.Add(card);
        }
    }

    private void Update()
    { 
        foreach (ServiceCard service in services)
        {
            if (service.Id == serverID)
            {
                if (service.installedServiceID > 0)
                {
                    curCapacity = service.capacity;
                }                
            }
            if (curCapacity > maxCapacity && !crashed)
            {
                errorID = 2;
                service.capacityOutage++;
                StartCoroutine(Crash(30));
                return;
            }
            else if (curCapacity <= maxCapacity && crashed)
            {
                crashed = false;
                timeUp = Random.Range(5, 10);
                ServerUp();
                serverRunning = true;
            }
        }
    }

    public IEnumerator ServerDown(bool switchCrash)
    {

        yield return new WaitForSecondsRealtime(0.5f);

        bool crashed = false;

        if (switchID != -1)
        {
            serverRunning = false;
            foreach (ServiceCard service in FindObjectsOfType<ServiceCard>())
            {
                if (service.Id == serverID && service.developed )
                {
                    if (!service.badCode)
                    {
                        service.timeDown = timeDown;
                        service.errorCode = errorID;
                        service.errorID.GetComponent<Text>().text = service.errorCode.ToString("0");
                        service.curEffeciency = 0;
                        StartCoroutine(service.ServerCrash());
                        //service.capacityOutage++;

                        if (!crashed)
                        {
                            crashed = true;
                            if (!service.facIncreased && !switchCrash && !service.badCode && !service.unHappy)
                            {

                                FindObjectOfType<GameplayHandler>().GetComponent<GameplayHandler>().serverCapacities += 1;
                            }
                            else
                            {
                                service.facIncreased = false;
                            }
                        }                                           
                    }

                    while (!serverRunning)
                    {
                        yield return new WaitForSecondsRealtime(1);

                        if (service.active && !service.badCode)
                        {
                            service.errorID.GetComponent<Text>().text = service.errorCode.ToString("0");
                            service.curEffeciency = 0;
                            StartCoroutine(service.ServerCrash());
                        }
                    }                    
                }
            }
        }
    }

    public void ServerUp()
    {

    serverRunning = true;
    capacityCrash = false;

    foreach (ServiceCard service in FindObjectsOfType<ServiceCard>())
        {
            if (service.Id == serverID)
            {
                RestoreServices(service);
            }
        }
    }
    public void ServerFail()
    {
        if (switchID != -1)
        {
            serverRunning = false;
            foreach (ServiceCard service in FindObjectsOfType<ServiceCard>())
            {
                if (service.Id == serverID)
                {
                    service.timeUp = timeUp;
                    service.curEffeciency = 0;
                    StartCoroutine(service.ServiceActivateFailure());
                }
            }
        }
    }

    public IEnumerator Crash(int time = 120)
    {
        crashed = true;
        int temp = 0;
        if (!capacityCrash)
        {
            capacityCrash = true;
        }
        while(curCapacity > maxCapacity && temp < time)
        {
            yield return new WaitForSecondsRealtime(1);
            temp += 1;
        }
        if (curCapacity > maxCapacity)
        {
            StartCoroutine(ServerDown(false));
            serverRunning = false;
        }
    }

    public IEnumerator AddStorage(int time = 10)
    {
        ServiceCard serv = new ServiceCard();
        foreach (ServiceCard service in FindObjectsOfType<ServiceCard>())
        {
            if (service.Id == serverID)
            {
                service.curEffeciency = 0;
                service.AddStorage();
                serv = service;
            }
        }

        yield return new WaitForSecondsRealtime(time);

        maxCapacity += 2;

        if (serv.badCode)
        {
            Debug.Log("Didnt clear ot");
            serv.DevelopedBug();
        }
        
    }
    private void RestoreServices(ServiceCard service)
    {
        service.timeUp = timeUp;
        service.serverCrashed = false;
        if (service.badCode)
        {
            service.errorCode = 1;
            Debug.Log("HERE IS BAD");
            StartCoroutine(service.ServiceActivateFailure());
            if (service.dependentID > 0)
            {
                StartCoroutine(service.DepCheck());
            }
        }
        else if (service.bugCode)
        {
            Debug.Log("HERE IS BAD");
            service.errorCode = 1;
            StartCoroutine(service.ServiceActivateFailure());
            if (service.dependentID > 0)
            {
                StartCoroutine(service.DepCheck());
            }
        }
        else if (FindObjectOfType<ErrorCodeDatabase>().GetComponent<ErrorCodeDatabase>().FetchErrorByID(service.errorId) != null)
        {
            if (FindObjectOfType<ErrorCodeDatabase>().GetComponent<ErrorCodeDatabase>().FetchErrorByID(service.errorId).ErrorFixMethod != "Reboot Switch")
            {
                Debug.Log("HERE IS BAD " + service.errorID);
                service.errorCode = 1;
                StartCoroutine(service.ServiceActivateFailure());
                if (service.dependentID > 0)
                {
                    StartCoroutine(service.DepCheck());
                }
            }
        }
        else
        {
            StartCoroutine(service.ServiceActivateSuccess());
            StartCoroutine(service.DepCheck());
        }
    }
}
