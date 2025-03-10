using UnityEngine;
using System;
using System.Collections;
using jp.nyatla.nyartoolkit.cs.markersystem;
using jp.nyatla.nyartoolkit.cs.core;
using NyARUnityUtils;
using System.IO;
using System.Timers;

public class ARCamera : MonoBehaviour
{
	private NyARUnityMarkerSystem _ms;
	private NyARUnityWebCam _ss;
	private int Marker_Peao;
	private int Marker_King;
	private int Marker_Queen;
	private int Marker_Torre;
	private int Marker_Bispo;
	private int Marker_Horse;
	public bool Peao = false;
	public bool King;
	public bool Queen;
	public bool Torre;
	public bool Bispo;
	public bool Horse;

	//Timer
	float timer;
	float timerLimite = 5;

	//Animacao Personagens
	public GameObject AnimaPeao;
	public GameObject prefabsPeao;

	//Calibra Tabuleiro
	public bool calibra = false;
	private string[] Mensagens = new string[100];
	public Font FonteLabel;
	public Texture ImgTexture;
	GUIStyle FontStyle = new GUIStyle();
	CriaTabuleiro tabu = new CriaTabuleiro ();
	public Vector3[,] Tabuleiro = new Vector3[9, 9];
	public int i = 1;

	//Fim teste Tabulerio
	private GameObject _bg_panel;

