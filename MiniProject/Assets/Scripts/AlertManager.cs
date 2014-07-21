using UnityEngine;
using System.Collections;

public class AlertManager: MonoBehaviour {

	//BRIAN, use randomAnnoyanceLevel for the values you need

	public Alert[] alerts;
	private ArrayList randomAlerts = new ArrayList();
	private static ArrayList currentActiveAlerts = new ArrayList();
	public float minAlertTime, maxAlertTime;
	private bool pickRandomAlert;
	public bool setOffRandomAlert = false;
	private bool newAlert = true;
	private int randomAlertID;
	private float randomAlertTime, randomAlertStart, annoyanceTimeElapse;
	public float randomAnnoyanceLevel;
	private float minAnnoyance, maxAnnoyance;
	private float superiorLevel;
	private int superiorOrSubordinate; 
	public AnimationCurve annoyanceCurve;
	private float lastCurveIncrement, betweenCurveIncrement;
	private bool incrementedAlert = true;

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
		//debug and presentation controlls
		if(Input.GetKeyDown("p")){ // turn alerts on and off
			setOffRandomAlert = !setOffRandomAlert;
		}
		if(Input.GetKeyDown("o")){ // progress forward and backward through annoyance animation curve
			annoyanceTimeElapse += 0.1f;
			if(annoyanceTimeElapse > 1.0f) annoyanceTimeElapse = 1.0f;
		}
		if(Input.GetKeyDown("i")){ 
			annoyanceTimeElapse -= 0.1f;
			if (annoyanceTimeElapse < 0f) annoyanceTimeElapse = 0f;
		}
		if(Input.GetKeyDown("l")){ // test the camera movement request
			CallAlert(alerts[0]);
		}
		if(Input.GetKeyDown("m")){ // call quiet on set, conclude game
			setOffRandomAlert = false;
			queueRecording.activate = true;
		}

		//set up and call random alerts
		if(setOffRandomAlert) {
			if(newAlert){
				incrementedAlert = true;
				SetupRandomAlert();
			}
			if(Time.time - randomAlertStart > randomAlertTime && setOffRandomAlert) {
				Debug.Log("playing a new random alert");
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

		if(annoyanceTimeElapse == 0.3f){
			setOffRandomAlert = false;
			StartCoroutine(CallLocationAfterAlert());
		}
		
		//when at the end of the curve, queue the quiet on set script
		if (annoyanceTimeElapse > 1f) {
			//Debug.LogWarning("quiet on set running");
			setOffRandomAlert = false;
			queueRecording.activate = true;
		}

		//min and max annoyance move across animation curve
		minAnnoyance = (annoyanceCurve.Evaluate(annoyanceTimeElapse - 0.1f));
		maxAnnoyance = annoyanceCurve.Evaluate(annoyanceTimeElapse);

		superiorOrSubordinate = Random.Range (0, 1);
		superiorLevel = Random.Range (1f, 3f);
		randomAlertID = Random.Range (0, randomAlerts.Count);
		randomAnnoyanceLevel = Random.Range (minAnnoyance, maxAnnoyance);
		float annoyanceMod = Mathf.Abs(0.9f-randomAnnoyanceLevel);
		randomAlertTime = Random.Range (minAlertTime*annoyanceMod, maxAlertTime*annoyanceMod);
		//set annoyance level to determine audio clip
		if (randomAnnoyanceLevel < 0.2f){
			sendNotice.annoyanceLevel = 0;
		} else if (randomAnnoyanceLevel < 0.6) {
			sendNotice.annoyanceLevel = 1;
		} else {
			sendNotice.annoyanceLevel = 2;
		}
		//add length of alert + voice clips to the time when the next random alert will go off
//		randomAlertTime += sendNotice.superiorSound.length + sendNotice.annoyanceClips [sendNotice.annoyanceLevel].length;
		randomAlertTime += sendNotice.superiorSound.length + 5F;

		//Debug.Log ("random time chosen: " + randomAlertTime);
		newAlert = false;
		randomAlertStart = Time.time;
		//Debug.Log (randomAnnoyanceLevel);
		//Debug.Log ("new alert in " + randomAlertTime);
	
	}

	public float getAnnoyanceLevel() {
		return randomAnnoyanceLevel;
	}

	IEnumerator CallLocationAfterAlert(){
		yield return new WaitForSeconds (sendNotice.superiorSound.length + 5f);
		CallAlert(alerts[0]);
	}

}
