using UnityEngine;
using System.Collections;

public class AlertManager: MonoBehaviour {

	public Alert[] alerts;
	private ArrayList randomAlerts = new ArrayList();
	private static ArrayList currentActiveAlerts = new ArrayList();
	public float minAlertTime, maxAlertTime;
	private bool pickRandomAlert;
	private bool setOffRandomAlert = true;
	private bool newAlert = true;
	private int randomAlertID;
	private float randomAlertTime, randomAlertStart, randomAnnoyanceLevel, annoyanceTimeElapse;
	private float minAnnoyance, maxAnnoyance; // ADD superior/inferior bool
	public AnimationCurve annoyanceCurve;

	public ReceiveNotice sendNotice;

	
	//---------------------------------------------------------
	//---------------------------------------------------------
	//	global methods to be called on alerts
	//---------------------------------------------------------
	//---------------------------------------------------------
	public static void CallAlert(Alert alertToCall){
		Debug.Log ("starting an alert");
		AddAlertVisual (alertToCall);
		alertToCall.ActivateAlert ();
		currentActiveAlerts.Add (alertToCall);
	}
	
	public static void ClearAlert(Alert alertToClear){
		Debug.Log ("clearing alert");
		alertToClear.ResolveAlert ();
		RemoveAlertVisual (alertToClear);
		currentActiveAlerts.Remove (alertToClear);
	}

	//---------------------------------------------------------
	//---------------------------------------------------------
	//	class randomly calls alerts forever unless told otherwise
	//---------------------------------------------------------
	//---------------------------------------------------------
	void Start(){
		annoyanceTimeElapse = 0.0f;
		foreach(Alert i in alerts){
			if(i.randomAlert){
				randomAlerts.Add(i);
			}
		}

		//temp test code
		//CallAlert ((Alert)alerts [0]);
	}

	void Update(){
		if(setOffRandomAlert) {
			if(newAlert){
				SetupRandomAlert();
			}
			if(Time.time - randomAlertStart > randomAlertTime) {
				//CallAlert ((Alert)randomAlerts[randomAlertID]);
				Debug.Log("notice with annoyance level " + randomAnnoyanceLevel);
				sendNotice.play = true; 
				newAlert = true;
			}
		}
	}

	void SetupRandomAlert(){
		//min and max annoyance move across animation curve
		annoyanceTimeElapse += 0.1f;
		minAnnoyance = (float)((annoyanceTimeElapse - 0.1f) * 10.0f);
		maxAnnoyance = (float)(annoyanceTimeElapse * 10.0f);

		randomAlertID = Random.Range (0, randomAlerts.Count);
		randomAlertTime = Random.Range (minAlertTime, maxAlertTime);
		randomAnnoyanceLevel = Random.Range (minAnnoyance, maxAnnoyance);
		newAlert = false;
		randomAlertStart = Time.time;
		Debug.Log ("new alert in " + randomAlertTime);

		//when at the end of the curve, queue the quiet on set script
		if (annoyanceTimeElapse > 1f) {
			setOffRandomAlert = false;
		}
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
