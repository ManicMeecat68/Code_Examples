using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SnapshotCam : MonoBehaviour
{

    Camera snapCam;
    public Camera mainCam;

    public GameObject[] reports;

    int resWidth = 1920;
    int resHeight = 1080;

    int roundInt;

    private void Awake()
    {

        resWidth = Screen.width;
        resHeight = Screen.height;

        snapCam = GetComponent<Camera>();
        if (snapCam.targetTexture == null)
        {
            snapCam.targetTexture = new RenderTexture(resWidth, resHeight, 24);
        }
        else
        {
            resWidth = snapCam.targetTexture.width;
            resHeight = snapCam.targetTexture.height;
        }

        snapCam.gameObject.SetActive(false);
    }

    public void StartSnapshots(int roundNum)
    {
        roundInt = roundNum;
        snapCam.gameObject.SetActive(true);
    }
    void LateUpdate()
    {
        if (snapCam.gameObject.activeInHierarchy && !FindObjectOfType<PersistantObjects>().demo)
        {
            foreach (GameObject report in reports)
            {
                report.GetComponent<Canvas>().worldCamera = snapCam;
                report.GetComponent<Canvas>().enabled = true;
                Texture2D snapshot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
                snapCam.Render();
                RenderTexture.active = snapCam.targetTexture;
                snapshot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
                byte[] bytes = snapshot.EncodeToPNG();
                string filename = SnapshotName(report.name);
                if (!Directory.Exists(DirectoryName()))
                {
                    Directory.CreateDirectory(DirectoryName());
                }
                File.WriteAllBytes(filename, bytes);
                Debug.Log("Snapshot Taken : " + filename);
                report.GetComponent<Canvas>().enabled = false;
                report.GetComponent<Canvas>().worldCamera = mainCam;
            }
            snapCam.gameObject.SetActive(false);
        }
    }

    string SnapshotName(string report)
    {
        return string.Format("{0}/{1}.png",
            DirectoryName(),
            report
            );
    }

    string DirectoryName()
    {
        return string.Format("{0}/Reports/{2}/Game_{1}/Round_{3}",
            Application.persistentDataPath,
            FindObjectOfType<PersistantObjects>().currentGameId,
            System.DateTime.Now.ToString("dd-MM-yyyy"),
            roundInt
            );
    }

}
