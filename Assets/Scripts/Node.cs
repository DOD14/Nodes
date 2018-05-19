using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Node : MonoBehaviour
{


    public Color myColor;
    public string inputText;

    private MeshRenderer meshRenderer;
    private Text myText;

    private bool touched = false;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        myText = GetComponentInChildren<Text>();
    }

    public void UpdateObjectWithInfo()
    {
        meshRenderer.material.color = myColor;
        myText.text = inputText;
    }

    private void OnMouseDown()
    {
        UpdateObjectWithInfo();
    }

	private void OnMouseDrag()
	{
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos = new Vector3(pos.x, pos.y, 0f);
        transform.position = pos;
	}

}