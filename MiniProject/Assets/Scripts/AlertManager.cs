using UnityEngine;
using System.Collections;

public static class AlertManager : MonoBehaviour {

	public Alert[] alerts;
	private Alert[] randomAlerts;
	private bool pickRandomAlert;


	// Use this for initialization
	void Start () {
		//cycle through alerts, if alert isn't intended to be triggered at a particular time,
		//add it to random alerts
		//.RandomAlert();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void CallAlert(Alert alertToCall){
		AddAlertVisual ();
		alertToCall.ActivateAlert ();
	}

	public void ClearAlert(Alert alertToClear){
		alertToCall.ResolveAlert ();
		RemoveAlertVisual ();
	}

	public void RemoveAlertVisual(){

	}

	public void AddAlertVisual(){

	}
	
}
