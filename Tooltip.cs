using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Tooltip : MonoBehaviour {

    private bool active;

	private ServiceCard service;

	private string data;

	void Start(){
        transform.root.GetComponent<Canvas>().enabled = false;
	}

	void Update(){

        if (active /*&& Input.GetButton("AltAction")*/){
            if (transform.root.GetComponent<Canvas>().enabled == false)
            {
                transform.root.GetComponent<Canvas>().enabled = true;
            }
            gameObject.transform.position = Input.mousePosition;
		}
        //if (Input.GetButtonUp("AltAction"))
        //{
        //    transform.root.GetComponent<Canvas>().enabled = false;
        //}
	
	}

	public void Activate(ServiceCard service){
		this.service = service;
        if (service.installedServiceID > 0)
        {
            ConstructDataString();
            active = true;
        }
    }

	public void Deactivate(){
        transform.root.GetComponent<Canvas>().enabled = false;
        active = false;
    }

	public void ConstructDataString(){
        data = string.Format("Service Name : {0}\nService ID : {1}\nServer ID : {2}", service.serviceName, service.installedServiceID, service.Id);
        gameObject.transform.GetComponentInChildren<Text>().text = data;
	}
}
