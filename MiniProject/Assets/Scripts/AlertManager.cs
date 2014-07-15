﻿using UnityEngine;
using System.Collections;

public class AlertManager: MonoBehaviour {

	public Alert[] alerts;
	private ArrayList randomAlerts = new ArrayList();
	public float minAlertTime, maxAlertTime;
	private bool pickRandomAlert;
	private bool setOffRandomAlert = true;
	private bool newAlert = true;
	private int randomAlertID;
	private float randomAlertTime, randomAlertStart;

	
	//---------------------------------------------------------
	//---------------------------------------------------------
	//	global methods to be called on alerts
	//---------------------------------------------------------
	//---------------------------------------------------------
	public static void CallAlert(Alert alertToCall){
		AddAlertVisual (alertToCall);
		alertToCall.ActivateAlert ();
	}
	
	public static void ClearAlert(Alert alertToClear){
		alertToClear.ResolveAlert ();
		RemoveAlertVisual (alertToClear);
	}

	//---------------------------------------------------------
	//---------------------------------------------------------
	//	class randomly calls alerts forever unless told otherwise
	//---------------------------------------------------------
	//---------------------------------------------------------
	void Start(){
		//these are overriden if values are set in editor
		minAlertTime = 3.0f;
		maxAlertTime = 12.0f;

		foreach(Alert i in alerts){
			if(i.randomAlert){
				randomAlerts.Add(i);
			}
		}
	}

	void Update(){
		if(setOffRandomAlert) {
			if(newAlert){
				SetupRandomAlert();
			}
			if(Time.time - randomAlertStart == randomAlertTime) {
				CallAlert ((Alert)randomAlerts[randomAlertID]);
				newAlert = true;
			}
		}
	}

	void SetupRandomAlert(){
		randomAlertID = Random.Range (0, randomAlerts.Count);
		randomAlertTime = Random.Range (minAlertTime, maxAlertTime);
		newAlert = false;
		randomAlertStart = Time.time;
	}


	//---------------------------------------------------------
	//---------------------------------------------------------
	//	PLACEHOLDERS:
	//---------------------------------------------------------
	//---------------------------------------------------------
	
	static void RemoveAlertVisual(Alert alert){
		//remove color visual
		Debug.Log ("Alert successfully removed");
	}
	
	static void AddAlertVisual(Alert alert){
		//color visuals
		Debug.Log ("Alert successfully added");
	}

}