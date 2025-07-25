using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using PDollarGestureRecognizer;
using Random = System.Random;
//using Random = UnityEngine.Random;


public class Demo : MonoBehaviour {
    Random rand = new Random();
    public Transform gestureOnScreenPrefab;
	[SerializeField] private Image gestureImg;

	private List<Gesture> trainingSet = new List<Gesture>();

	private List<Point> points = new List<Point>();
	private int strokeId = -1;

	private Vector3 virtualKeyPosition = Vector2.zero;
	private Rect drawArea;

	private RuntimePlatform platform;
	private int vertexCount = 0;

	private List<LineRenderer> gestureLinesRenderer = new List<LineRenderer>();
	private LineRenderer currentGestureLineRenderer;

	//GUI
	private string message;
	private bool recognized;
	private string newGestureName = "";
	private int counter = 0;
	[SerializeField] List<Sprite> imgs;
	[Header("Game Settings")]
	public float enemydamage = 0.2f;
	public float playerdamage = 0.2f;


	void Start () {
        counter = rand.Next(0, imgs.Count);
        gestureImg.sprite = imgs[counter];
        platform = Application.platform;
		drawArea = new Rect(0, 0, Screen.width-Screen.width/3, Screen.height);
		Debug.Log(drawArea.center);
		//Load pre-made gestures
		TextAsset[] gesturesXml = Resources.LoadAll<TextAsset>("GestureSet/10-stylus-MEDIUM/");
		foreach (TextAsset gestureXml in gesturesXml)
			trainingSet.Add(GestureIO.ReadGestureFromXML(gestureXml.text));

		//Load user custom gestures
		string[] filePaths = Directory.GetFiles(Application.persistentDataPath, "*.xml");
		foreach (string filePath in filePaths)
			trainingSet.Add(GestureIO.ReadGestureFromFile(filePath));
		
	}

	void Update () {

		if (platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer) {
			if (Input.touchCount > 0) {
				virtualKeyPosition = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
			}
		} else {
			if (Input.GetMouseButton(0) && Input.mousePosition.y>231) {
				virtualKeyPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
				//Debug.Log(Input.mousePosition.x + " " + Input.mousePosition);
                
            }
		}

		if (drawArea.Contains(virtualKeyPosition)) {
			//Debug.Log(drawArea.position);
			if (Input.GetMouseButtonDown(0)) {

				if (recognized) {

					recognized = false;
					strokeId = -1;

					points.Clear();

					foreach (LineRenderer lineRenderer in gestureLinesRenderer) {

						lineRenderer.SetVertexCount(0);
						Destroy(lineRenderer.gameObject);
					}

					gestureLinesRenderer.Clear();
				}

				++strokeId;
				
				Transform tmpGesture = Instantiate(gestureOnScreenPrefab, transform.position, transform.rotation) as Transform;
				currentGestureLineRenderer = tmpGesture.GetComponent<LineRenderer>();
				
				gestureLinesRenderer.Add(currentGestureLineRenderer);
				
				vertexCount = 0;
			}
			
			if (Input.GetMouseButton(0)) {
				points.Add(new Point(virtualKeyPosition.x, -virtualKeyPosition.y, strokeId));

				currentGestureLineRenderer.SetVertexCount(++vertexCount);
				currentGestureLineRenderer.SetPosition(vertexCount - 1, Camera.main.ScreenToWorldPoint(new Vector3(virtualKeyPosition.x, virtualKeyPosition.y, 1)));
			}
		}
	}

	public void Open()
	{
		this.gameObject.SetActive(true);
	}

	public void Close()
	{
        this.gameObject.SetActive(false);
    }

	public void OnRecognize()
	{
        recognized = true;
        Gesture candidate = new Gesture(points.ToArray());
        Result gestureResult = PointCloudRecognizer.Classify(candidate, trainingSet.ToArray());
        message = gestureResult.GestureClass + " " + gestureResult.Score;
		Debug.Log(message);
        if (gestureResult.GestureClass == imgs[counter].name)
        {
			
                counter= rand.Next(0, imgs.Count);
				Messenger.Broadcast(GameEvent.PICK_TIME);
				Messenger.Broadcast(GameEvent.CALC_SCORE);
                Messenger<float>.Broadcast(GameEvent.TIMER_START, DataHolder.difficultySettings[0]);
           
            
            gestureImg.sprite = imgs[counter];
            Messenger<float>.Broadcast(GameEvent.CORRECT_ANSW, DataHolder.difficultySettings[1]);
			
            Debug.Log(counter);
        }
        else
        {
            Messenger<float>.Broadcast(GameEvent.MISTAKE, DataHolder.difficultySettings[2]);
        }
        
    }

    private void OnDisable()
    {

        recognized = false;
        strokeId = -1;

        points.Clear();

        foreach (LineRenderer lineRenderer in gestureLinesRenderer)
        {

            lineRenderer.SetVertexCount(0);
            Destroy(lineRenderer.gameObject);
        }
        gestureLinesRenderer.Clear();
    }
}
