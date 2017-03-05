using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HexMapEditor : MonoBehaviour {

	public Color[] colors;
	public HexGrid hexgrid;
	private Color activeColor;

	void Awake(){
		SelectColor (0);
	}

	public void SelectColor(int index){
		activeColor = colors [index];
	}

	void HandleInput(){
		Ray inputRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (inputRay, out hit)) {
			hexgrid.ColorCell (hit.point, activeColor);
		}
	}

	void Update(){
		if (Input.GetMouseButton (0) && !EventSystem.current.IsPointerOverGameObject()) {
			HandleInput ();
		} 
	}
}
