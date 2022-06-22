using System.Collections;
using System.Collections.Generic;
using ChartAndGraph;
using UnityEngine;

public class BusinessKPIChart : MonoBehaviour
{

    public GraphChart chart;

    public List<Vector3> satPoints = new List<Vector3>();
    public List<Vector3> loadPoints = new List<Vector3>();
    public List<Vector3> perfPoints = new List<Vector3>();

    public void UpdateChart()
    {

        //chart.DataSource.StartBatch();
        chart.DataSource.ClearCategory("Customer Satisfaction");
        int i = 0;
        foreach (Vector3 point in satPoints)
        {
            chart.DataSource.AddPointToCategory("Customer Satisfaction", satPoints[i].x, satPoints[i].y);
            i++;
        }

        chart.DataSource.ClearCategory("Load Factor");
        i = 0;
        foreach (Vector3 point in loadPoints)
        {
            chart.DataSource.AddPointToCategory("Load Factor", loadPoints[i].x, loadPoints[i].y);
            i++;
        }

        chart.DataSource.ClearCategory("On Time Performance");
        i = 0;
        foreach (Vector3 point in perfPoints)
        {
            chart.DataSource.AddPointToCategory("On Time Performance", perfPoints[i].x, perfPoints[i].y);
            i++;
        }
        //chart.DataSource.EndBatch();

    }
        public void UpdateChart(int roundID)
    {

        //chart.DataSource.StartBatch();
        chart.DataSource.ClearCategory("Customer Satisfaction");
        int i = 0;
        foreach (Vector3 point in satPoints)
        {
            if (satPoints[i].z == roundID)
            {
                chart.DataSource.AddPointToCategory("Customer Satisfaction", satPoints[i].x, satPoints[i].y);
            }
            i++;
        }

        chart.DataSource.ClearCategory("Load Factor");
        i = 0;
        foreach (Vector3 point in loadPoints)
        {
            if (loadPoints[i].z == roundID)
            {
                chart.DataSource.AddPointToCategory("Load Factor", loadPoints[i].x, loadPoints[i].y);
            }
            i++;
        }

        chart.DataSource.ClearCategory("On Time Performance");
        i = 0;
        foreach (Vector3 point in perfPoints)
        {
            if (perfPoints[i].z == roundID)
            {
                chart.DataSource.AddPointToCategory("On Time Performance", perfPoints[i].x, perfPoints[i].y);
            }
            i++;
        }
        //chart.DataSource.EndBatch();

    }
}
