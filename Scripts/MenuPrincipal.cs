using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class MenuPrincipal : MonoBehaviour {

	public Texture fundoMenu = new Texture();
	public Texture ImgTexture = new Texture();
	public Texture Botao = new Texture();
	public Texture Options = new Texture();
	public Font FonteLabel;
	GUIStyle FontStyle = new GUIStyle();
	public GUIStyle ButtonStyle = new GUIStyle();
	int i = 0;
	float resolu;
	float volume;
	bool tela;
	Resolution resolucao;
	private string strPathFile = @"C:\Users\Luis\Documents\My Games\XadrezMagico\confg\Conf.Config";
	private string[] conf = new string[20];
	public bool game = false;
	public GameObject menu;
	SaveGame saveGame = new SaveGame ();

	void Start () {
		BuscaInformacao ();
		PlayerPrefs.SetInt("menuPrincipal",0);
	}

	void Update () {
	
	}
	void OnGUI(){
			ButtonStyle.font = FonteLabel;
			ButtonStyle.fontSize =  Screen.width / 70 + Screen.height / 70;
			FontStyle.font = FonteLabel;
			FontStyle.fontSize = Screen.width / 30 + Screen.height / 30;
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), fundoMenu);
		if (i == 0) {
			GUI.DrawTexture (new Rect (Screen.width / 4, 0, Screen.width / 2 + Screen.width / 18, Screen.height / 3 + Screen.height / 15), ImgTexture);
			GUI.Label (new Rect (Screen.width / 4 + Screen.width / 10, Screen.height / 12 + Screen.height / 20, Screen.width / 2, Screen.height / 3), "Xadrez Magico", FontStyle);


			if (GUI.Button (new Rect (Screen.width / 8, Screen.height / 2 - Screen.height / 30, Screen.width / 5, Screen.height / 10), Botao, ButtonStyle)) {
				i = 1;
			}
			GUI.Label (new Rect (Screen.width / 8 + Screen.width / 30, Screen.height / 2 - Screen.height / 60, Screen.width / 10, Screen.height / 15), "Novo Jogo", ButtonStyle);

			if (GUI.Button (new Rect (Screen.width / 3 + Screen.width / 14, Screen.height / 2 - Screen.height / 30, Screen.width / 5, Screen.height / 10), Botao, ButtonStyle)) {
				i = 2;
			}
			GUI.Label (new Rect (Screen.width / 3 + Screen.width / 11, Screen.height / 2 - Screen.height / 60, Screen.width / 10, Screen.height / 15), "Carregar Jogo", ButtonStyle);
			if (GUI.Button (new Rect (Screen.width - Screen.width / 8 - Screen.width / 5, Screen.height / 2 - Screen.height / 30, Screen.width / 5, Screen.height / 10), Botao, ButtonStyle)) {
				i = 3;
			}
			GUI.Label (new Rect (Screen.width - Screen.width / 7 - Screen.width / 6, Screen.height / 2 - Screen.height / 60, Screen.width / 10, Screen.height / 15), "Configuraçao", ButtonStyle);

			if (GUI.Button (new Rect (Screen.width - Screen.width / 5, Screen.height - Screen.height / 10, Screen.width / 5, Screen.height / 10), Botao, ButtonStyle)) {
				Application.Quit();
			}
			GUI.Label (new Rect (Screen.width - Screen.width / 8, Screen.height - Screen.height / 13, Screen.width / 10, Screen.height / 15), "Sair", ButtonStyle);
		}
		if (i == 1) {
			GUI.DrawTexture (new Rect (Screen.width / 3, Screen.height /4, Screen.width / 3, Screen.height / 2), Options);
			GUI.Label (new Rect (Screen.width /3 + Screen.width / 8, Screen.height / 4 + Screen.width / 25, Screen.width / 10, Screen.height / 15), "Nivel", ButtonStyle);
			if(GUI.Button (new Rect (Screen.width /3 + Screen.width / 9,Screen.height / 4 + Screen.width / 10,Screen.width / 10, Screen.height / 15),"Iniciante",ButtonStyle)){
				PlayerPrefs.SetInt("LoadGame",0);
				Application.LoadLevel ("Game");
			}
			if(GUI.Button (new Rect (Screen.width /3 + Screen.width / 5,Screen.height / 4 + Screen.width / 6,Screen.width / 10, Screen.height / 15),"Voltar",ButtonStyle)){
				i = 0;
			}
		}
		if(i == 2){
			GUI.DrawTexture (new Rect (Screen.width / 3, Screen.height /4, Screen.width / 3, Screen.height / 2), Options);
			GUI.Label (new Rect (Screen.width /3 + Screen.width / 10, Screen.height / 4 + Screen.height / 25, Screen.width / 10, Screen.height / 15), "Jogos Salvos", ButtonStyle);
			saveGame.BuscaJogos();
			String[,] jogosSalvos = new String[4,4];
			jogosSalvos = saveGame.getJogos();

			if(GUI.Button (new Rect (Screen.width /3 + Screen.width / 13, Screen.height / 4 + Screen.height / 7, Screen.width / 10, Screen.height / 15),"Save 1 : "+jogosSalvos[1,3],ButtonStyle)){
				PlayerPrefs.SetInt("LoadGame", Convert.ToInt32(jogosSalvos[1,2]));
				Application.LoadLevel ("Game");
			}
			if(GUI.Button (new Rect (Screen.width /3 + Screen.width / 13, Screen.height / 4 + Screen.height / 5, Screen.width / 10, Screen.height / 15),"Save 2 : "+jogosSalvos[2,3],ButtonStyle)){
				PlayerPrefs.SetInt("LoadGame", Convert.ToInt32(jogosSalvos[2,2]));
				Application.LoadLevel ("Game");
			}
			if(GUI.Button (new Rect (Screen.width /3 + Screen.width / 13, Screen.height / 4 + Screen.height / 5 + Screen.height / 17, Screen.width / 10, Screen.height / 15),"Save 3 : "+jogosSalvos[3,3],ButtonStyle)){
				PlayerPrefs.SetInt("LoadGame", Convert.ToInt32(jogosSalvos[3,2]));
				Application.LoadLevel ("Game");
			}
			if(GUI.Button (new Rect (Screen.width /3 + Screen.width / 13, Screen.height / 4 + Screen.height / 4 + Screen.height / 16, Screen.width / 10, Screen.height / 15),"Save 4 : "+jogosSalvos[4,3],ButtonStyle)){
				PlayerPrefs.SetInt("LoadGame", Convert.ToInt32(jogosSalvos[4,2]));
				Application.LoadLevel ("Game");
			}

			if(GUI.Button (new Rect (Screen.width /6 + Screen.width / 3 + Screen.width / 18,Screen.height / 3 + Screen.height / 3,Screen.width / 10, Screen.height / 15),"Voltar",ButtonStyle)){
				i = 0;
			}
		}
		if(i == 3){
			Configuracao();
		}
	}

	public void Configuracao(){
		i = 3;
		GUI.DrawTexture (new Rect (Screen.width / 9, Screen.height /9, Screen.width / 2 + Screen.width / 3, Screen.height / 2 +  Screen.height / 3), Options);
		GUI.Label (new Rect (Screen.width /3 + Screen.width / 13,Screen.height / 9 +  Screen.height / 20, Screen.width / 10, Screen.height / 15), "Configuraçoes", ButtonStyle);
		GUI.Label (new Rect (Screen.width / 9 + Screen.width / 10,Screen.height / 9 +  Screen.height / 7, Screen.width / 10, Screen.height / 15), "Video", ButtonStyle);
		GUI.Label (new Rect (Screen.width / 9 + Screen.width / 8,Screen.height / 9 +  Screen.height / 5, Screen.width / 10, Screen.height / 15), "Resoluçao", ButtonStyle);
		resolu = GUI.HorizontalSlider (new Rect (Screen.width / 5 + Screen.width / 5,Screen.height / 9 +  Screen.height / 5 + Screen.height / 70, Screen.width / 5, Screen.height / 40),resolu,0,Screen.resolutions.Length );
		resolucao = Screen.resolutions[Mathf.FloorToInt(resolu)];
		GUI.Label (new Rect (Screen.width / 3 + Screen.width / 3,Screen.height / 9 +  Screen.height / 5, Screen.width / 10, Screen.height / 15),resolucao.width + "x" + resolucao.height, ButtonStyle);
		GUI.Label (new Rect (Screen.width / 9 + Screen.width / 8,Screen.height / 5 +  Screen.height / 5, Screen.width / 10, Screen.height / 15), "Tela Cheia", ButtonStyle);
		tela = GUI.Toggle (new Rect (Screen.width / 5 + Screen.width / 5,Screen.height / 5 +  Screen.height / 5, Screen.width / 10, Screen.height / 15),tela," ");
		GUI.Label (new Rect (Screen.width / 9 + Screen.width / 10,Screen.height / 2, Screen.width / 10, Screen.height / 15), "Audio", ButtonStyle);
		GUI.Label (new Rect (Screen.width / 9 + Screen.width / 8,Screen.height / 2 +  Screen.height / 10, Screen.width / 10, Screen.height / 15), "Musica", ButtonStyle);
		int volu = Mathf.FloorToInt(volume);
		string audio = volu.ToString();
		volume = GUI.HorizontalSlider (new Rect (Screen.width / 5 + Screen.width / 5,Screen.height / 2 +  Screen.height / 10, Screen.width / 5, Screen.height / 40),volume,0,100);
		GUI.Label (new Rect (Screen.width / 2 + Screen.width / 8,Screen.height / 2 +  Screen.height / 10, Screen.width / 10, Screen.height / 15),audio, ButtonStyle);
		if(GUI.Button (new Rect (Screen.width /3 + Screen.width / 7,Screen.height / 2 + Screen.height / 3,Screen.width / 10, Screen.height / 15),"Aplicar",ButtonStyle)){
			setResolucao(resolucao.width,resolucao.height,tela);
			GravaInformacao(tela,Convert.ToInt32(resolu),audio,resolucao.width,resolucao.height);
		}
		if(GUI.Button (new Rect (Screen.width / 9 + Screen.width / 10,Screen.height / 2 + Screen.height / 3,Screen.width / 10, Screen.height / 15),"Calibrar Tabuleiro",ButtonStyle)){
			if(game == true){
				menu.GetComponent<ARCamera> ().calibra = true;
			}else{
				PlayerPrefs.SetInt("menuPrincipal",1);
				Application.LoadLevel ("Game");
			}

		}
		if(GUI.Button (new Rect (Screen.width / 3 + Screen.width / 3,Screen.height / 2 + Screen.height / 3,Screen.width / 10, Screen.height / 15),"Voltar",ButtonStyle)){
			i = 0;
		}
	}
	public void setResolucao(int width , int height , bool tela){

			Screen.SetResolution(width,height,tela);

	}
	public void BuscaInformacao(){
		try
		{
			int x = 0;
			StreamReader objReader = new StreamReader(strPathFile);
			string sLine = "";
			while (sLine != null)
			{
				sLine = objReader.ReadLine();

				if (sLine != null){
					conf[x] = sLine;
					x++;
				}
			}
			tela = Convert.ToBoolean(conf[0]);
			resolu = Convert.ToInt32(conf[1]);
			volume = float.Parse(conf[2]);
			int width = Convert.ToInt32(conf[3]);
			int heigth  = Convert.ToInt32(conf[4]);
			Screen.SetResolution(width,heigth,tela);
		}
		catch (Exception ex)
		{
			Debug.Log("Erro ao abrir Arquivo");
		}
			//Debug.Log("Arquivo aberto com sucesso!!!");
	}
	 void GravaInformacao(bool x ,float resu, String vol, int width , int heigth){
		try
		{
			using (FileStream fs = File.Create(strPathFile))
			{
				using (StreamWriter sw = new StreamWriter(fs))
				{
					sw.WriteLine(tela);
					sw.WriteLine(resu);
					sw.WriteLine(vol);
					sw.WriteLine(width);
					sw.WriteLine(heigth);
				}
			}
		}
		catch (Exception ex)
		{
			Debug.Log("Erro ao criar Arquivo");
		}
		//Debug.Log("Arquivo criado com sucesso!!!");
	}

	public int getI(){
		return i;
	}
}


