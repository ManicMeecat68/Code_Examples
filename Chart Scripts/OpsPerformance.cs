using System.Collections;
using System.Collections.Generic;
using ChartAndGraph;
using UnityEngine;

public class OpsPerformance : MonoBehaviour
{

    public BarChart chart;

    [Header ("Categories")]
    public List<Vector2> bugs = new List<Vector2>();
    public List<Vector2> installs = new List<Vector2>();
    public List<Vector2> security = new List<Vector2>();
    public List<Vector2> hardware = new List<Vector2>();
    public List<Vector2> serviceRequests = new List<Vector2>();

    public void UpdateChart()
    {
        //chart.DataSource.StartBatch();
        //chart.DataSource.AutomaticMaxValue = true;
        foreach (Vector2 bug in bugs)
        {
            if (bug.x <= 1)
            {
                chart.DataSource.SetValue("Bugs", "Round 1", bug.y);
            }
            else if (bug.x <= 2)
            {
                chart.DataSource.SetValue("Bugs", "Round 2", bug.y);
            }
            else if (bug.x <= 3)
            {
                chart.DataSource.SetValue("Bugs", "Round 3", bug.y);
            }
            else if (bug.x <= 4)
            {
                chart.DataSource.SetValue("Bugs", "Round 4", bug.y);
            }
        }
        foreach (Vector2 install in installs)
        {
            if (install.x <= 1)
            {
                chart.DataSource.SetValue("Install", "Round 1", install.y);
            }
            else if (install.x <= 2)
            {
                chart.DataSource.SetValue("Install", "Round 2", install.y);
            }
            else if (install.x <= 3)
            {
                chart.DataSource.SetValue("Install", "Round 3", install.y);
            }
            else if (install.x <= 4)
            {
                chart.DataSource.SetValue("Install", "Round 4", install.y);
            }
        }
        foreach (Vector2 secure in security)
        {
            if (secure.x <= 1)
            {
                chart.DataSource.SetValue("Security", "Round 1", secure.y);
            }
            else if (secure.x <= 2)
            {
                chart.DataSource.SetValue("Security", "Round 2", secure.y);
            }
            else if (secure.x <= 3)
            {
                chart.DataSource.SetValue("Security", "Round 3", secure.y);
            }
            else if (secure.x <= 4)
            {
                chart.DataSource.SetValue("Security", "Round 4", secure.y);
            }
        }
        foreach (Vector2 hrdware in hardware)
        {
            if (hrdware.x <= 1)
            {
                chart.DataSource.SetValue("Hardware", "Round 1", hrdware.y);
            }
            else if (hrdware.x <= 2)
            {
                chart.DataSource.SetValue("Hardware", "Round 2", hrdware.y);
            }
            else if (hrdware.x <= 3)
            {
                chart.DataSource.SetValue("Hardware", "Round 3", hrdware.y);
            }
            else if (hrdware.x <= 4)
            {
                chart.DataSource.SetValue("Hardware", "Round 4", hrdware.y);
            }
        }
        foreach (Vector2 serviceRequest in serviceRequests)
        {
            if (serviceRequest.x <= 1)
            {
                chart.DataSource.SetValue("Service Requests", "Round 1", serviceRequest.y);
            }
            else if (serviceRequest.x <= 2)
            {
                chart.DataSource.SetValue("Service Requests", "Round 2", serviceRequest.y);
            }
            else if (serviceRequest.x <= 3)
            {
                chart.DataSource.SetValue("Service Requests", "Round 3", serviceRequest.y);
            }
            else if (serviceRequest.x <= 4)
            {
                chart.DataSource.SetValue("Service Requests", "Round 4", serviceRequest.y);
            }
        }
        //chart.DataSource.EndBatch();
    }
}
