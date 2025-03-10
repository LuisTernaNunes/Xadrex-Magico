using UnityEngine;
using System.Collections;

public class Torre : MonoBehaviour {
	
	public Texture ImgTexture;
	public Vector3[,] Tabuleiro = new Vector3[9,9];
	public Font FonteLabel;
	public GameObject Marker;
	public GameObject MenuGame;

	private bool start;
	private GUIStyle FontStyle = new GUIStyle();
	private string[] Mensagens = new string[100];
	private CriaTabuleiro tabu = new CriaTabuleiro ();
	private float widthMen = Screen.width / 4 + Screen.width / 13;
	private float widthFun = Screen.width / 4;
	private float speed = 0.05f;
	private float timer;
	private float timerlimit = 5;

	//Controle da classe
	private int mens = 1;
	private int tuto = 1;
	private int level = 1;

	// Use this for initialization
	void Start () {
		Mensagens [1] = "Agora que aprendemos sobre\n o peão vamos aprender sobre a Torre";
		Mensagens [2] = "Para começarmos vamos aprender \n como o Torre se movimenta";
		Mensagens [3] = "Posicione o marcador da Torre\n na posição indicada";
		Mensagens [4] = "O primeiro movimento do peão\n pode ser de duas casas ";
		Mensagens [5] = "Observe o exemplo \n a seguir";
		Mensagens [6] = "Agora faça o mesmo com \n o marcador";
		Mensagens [7] = "Após o primeiro movimento\n o peão é limitado a uma\n casa por vez";
		Mensagens [8] = "Observe o exemplo \n a seguir";
		Mensagens [9] = "Agora faça o mesmo com \n o marcador";
		Mensagens [10] = "Agora vamos aprender \n como o peao captura outras\n as peças";
		Mensagens [11] = "O peão pode captaurar \n peças nas diagonais";
		Mensagens [12] = "Mas somente a sua frente";
		Mensagens [13] = "Observe o exemplo \n a seguir";
		Mensagens [14] = "Agora faça o mesmo com \n o marcador";
		Mensagens [15] = "De todas as peças do jogo de\n xadrex o peão é a \n única que pode ser promovida";
		Mensagens [16] = "O peão é promovido\n quando o mesmo atinge a oitava\ncasa do tabuleiro";
		Mensagens [17] = "Observe o exemplo \n a seguir";
		Mensagens [18] = "Agora leve o marcador \n até a oitava casa";
		Mensagens [19] = "Fim do tutorial Peão";

		tabu.SetTabuleiro ();
		Tabuleiro = tabu.GetTabuleiro ();

	}
	
	// Update is called once per frame
	void Update () {
		if(start)
		{
			TutoTorre();
		}
	}

	void OnGUI(){
		if (MenuGame.GetComponent<MenuGame> ().menu) {
		} else {
			if (start) {
				FontStyle.font = FonteLabel;
				FontStyle.fontSize = Screen.width / 40;
				GUI.DrawTexture (new Rect (widthFun, 0, Screen.width / 2 + Screen.width / 15, Screen.height / 3 + Screen.height / 15), ImgTexture);
				GUI.skin.label.fontSize = Screen.height / 30;
				GUI.Label (new Rect (widthMen, Screen.height / 12, Screen.width / 2, Screen.height / 3), Mensagens [mens], FontStyle);
			}
		}
	}

	public void TutoTorre(){
		if(mens <= 2){
			if(timer <= 0){
				timer = timerlimit;
				mens++;
			}else{
				timer -= Time.deltaTime;
			}
		}
		if(mens == 3)
		{
			Marker.transform.position = Tabuleiro[0,0];
		}
	}

	public bool getStart(){
		return start;
	}
	public void setStart(bool set){
		start = set;
	}
}
