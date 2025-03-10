using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class MenuGame : MonoBehaviour {

	public Texture fundoMenu;
	public Texture options = new Texture();
	GUIStyle ButtonStyle = new GUIStyle();

	int i = 0;
	public bool menu = false;
	public int esc = 0;

	MenuPrincipal config = new MenuPrincipal ();

	public Font FonteLabel;
	GUIStyle FontStyle = new GUIStyle();
	public GameObject ArCam;
	public GameObject Tutorial;
	SaveGame saveGame = new SaveGame ();
	String[,] jogosSalvos = new String[4,4];

	void Start () {
		if (PlayerPrefs.GetInt ("menuPrincipal") == 1) {
			i = 3;
			menu = true;
		}
		config.BuscaInformacao();
	}

	void Update () {
		if (Input.GetKey (KeyCode.Escape)) {
			if(esc == 0){
				esc = 1;
				menu = true;
			}
		}
		saveGame.BuscaJogos();
		jogosSalvos = saveGame.getJogos();	
	}
	void OnGUI(){
	
		ButtonStyle.font = FonteLabel;
		ButtonStyle.fontSize = Screen.width / 40;
		FontStyle.font = FonteLabel;
		FontStyle.fontSize = Screen.width / 25;

		if (menu) {
			if(i == 0){
			GUI.DrawTexture (new Rect (Screen.width / 4, Screen.height / 15, Screen.width / 2, Screen.height / 2 +  Screen.height / 3), fundoMenu);
			GUI.Label (new Rect (Screen.width / 3 + Screen.width / 20, Screen.height / 6, 1,1 ),"Menu Game",FontStyle);
			if (GUI.Button (new Rect (Screen.width / 3 + Screen.width / 15, Screen.height / 4 +  Screen.height / 20, Screen.width / 10 ,Screen.height / 20 ),"Novo Jogo",FontStyle)) {
					Tutorial.GetComponent<Tutorial> ().setNewGame();
			}
			if (GUI.Button (new Rect (Screen.width / 3 + Screen.width / 10, Screen.height / 4 + ((Screen.height / 20) * 3), Screen.width / 10 ,Screen.height / 20 ),"Salvar",FontStyle)) {
					i = 2;
			}
				if (GUI.Button (new Rect (Screen.width / 3 + Screen.width / 30, Screen.height / 2, Screen.width / 4 + Screen.width / 30 ,Screen.height / 20),"Configuraçoes",FontStyle)) {
					i = 3;
			}
			if (GUI.Button (new Rect (Screen.width / 3 + Screen.width / 8, Screen.height / 2 + Screen.height / 20 * 2, Screen.width / 10 ,Screen.height / 20),"Sair",FontStyle)) {
					Application.Quit();
			}
			if (GUI.Button (new Rect (Screen.width / 3,Screen.height / 2 + Screen.height / 6 + Screen.height / 15, Screen.width / 10 ,Screen.height / 20 ),"Voltar",FontStyle)) {
				menu = false;
				esc = 0;
			}
			}
			if(i == 3){
				if(ArCam.GetComponent<ARCamera> ().calibra == true){

				}else{
					config.menu = ArCam;
					config.game = true;
					config.ButtonStyle = ButtonStyle;
					config.Options = options;
					config.Configuracao();	
					i = config.getI();
				}
			}
			if(i == 2){
				if(i == 2){
					GUI.DrawTexture (new Rect (Screen.width / 3, Screen.height /4, Screen.width / 3, Screen.height / 2), options);
					GUI.Label (new Rect (Screen.width /3 + Screen.width / 10, Screen.height / 4 + Screen.height / 25, Screen.width / 10, Screen.height / 15), "Jogos Salvos", ButtonStyle);
					if(GUI.Button (new Rect (Screen.width /3 + Screen.width / 13, Screen.height / 4 + Screen.height / 7, Screen.width / 10, Screen.height / 15),"Save 1 : "+jogosSalvos[1,3],ButtonStyle)){
						saveGame.SalvaJogo(Tutorial.GetComponent<Tutorial> ().getLevel(),1);
					}
					if(GUI.Button (new Rect (Screen.width /3 + Screen.width / 13, Screen.height / 4 + Screen.height / 5, Screen.width / 10, Screen.height / 15),"Save 2 : "+jogosSalvos[2,3],ButtonStyle)){
						saveGame.SalvaJogo(Tutorial.GetComponent<Tutorial> ().getLevel(),2);
					}
					if(GUI.Button (new Rect (Screen.width /3 + Screen.width / 13, Screen.height / 4 + Screen.height / 5 + Screen.height / 17, Screen.width / 10, Screen.height / 15),"Save 3 : "+jogosSalvos[3,3],ButtonStyle)){
						saveGame.SalvaJogo(Tutorial.GetComponent<Tutorial> ().getLevel(),3);
					}
					if(GUI.Button (new Rect (Screen.width /3 + Screen.width / 13, Screen.height / 4 + Screen.height / 4 + Screen.height / 16, Screen.width / 10, Screen.height / 15),"Save 4 : "+jogosSalvos[4,3],ButtonStyle)){
						saveGame.SalvaJogo(Tutorial.GetComponent<Tutorial> ().getLevel(),4);
					}
					
					if(GUI.Button (new Rect (Screen.width /6 + Screen.width / 3 + Screen.width / 18,Screen.height / 3 + Screen.height / 3,Screen.width / 10, Screen.height / 15),"Voltar",ButtonStyle)){
						i = 0;
					}
				}
			}
			//Debug.Log(Screen.height);
	}
	}
}
