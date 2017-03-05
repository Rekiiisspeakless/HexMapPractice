using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HexGrid : MonoBehaviour {
	public int height = 6;
	public int width = 6;

	public Color defaultColor = Color.white;
	public Color touchedColor = Color.magenta;

	public HexCell cellPrefab;
    public Text cellLabelPrefab;

    Canvas gridCanvas;
    HexMesh hexMesh;

    HexCell[] cells;


	public void ColorCell(Vector3 position, Color color){
		//transform world position to local position
		position = transform.InverseTransformPoint (position);
		HexCoordinates coordinates = HexCoordinates.FromPosition (position);
		Debug.Log ("touch at: " + position);

		int index = coordinates.X + coordinates.Z * width + coordinates.Z / 2;
		HexCell cell = cells [index];
		cell.color = color;
		hexMesh.Triangulate (cells);
	}

    void Awake() {
        gridCanvas = GetComponentInChildren<Canvas>();
        hexMesh = GetComponentInChildren<HexMesh>();

        cells = new HexCell[height * width];
        for (int z = 0, i = 0; z < height; ++z) {
            for(int x = 0; x < width; ++x)
            {
                CreateCell(x, z, i++);
            }
        }
    }

	void Start(){
		hexMesh.Triangulate (cells);
	}
		
    void CreateCell(int x, int z,int i) {
        //Vector3 position = new Vector3(x * 10f, 0f, z * 10f);
        Vector3 position = new Vector3((x + 0.5f * z - z / 2) * HexMatrics.innerRadius * 2f,
            0f, z * HexMatrics.outerRadius * 1.5f);

        HexCell cell = cells[i] = Instantiate<HexCell>(cellPrefab);
        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = position;
		cell.coordinates = HexCoordinates.FromOffsetCoordinates (x, z);
		cell.color = defaultColor;

        //position label
        Text label = Instantiate<Text>(cellLabelPrefab);
        label.rectTransform.SetParent(gridCanvas.transform, false);
        label.rectTransform.anchoredPosition = new Vector2(position.x, position.z);
		label.text = cell.coordinates.ToStringSeparateLines();
    }
}
