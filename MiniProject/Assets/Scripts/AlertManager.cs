using UnityEngine;
using System.Collections;

public class AlertManager: MonoBehaviour {

	//BRIAN, use randomAnnoyanceLevel for the values you need

	public Alert[] alerts;
	private ArrayList randomAlerts = new ArrayList();
	private static ArrayList currentActiveAlerts = new ArrayList();
	public float minAlertTime, maxAlertTime;
	private bool pickRandomAlert;
	private bool setOffRandomAlert = true;
	private bool newAlert = true;
	private int randomAlertID;
	private float randomAlertTime, randomAlertStart, annoyanceTimeElapse;
	public float randomAnnoyanceLevel;
	private float minAnnoyance, maxAnnoyance;
	private float superiorLevel;
	private int superiorOrSubordinate; 
	public AnimationCurve annoyanceCurve;
	private float lastCurveIncrement, betweenCurveIncrement;
	private bool incrementedAlert = false;

	public ReceiveNotice sendNotice;
	public QuietOnSet queueRecording;



	
	//---------------------------------------------------------
	//---------------------------------------------------------
	//	global methods to be called on alerts
	//---------------------------------------------------------
	//---------------------------------------------------------
	public static void CallAlert(Alert alertToCall){
		//Debug.Log ("starting an alert");
		alertToCall.ActivateAlert ();
		currentActiveAlerts.Add (alertToCall);
	}
	
	public static void ClearAlert(Alert alertToClear){
		//Debug.Log ("clearing alert");
		alertToClear.ResolveAlert ();
		currentActiveAlerts.Remove (alertToClear);
	}

	//---------------------------------------------------------
	//---------------------------------------------------------
	//	class randomly calls alerts forever unless told otherwise
	//---------------------------------------------------------
	//---------------------------------------------------------
	void Start(){
		lastCurveIncrement = Time.time;
		betweenCurveIncrement = 10.0f;
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
				incrementedAlert = true;
				SetupRandomAlert();
			}
			if(Time.time - randomAlertStart > randomAlertTime) {
				//Debug.Log("notice with annoyance level " + randomAnnoyanceLevel);

				// we're playing a different alert based on if it's coming from
				// a superior or subordinate sender, as well as shifting pitch 
				// based on their specific ranking in those categories
				if(superiorOrSubordinate == 0 ) {
					sendNotice.superior = true;
				} else {
					sendNotice.superior = false;
				}
				sendNotice.rankMod = superiorLevel;
				sendNotice.play = true; 
				newAlert = true;
				//Debug.Log("alert");
			}

			if(Time.time - lastCurveIncrement > betweenCurveIncrement && incrementedAlert){
				//Debug.Log("incrementing curve pos to " + annoyanceTimeElapse);
				annoyanceTimeElapse += 0.1f;
				lastCurveIncrement = Time.time;
				incrementedAlert = false;
			}
		}
	}

	void SetupRandomAlert(){
		//min and max annoyance move across animation curve

		minAnnoyance = (annoyanceCurve.Evaluate(annoyanceTimeElapse - 0.1f));
		maxAnnoyance = annoyanceCurve.Evaluate(annoyanceTimeElapse);

		superiorOrSubordinate = Random.Range (0, 1);
		superiorLevel = Random.Range (0, 3f);
		randomAlertID = Random.Range (0, randomAlerts.Count);
		randomAnnoyanceLevel = Random.Range (minAnnoyance, maxAnnoyance);
		float annoyanceMod = Mathf.Abs(0.9f-randomAnnoyanceLevel);
		randomAlertTime = Random.Range (minAlertTime*annoyanceMod, maxAlertTime*annoyanceMod);
		//REPLACE THIS conidional to just add the length of the alert and message being played
		if (randomAlertTime < 2.0f)
						randomAlertTime += 1.5f;
		//Debug.Log ("random time chosen: " + randomAlertTime);
		newAlert = false;
		randomAlertStart = Time.time;
		//Debug.Log (randomAnnoyanceLevel);
		//Debug.Log ("new alert in " + randomAlertTime);

		//when at the end of the curve, queue the quiet on set script
		if (annoyanceTimeElapse > 1f) {
			//Debug.LogWarning("quiet on set running");
			setOffRandomAlert = false;
			queueRecording.activate = true;
		}
	}

	public float getAnnoyanceLevel() {
		return randomAnnoyanceLevel;
	}
}