	void Awake()
	{
		//setup unity webcam
		WebCamDevice[] devices= WebCamTexture.devices;
		if (devices.Length<=0){
			Debug.LogError("No Webcam.");
			return;
		}
		WebCamTexture w=new WebCamTexture(320,240,30);
		//Make WebcamTexture wrapped Sensor.
		this._ss=NyARUnityWebCam.createInstance(w);
		//Make configulation by Sensor size.
		NyARMarkerSystemConfig config = new NyARMarkerSystemConfig(this._ss.width,this._ss.height);
		this._ms=new NyARUnityMarkerSystem(config);
		//mid=this._ms.addARMarker("./Assets/Data/patt.hiro",16,25,80);
		//This line loads a marker from texture



		Marker_Peao=this._ms.addARMarker(
			new StreamReader(new MemoryStream(((TextAsset)Resources.Load("Peao1",typeof(TextAsset))).bytes)),
			16,25,50);
		Marker_King=this._ms.addARMarker(
			new StreamReader(new MemoryStream(((TextAsset)Resources.Load("King",typeof(TextAsset))).bytes)),
			16,25,50);
		Marker_Queen=this._ms.addARMarker(
			new StreamReader(new MemoryStream(((TextAsset)Resources.Load("Queen",typeof(TextAsset))).bytes)),
			16,25,50);
		Marker_Horse=this._ms.addARMarker(
			new StreamReader(new MemoryStream(((TextAsset)Resources.Load("Horse",typeof(TextAsset))).bytes)),
			16,25,50);
		Marker_Torre=this._ms.addARMarker(
			new StreamReader(new MemoryStream(((TextAsset)Resources.Load("Torre",typeof(TextAsset))).bytes)),
			16,25,50);

		//setup background
		this._bg_panel=GameObject.Find("Plane");
		this._bg_panel.GetComponent<Renderer>().material.mainTexture=w;
		this._ms.setARBackgroundTransform(this._bg_panel.transform);
		
		//setup camera projection
		this._ms.setARCameraProjection(this.GetComponent<Camera>());
		return;
	}
	// Use this for initialization
	void Start ()
	{
		if(PlayerPrefs.GetInt("menuPrincipal") == 1){
			calibra = true;
		}
		tabu.SetTabuleiro ();
		Tabuleiro = tabu.GetTabuleiro ();
		Mensagens [1] = "Bem Vindo ao Sistema \n de Calibragem do Tabuleiro";
		Mensagens [2] = "A calibragem Garante \n uma melhor Jogabilidade";
		Mensagens [3] = "Posicione o Tabuleiro e a Camera de forma \n que os numeros fiquem cobrindo as casas do\n canto do Tabuleiro";
		Mensagens [4] = "Agora seu Tabuleiro \n esta Calibrado !!!";

		//inicia a Realidade almentada
		this._ss.start();
	}
	// Update is called once per frame
	void Update ()
	{
		//Update SensourSystem
		this._ss.update ();
		//Update marker system by ss
		this._ms.update (this._ss);
		if (calibra == false) {
			// Inicio do carregamento dos personagens
			if (this._ms.isExistMarker (Marker_Peao)) {
				Quaternion q = new Quaternion ();
				Vector3 Position = new Vector3 ();
				this._ms.getMarkerTransform (Marker_Peao, ref Position, ref q);
				Position.z = 810;
				if (GameObject.Find ("Marker_Peao").transform.position.x == 0f) {
					GameObject.Find ("Marker_Peao").transform.position = Position;
					Instantiate (prefabsPeao, Position, transform.rotation);
				} else {
					GameObject.Find ("ColisorPai 1(Clone)").transform.position = Position;
					GameObject.Find ("Marker_Peao").transform.position = Vector3.Slerp (GameObject.Find ("Marker_Peao").transform.position, Position, Time.deltaTime * 0.7f);
					if(AnimaPeao.GetComponent<Collider> ().getTrigerInimigo())
					{
						AnimaPeao.GetComponent<Anima> ().animacao(3);
					}else{
					if (AnimaPeao.GetComponent<Collider> ().getTrigerAnima ()){
							if(timer <= 2){
								AnimaPeao.GetComponent<Anima> ().animacao(1);
								timer = timerLimite;
							}else{
								timer -= Time.deltaTime;
							}
					} else {
						AnimaPeao.GetComponent<Anima> ().animacao(2);
					}
					}				
				}
				//GameObject.Find("Marker_Peao").transform.rotation = q;
				//Transform t=GameObject.Find("Marker_Peao").transform;
				Peao = true;
			} else {
				AnimaPeao.GetComponent<Collider> ().setTrigerAnima ();
				Peao = false;
			}

			if (this._ms.isExistMarker (Marker_King)) {
				Quaternion q = new Quaternion ();
				Vector3 Position = new Vector3 ();
				this._ms.getMarkerTransform (Marker_King, ref Position, ref q);
				if (GameObject.Find ("Marker_King").transform.position.x == 0f) {
					GameObject.Find ("Marker_King").transform.position = Position;
				} else {
					GameObject.Find ("Marker_King").transform.position = Vector3.Slerp (GameObject.Find ("Marker_King").transform.position, Position, Time.deltaTime * 6f);

				}
				//GameObject.Find("Marker_King").transform.rotation = q;
				//Transform t=GameObject.Find("Marker_King").transform;
			} else {

			}
			//marker 2
			if (this._ms.isExistMarker (Marker_Queen)) {
				Quaternion q = new Quaternion ();
				Vector3 Position = new Vector3 ();
				this._ms.getMarkerTransform (Marker_Queen, ref Position, ref q);
				if (GameObject.Find ("Marker_Queen").transform.position.x == 0f) {
					GameObject.Find ("Marker_Queen").transform.position = Position;
				} else {
					GameObject.Find ("Marker_Queen").transform.position = Vector3.Slerp (GameObject.Find ("Marker_Queen").transform.position, Position, Time.deltaTime * 6f);
				
				}
				//GameObject.Find("Marker_Queen").transform.rotation = q;
				//Transform t=GameObject.Find("Marker_Queen").transform;
			} else {

			}

			if (this._ms.isExistMarker (Marker_Torre)) {
				Quaternion q = new Quaternion ();
				Vector3 Position = new Vector3 ();
				this._ms.getMarkerTransform (Marker_Torre, ref Position, ref q);
				if (GameObject.Find ("Marker_Torre").transform.position.x == 0f) {
					GameObject.Find ("Marker_Torre").transform.position = Position;
				} else {
					GameObject.Find ("Marker_Torre").transform.position = Vector3.Slerp (GameObject.Find ("Marker_Torre").transform.position, Position, Time.deltaTime * 6f);
				
				}
				GameObject.Find ("Marker_Torre").transform.rotation = q;
				//Transform t=GameObject.Find("Marker_Torre").transform;
			}
			if (this._ms.isExistMarker (Marker_Horse)) {
				Quaternion q = new Quaternion ();
				Vector3 Position = new Vector3 ();
				this._ms.getMarkerTransform (Marker_Horse, ref Position, ref q);
				if (GameObject.Find ("Marker_Horse").transform.position.x == 0f) {
					GameObject.Find ("Marker_Horse").transform.position = Position;
				} else {
					GameObject.Find ("Marker_Horse").transform.position = Vector3.Slerp (GameObject.Find ("Marker_Horse").transform.position, Position, Time.deltaTime * 6f);
				
				}
				GameObject.Find ("Marker_Horse").transform.rotation = q;
				//Transform t=GameObject.Find("Marker_Horse").transform;
			}
			c++;
		} else if (calibra == true) {
		
		}
	}
	static int c=0;

