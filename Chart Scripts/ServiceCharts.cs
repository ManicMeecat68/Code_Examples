using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServiceCharts : MonoBehaviour
{
    public int serviceID;

    public bool marked;

    private ServiceDatabase database;

    private void Start()
    {
        database = FindObjectOfType<ServiceDatabase>().GetComponent<ServiceDatabase>();

        if (database.FetchServiceByID(serviceID) != null)
        {
            GetComponentInChildren<Text>().text = database.FetchServiceByID(serviceID).ServiceName;
        }
    }
}
