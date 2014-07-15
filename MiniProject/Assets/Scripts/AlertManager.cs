using UnityEngine;
using System.Collections;

public class AlertManager: MonoBehaviour {

	public Alert[] alerts;
	private ArrayList randomAlerts = new ArrayList();
	public float minAlertTime, maxAlertTime;
	private bool pickRandomAlert;
	private bool setOffRandomAlert = false;
	private bool newAlert = true;
	private int randomAlertID;
	private float randomAlertTime, randomAlertStart;

	void Start(){
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
			}
		}
	}

	void SetupRandomAlert(){
		randomAlertID = Random.Range (0, randomAlerts.Count);
		randomAlertTime = Random.Range (minAlertTime, maxAlertTime);
		newAlert = false;
		randomAlertStart = Time.time;
	}

	public static void CallAlert(Alert alertToCall){
		AddAlertVisual ();
		alertToCall.ActivateAlert ();
	}

	public static void ClearAlert(Alert alertToClear){
		alertToClear.ResolveAlert ();
		RemoveAlertVisual ();
	}

	public static void RemoveAlertVisual(){

	}

	public static void AddAlertVisual(){

	}
	
}