	void CriaImagens(){
		Vector3 x5 = new Vector3 (0,0,810);
		GameObject.Find ("Mark1").transform.position = Tabuleiro[0,0];
		GameObject.Find ("Mark2").transform.position = Tabuleiro[7,0];
		GameObject.Find ("Mark3").transform.position = Tabuleiro[7,7];
		GameObject.Find ("Mark4").transform.position = Tabuleiro[0,7];
		GameObject.Find ("Mark5").transform.position =  x5;
	}
	void ApagaImagens(){
		Vector3 x1 = new Vector3 (0, 0,0);
		Vector3 x2 = new Vector3 (0,0,0);
		Vector3 x3 = new Vector3(0,0,0);
		Vector3 x4 = new Vector3 (0,0,0);
		Vector3 x5 = new Vector3 (0,0,0);
		GameObject.Find ("Mark1").transform.position = x1;
		GameObject.Find ("Mark2").transform.position = x2;
		GameObject.Find ("Mark3").transform.position = x3;
		GameObject.Find ("Mark4").transform.position = x4;
		GameObject.Find ("Mark5").transform.position = x5;
	}

	void OnGUI(){
		if (calibra == true) {
			FontStyle.font = FonteLabel;
			FontStyle.normal.textColor = Color.black;
			if (i == 3) {
				FontStyle.fontSize = Screen.height / 30;
			} else {
				FontStyle.fontSize = Screen.height / 20;
			}
			if (i != 5) {
				GUI.DrawTexture (new Rect (Screen.width / 4, Screen.height / 9, Screen.width / 2, Screen.height / 3), ImgTexture);
				GUI.Label (new Rect (Screen.width / 4 + Screen.width / 15, Screen.height / 9 + Screen.height / 9, Screen.width / 2, Screen.height / 3), Mensagens [i], FontStyle);
				if (i == 4) {
					if (GUI.Button (new Rect (Screen.width / 2, Screen.height / 3, Screen.width / 10, Screen.height / 10), "Finalizar", GUIStyle.none)) {
						i++;
						ApagaImagens();
						calibra = false;
					}
				} else {
					if (GUI.Button (new Rect (Screen.width / 2, Screen.height / 3, Screen.width / 10, Screen.height / 10), "Proximo ->", GUIStyle.none)) {
						i++;
						if (i == 3) {
							CriaImagens ();
						}
					}
				}
				if (GUI.Button (new Rect (Screen.width / 2 - Screen.width / 9, Screen.height / 3, Screen.width / 10, Screen.height / 10), "Anterior ->", GUIStyle.none)) {
					if (i > 1) {
						i--;
					}
				}
			}
		}
	}

	}

