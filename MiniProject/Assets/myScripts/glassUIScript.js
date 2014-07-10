#pragma strict

public var glassCamera: GameObject;

public var pointPrefab1:GameObject;
public var pointPrefab2:GameObject;
private var pointArray : Array;
private var timeSinceLastCreate: float;

// baseline
private var active1: boolean;
private var yOffset1: float;
private var scatterLevel1: float;

// active test
private var active2: boolean;
private var yOffset2: float;
private var scatterLevel2: float;

function Start () {
	// glass camera set to fixed size
	//glassCamera.camera.pixelRect = new Rect(Screen.width - 0.7658, Screen.height - 0.745, 0.23, 0.25);
	
	pointArray = new Array();
	timeSinceLastCreate = 0;
	
	// baseline
	active1 = true;
	yOffset1 = 0.0;
	scatterLevel1 = 0.2;
	//active test
	active2 = false;
	yOffset2 = 0.0;
	scatterLevel2 = 0.2;
}

function Update () {
	timeSinceLastCreate += Time.deltaTime;
	if (timeSinceLastCreate >= .10) {
		timeSinceLastCreate = 0;
		var x = this.transform.position.x + 7.0;
		var y = this.transform.position.y;
		var z = this.transform.position.z;
		
		// init scattering vars
		// baseline
		var posOrNeg1 = Random.value;
		var scatter1 = Random.value;
		if (posOrNeg1 > 0.5) // so that scattering happend above and below the baseline.
			scatter1 *= -1;
		//active
		var posOrNeg2 = Random.value;
		var scatter2 = Random.value;
		if (posOrNeg2 > 0.5) // so that scattering happend above and below the baseline.
			scatter2 *= -1;
			
		// baseline
		var newPoint1 = null;
		if (active1) {
			newPoint1 = Instantiate(pointPrefab1, Vector3(x, y, z+scatter1*scatterLevel1+yOffset1), Quaternion.identity);
			pointArray.unshift(newPoint1);
		}
		
		// active test
		var newPoint2 = null;
		if (active2) {
			newPoint2 = Instantiate(pointPrefab2, Vector3(x, y, z+scatter2*scatterLevel2+yOffset2), Quaternion.identity);
			pointArray.unshift(newPoint2);
		}
		
		// kill all points that have reached beyond the left margin
		for (var i = 0; i < pointArray.length; i++) {
			var moo: GameObject = pointArray[i];
			if (moo.transform.position.x < this.transform.position.x - 7.0) {
				pointArray.splice(i, 1);
				Destroy(moo);
			}
		}
	}
}

// managing chart state

// draw or not to draw the line?
function setLineActive1(active : boolean)
{
	active1 = active;
}

function setLineActive2(active : boolean)
{
	active2 = active;
}

// set the offset from middle of the UI screen
function setLineYOffset1(yOffset : float)
{
	yOffset1 = yOffset;
}

function setLineYOffset2(yOffset: float)
{
	yOffset2 = yOffset;
}

// set the scatter level for the lines
function setScatterLevel1(scatter: float)
{
	scatterLevel1 = scatter;
}

function setScatterLevel2(scatter: float)
{
	scatterLevel2 = scatter;
}


