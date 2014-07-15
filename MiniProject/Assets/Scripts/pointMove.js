#pragma strict
private var position : Vector3;
private var moveRate : float;

private var parent;

function Start () {
	//position = this.transform.position.Set;
	moveRate = 3.0;
}

function Update () {
	this.transform.position.x -= moveRate * Time.deltaTime;
}