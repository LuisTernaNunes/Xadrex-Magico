using UnityEngine;
using System;
using System.Collections;
using jp.nyatla.nyartoolkit.cs.markersystem;
using jp.nyatla.nyartoolkit.cs.core;
using NyARUnityUtils;
using System.IO;

public class CalibraTabuleiro : MonoBehaviour
{
	private NyARUnityMarkerSystem _ms;
	private NyARUnityWebCam _ss;
	public int i = 1;
	private GameObject _bg_panel;
	private string[] Mensagens = new string[100];
	public Font FonteLabel;
	public Texture ImgTexture;
	GUIStyle FontStyle = new GUIStyle();

	CriaTabuleiro tabu = new CriaTabuleiro ();
	public Vector3[,] Tabuleiro = new Vector3[9,9];

	private string strPathFile = @"C:\Users\Luis\Documents\XadrezMagico\Assets\Data\Calibragem.txt";
	
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
		tabu.SetTabuleiro ();
		Tabuleiro = tabu.GetTabuleiro ();
		Mensagens [1] = "Bem Vindo ao Sistema \n de Calibragem do Tabuleiro";
		Mensagens [2] = "A calibragem Garante \n uma melhor Jogabilidade";
		Mensagens [3] = "Posicione o Tabuleiro e a Camera de forma \n que os numeros fiquem cobrindo as casas do\n canto do Tabuleiro";
		Mensagens [4] = "Agora seu Tabuleiro \n esta Calibrado !!!";
		//CriaArquivo ();

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

	}
	void CriaImagens(){
		Vector3 x1 = new Vector3 (145, -142,608);
		Vector3 x2 = new Vector3 (-150,-142,608);
		Vector3 x3 = new Vector3 (-150,142,608);
		Vector3 x4 = new Vector3 (145,144,608);
		GameObject.Find ("Mark1").transform.position = Tabuleiro[0,0];
		GameObject.Find ("Mark2").transform.position = Tabuleiro[7,0];
		GameObject.Find ("Mark3").transform.position = Tabuleiro[7,7];
		GameObject.Find ("Mark4").transform.position = Tabuleiro[0,7];
	}
	void ApagaImagens(){
		Vector3 x1 = new Vector3 (0, 0,0);
		Vector3 x2 = new Vector3 (0,0,0);
		Vector3 x3 = new Vector3(0,0,0);
		Vector3 x4 = new Vector3 (0,0,0);
		GameObject.Find ("Mark1").transform.position = x1;
		GameObject.Find ("Mark2").transform.position = x2;
		GameObject.Find ("Mark3").transform.position = x3;
		GameObject.Find ("Mark4").transform.position = x4;
	}


	void OnGUI(){
		FontStyle.font = FonteLabel;
		FontStyle.normal.textColor = Color.black;
		if (i == 3) {
			FontStyle.fontSize = Screen.height / 30;
		} else {
			FontStyle.fontSize = Screen.height / 20;
		}
		if(i != 5){
			GUI.DrawTexture (new Rect (Screen.width/4,Screen.height/9,Screen.width/2,Screen.height/3),ImgTexture);
			GUI.Label (new Rect (Screen.width/4 + Screen.width/15,Screen.height/9 + Screen.height/9,Screen.width/2,Screen.height/3),Mensagens[i],FontStyle);
			if (i == 4) {
				if (GUI.Button (new Rect (Screen.width / 2 , Screen.height / 3, Screen.width /10, Screen.height / 10), "Finalizar",GUIStyle.none)) {
					i++;
					//ApagaImagens();
					Application.LoadLevel(0);
				}
			} else {
				if (GUI.Button (new Rect (Screen.width / 2 , Screen.height / 3, Screen.width /10, Screen.height / 10),"Proximo ->",GUIStyle.none)) {
					i++;
					if (i == 3) {
						CriaImagens ();
						Debug.Log(Tabuleiro[0,0]);
					}
				}
			}
			if (GUI.Button (new Rect (Screen.width / 2 - Screen.width /9, Screen.height / 3, Screen.width /10, Screen.height / 10), "Anterior ->",GUIStyle.none)) {
				if(i > 1){
						i--;
				}
			}
		}
	}
}

