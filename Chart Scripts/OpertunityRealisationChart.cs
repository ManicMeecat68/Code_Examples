using System.Collections;
using System.Collections.Generic;
using ChartAndGraph;
using UnityEngine;

public class OpertunityRealisationChart : MonoBehaviour
{

    public GraphChart chart;

    public List<Vector3> maxRevenue = new List<Vector3>();
    public List<Vector3> actualRevenue = new List<Vector3>();
    public List<Vector3> averageRevenue = new List<Vector3>();

    public void UpdateChart()
    {
        int i = 0;
        chart.DataSource.ClearCategory("Max Revenue");
        foreach (Vector3 point in maxRevenue)
        {
            chart.DataSource.AddPointToCategory("Max Revenue", maxRevenue[i].x, maxRevenue[i].y);
            i++;
        }
        i = 0;
        chart.DataSource.ClearCategory("Actual Revenue");
        foreach (Vector3 point in actualRevenue)
        {
            chart.DataSource.AddPointToCategory("Actual Revenue", actualRevenue[i].x, actualRevenue[i].y);
            i++;
        }
        chart.DataSource.ClearCategory("Average Revenue");
        i = 0;
        foreach (Vector3 point in averageRevenue)
        {
            chart.DataSource.AddPointToCategory("Average Revenue", averageRevenue[i].x, averageRevenue[i].y);
            i++;
        }
    }

    public void UpdateChart(int roundID)
    {
        //chart.DataSource.StartBatch();
        chart.DataSource.ClearCategory("Max Revenue");
        int i = 0;
        foreach (Vector3 point in maxRevenue)
        {
            if (maxRevenue[i].z == roundID)
            {
                chart.DataSource.AddPointToCategory("Max Revenue", maxRevenue[i].x, maxRevenue[i].y);
            }
            i++;
        }

        chart.DataSource.ClearCategory("Actual Revenue");
        i = 0;
        foreach (Vector3 point in actualRevenue)
        {
            if (actualRevenue[i].z == roundID)
            {
                chart.DataSource.AddPointToCategory("Actual Revenue", actualRevenue[i].x, actualRevenue[i].y);
            }
            i++;
        }

        chart.DataSource.ClearCategory("Average Revenue");
        i = 0;
        foreach (Vector3 point in averageRevenue)
        {
            if (maxRevenue[i].z == roundID)
            {
                chart.DataSource.AddPointToCategory("Average Revenue", averageRevenue[i].x, averageRevenue[i].y);
            }
            i++;
        }
    }
}
