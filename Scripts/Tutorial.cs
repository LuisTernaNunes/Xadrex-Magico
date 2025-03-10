using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class Tutorial : MonoBehaviour {

	private string[] Mensagens = new string[100];
	public GameObject MenuGame;
	private bool menu = false;
	public Font FonteLabel;
	public Vector3[,] Tabuleiro = new Vector3[9,9];
	public Texture ImgTexture;

	GUIStyle FontStyle = new GUIStyle();
	GUIStyle BoxStyle = new GUIStyle();

	private int level = 1;
	private int i = 1;

	CriaTabuleiro tabu = new CriaTabuleiro ();
	//modulos
	public GameObject Peao;
	public GameObject Torre;

	void Start () {
		if (PlayerPrefs.GetInt ("LoadGame") >= 1) {
			i = 4;
			level = PlayerPrefs.GetInt ("LoadGame");
			IniTutorial();
		}
		tabu.SetTabuleiro ();
		Tabuleiro = tabu.GetTabuleiro ();
		Mensagens [1] = "Bem Vindo ao Xadrez Magico !";
		Mensagens [2] = "O Sistema de Mensagens Avançara \n Sozinho de Forma que Voçe \ncunpra as missões !";
		Mensagens [3] = "Pronto Para Começar ?";
		Mensagens [4] = "";
		Mensagens [5] = "Fim de Jogo";
	}
	void Update () {
		menu = MenuGame.GetComponent<MenuGame> ().menu;
	}
	
	void OnGUI(){
		if(menu) {
		}else{
			FontStyle.font = FonteLabel;
			if(i == 1){
				FontStyle.fontSize = Screen.width / 35;
				GUI.DrawTexture (new Rect (Screen.width/4 - Screen.width/20,Screen.height/9,Screen.width/2 + Screen.width/10,Screen.height/2),ImgTexture);
				GUI.Label (new Rect (Screen.width/4 + Screen.width/20,Screen.height/9 + Screen.height/7,Screen.width/2,Screen.height/3),Mensagens[i],FontStyle);
				if (GUI.Button (new Rect (Screen.width / 2 + Screen.width /15, Screen.height / 3 + Screen.height / 15, Screen.width /10, Screen.height / 20), "Proximo ->",FontStyle)) {
				i++;
			}
			}else if (i < 4){
				FontStyle.fontSize = Screen.width / 40;
				GUI.DrawTexture (new Rect (Screen.width/4,0,Screen.width/2 + Screen.width/15,Screen.height/3 + Screen.height/15),ImgTexture);
				GUI.skin.label.fontSize = Screen.height / 30;
				GUI.Label (new Rect (Screen.width/4 + Screen.width/15,Screen.height/12,Screen.width/2,Screen.height/3),Mensagens[i],FontStyle);
			}
			if(i == 3){
				if (GUI.Button (new Rect (Screen.width / 2 + Screen.width /12, Screen.height / 5 + Screen.height / 25, Screen.width / 10, Screen.height / 25), "Começar",FontStyle)) {
					i++;
					IniTutorial();
			}}
			if(i == 2){
				if (GUI.Button (new Rect (Screen.width / 2 + Screen.width /12, Screen.height / 5 + Screen.height / 25, Screen.width / 10, Screen.height / 25), "Proximo ->",FontStyle)) {
				i++;
			}}
		}
	}
	void IniTutorial(){
		if(level == 1){
			Peao.GetComponent<Peao>().setStart(true);
			if(Peao.GetComponent<Peao>().getStart() == false){
				level++;
			}
		}
		if(level == 2){
			Torre.GetComponent<Torre>().setStart(true);
			if(Torre.GetComponent<Torre>().getStart() == false){
				level++;
			}
		}
		if (level == 10) {
			i = 5;
		}
	}
	public int getLevel(){
		return level;
	}
	public void setNewGame(){
		i = 1;
		level = 1;
	}



}