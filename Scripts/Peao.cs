using UnityEngine;
using System.Collections;

public class Peao : MonoBehaviour {
	public Texture ImgTexture;
	GUIStyle FontStyle = new GUIStyle();
	private string[] Mensagens = new string[100];
	public Vector3[,] Tabuleiro = new Vector3[9,9];
	public Font FonteLabel;
	CriaTabuleiro tabu = new CriaTabuleiro ();
	public int mov;
	public int atc = 1;
	public int level = 1;
	public int promo = 1;
	bool start = false;
	bool peoa;
	bool arpeoa;
	bool triger;
	public int mens = 1;
	public GameObject prefabs;
	public GameObject ThisPeao;
	public GameObject MenuGame;
	public GameObject ARCamera;
	public GameObject MKPeao;
	public GameObject Model;
	float speed = 0.05f;
	float timer;
	float timerlimit = 5;
	float widthMen = Screen.width / 4 + Screen.width / 13;
	float widthFun = Screen.width / 4;



	void Start (){
		Mensagens [1] = "Primeiro iremos aprender \n sobre o peão";
		Mensagens [2] = "Para começar vamos aprender \n como o peão se movimenta";
		Mensagens [3] = "Posicione o marcador do peão\n na posição indicada";
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
		timer = timerlimit;
		Instantiate (prefabs,transform.position, transform.rotation);
		GameObject.Find("colisao(Clone)").name = "colisao(Clone)1";
	}

	public void Update () {
		if (start)
		{
			if(level == 1)
			{
				Movimento ();
			}
			if(level == 2)
			{
				Ataca();
			}
			if(level == 3)
			{
				start = false;
			}
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
				if(mens == 19){
					if (GUI.Button (new Rect (Screen.width / 2 + Screen.width /12, Screen.height / 5 + Screen.height / 25, Screen.width / 10, Screen.height / 25), "Proximo ->",FontStyle)) {
						level++;
					}
				}
			}
		}
	}

	void Movimento(){
		if (MenuGame.GetComponent<MenuGame> ().menu) {
		} else {
			peoa = MKPeao.GetComponent<Collider> ().getTrigerMark();
			triger = MKPeao.GetComponent<Collider> ().getTriger ();
			if (mens < 3) {
				if (timer <= 0) {
					timer = timerlimit;
					mens++;
				} else {
					timer -= Time.deltaTime;
				}
			} else if (mens == 3) {
				if (peoa) {
					GameObject.Find ("MarkerPeao").transform.position = new Vector3 (0, 0, 0);
					mens++;
				} else {
					GameObject.Find ("MarkerPeao").transform.position = Tabuleiro [2, 1];
				}
			}
			if (peoa) {
				if ((mens <= 4) && (mens > 3)) {
					if (timer <= 0) {
						timer = timerlimit;
						mens++;
					} else {
						timer -= Time.deltaTime;
					}
				}
				if ((mens == 4) && (mov == 0)) {
					mov++;
				}
				if (mov == 1) {
					transform.position = Tabuleiro [1, 1];
					if (timer <= 0) {
						timer = timerlimit;
						mov++;
					} else {
						timer -= Time.deltaTime;
					}
				}
				if ((mov == 2) && (mens == 5)) {
					transform.position = Vector3.Slerp (transform.position, Tabuleiro [1, 3], speed - Time.deltaTime);
					if ((int)transform.position.y ==  (int)Tabuleiro [1, 3].y) {
						ThisPeao.GetComponent<Anima> ().animacao (1);
					} else {
						ThisPeao.GetComponent<Anima> ().animacao (2);
					}
					if ((int)transform.position.y == (int)Tabuleiro [1, 3].y) {

						if (timer <= 3) {
							GameObject.Find("colisao(Clone)1").transform.position = Tabuleiro[2,3];
							timer = timerlimit;
							mens++;
							mov++;	
						} else {
							timer -= Time.deltaTime;
						}
					}
				}

			}
			if((mov == 3)&&(triger == true)){
				MKPeao.GetComponent<Collider> ().setTriger ();
				triger = MKPeao.GetComponent<Collider> ().getTriger ();
				MKPeao.GetComponent<Collider> ().setTriger();
				mov++;
			}
			if(mov == 4){
				if(mens <= 7){
					if(timer <= 0){
						timer = timerlimit;
						mens++;

					}else{
						timer -= Time.deltaTime;
					}
				}
				if(mens == 8){
					widthFun = Screen.width / 50;
					widthMen = Screen.width / 50 + Screen.width / 13;
					transform.position = Vector3.Slerp (transform.position, Tabuleiro [1, 4], speed - Time.deltaTime);
					if ((int)transform.position.y ==  (int)Tabuleiro [1, 4].y) {
						ThisPeao.GetComponent<Anima> ().animacao (1);
						GameObject.Find("colisao(Clone)1").transform.position = Tabuleiro[2,4];
						mens++;
					} else {
						ThisPeao.GetComponent<Anima> ().animacao (2);
					}
				}
				if((mens == 9)&&(MKPeao.GetComponent<Collider>().triger == true)){
					if(timer <= 0){
						timer = timerlimit;
						mens++;
						level++;
					}else{
						timer -= Time.deltaTime;
					}
				}
			}
		}
	}

	void Ataca()
	{
		if (mens == 10) {
			transform.position = Vector3.Slerp (transform.position, Tabuleiro [1, 5], speed - Time.deltaTime);
			if ((int)transform.position.y == (int)Tabuleiro[1,5].y) {
				ThisPeao.GetComponent<Anima> ().animacao (1);
				ThisPeao.transform.rotation = Quaternion.Euler (80, 90, 270);
				mens++;
			} else {
				ThisPeao.GetComponent<Anima> ().animacao (2);
			}
		}
		if((mens == 11)||(mens == 12)){
			if(timer <= 0)
			{
				timer = timerlimit;
				mens++;
			}else{
				timer -= Time.deltaTime;
			}
		}
		if (mens == 13) {
				NpcAtaca ();
			}
			if (mens == 14) {
			if(MKPeao.GetComponent<Collider>().trigerInimigo){
				if(timer <= 4){
					ThisPeao.transform.position = new Vector3(0,0,0);
					MKPeao.GetComponent<Collider>().trigerInimigo = false;
					mens++;
					timer = timerlimit;
				}else{
					timer -= Time.deltaTime;
				}
			}
			}
		if((mens == 15)||(mens == 16)){
			if(timer <= 0)
			{
				timer = timerlimit;
				mens++;
			}else{
				timer -= Time.deltaTime;
			}
		}
		if(mens == 17)
		{
			Promocao();
		}
			//level++;	
	}
 
	public void Promocao(){
		if (promo == 1) {
			if (timer <= 0) {
				GameObject.Find ("SKELETON(Clone)1").transform.position = Tabuleiro [3, 6];
				GameObject.Find ("SKELETON(Clone)1").GetComponent<Collider> ().setTriger();
				MKPeao.GetComponent<Collider> ().setTriger();
				timer = timerlimit;
				GameObject.Find ("colisao(Clone)").transform.position = Tabuleiro [3, 6];
				promo ++;
			} else {
				timer -= Time.deltaTime;
			}
		}
		if(promo == 2)
		{
			if (timer <= 2) {
				promo ++;
				timer = timerlimit;
			} else {
				timer -= Time.deltaTime;
			}
		}
		if(promo == 3){
			GameObject.Find ("SKELETON(Clone)1").transform.position = Vector3.Slerp(GameObject.Find ("SKELETON(Clone)1").transform.position,Tabuleiro[3,7],speed * Time.deltaTime);
			GameObject.Find ("colisao(Clone)").transform.position = Tabuleiro [3, 7];
			if(GameObject.Find ("SKELETON(Clone)1").GetComponent<Collider> ().triger)
			{
				if(timer <= 3){
					GameObject.Find ("SKELETON(Clone)1").GetComponent<Anima> ().animacao (1);
					promo ++;
					mens++;
					timer = timerlimit;
				}else{
					timer -= Time.deltaTime;
				}
			}else{
				GameObject.Find ("SKELETON(Clone)1").GetComponent<Anima> ().animacao (2);
			}
		}
		if (promo == 4) {
			GameObject.Find ("colisao(Clone)").transform.position = Tabuleiro [1, 7];
			GameObject.Find ("MarkerPeao").transform.position = Tabuleiro [1, 7];
			Debug.Log("antes do if");
			if(MKPeao.GetComponent<Collider> ().triger == true){
				Debug.Log("sentro do if");
				GameObject.Find ("MarkerPeao").transform.position = new Vector3(0,0,0);
				mens++;
			}
		}
	}
		public void NpcAtaca(){
		if (atc == 1) 
		{
			if(timer <= 0)
			{
				Instantiate (Model, Tabuleiro [4, 2], Quaternion.Euler (270, 0, 0));
				GameObject.Find("SKELETON(Clone)").name = "SKELETON(Clone)1";
				Instantiate (Model, Tabuleiro [3, 3], Quaternion.Euler (80, 90, 270));
				GameObject.Find("SKELETON(Clone)").name = "SKELETON(Clone)2";
				GameObject.Find("SKELETON(Clone)2").tag = "Inimigo";
				atc++;
				timer = timerlimit;
			}else{
				timer -= Time.deltaTime;
			}
		}

		if(atc == 2)
		{
			if(timer <= 0)
			{
				GameObject.Find("SKELETON(Clone)1").transform.rotation = Quaternion.Euler (300, 90, 270);
				atc++;
				timer = timerlimit;
			}else{
				timer -= Time.deltaTime;
			}
		}

		if (atc == 3) {
			if (GameObject.Find ("SKELETON(Clone)1").GetComponent<Collider> ().trigerInimigo == true) {
				GameObject.Find ("SKELETON(Clone)1").GetComponent<Anima> ().animacao (3);
				if(timer <= 4.3f)
				{
					Destroy(GameObject.Find("SKELETON(Clone)2"));
					Instantiate (prefabs, Tabuleiro [3, 3], transform.rotation);
					timer = timerlimit;
					GameObject.Find ("SKELETON(Clone)1").GetComponent<Collider> ().setTrigerInimigo();
				}else{
					timer -= Time.deltaTime;
				}

			} else {
				GameObject.Find ("SKELETON(Clone)1").transform.position = Vector3.Slerp (GameObject.Find ("SKELETON(Clone)1").transform.position, Tabuleiro [3, 3], speed - Time.deltaTime);
				if (GameObject.Find ("SKELETON(Clone)1").GetComponent<Collider> ().triger == true) {
					if(timer <= 4)
					{
						GameObject.Find ("SKELETON(Clone)1").GetComponent<Anima> ().animacao (1);
						GameObject.Find("SKELETON(Clone)1").transform.rotation = Quaternion.Euler (270, 0, 0);
						timer = timerlimit;
						mens++;
					}else{
						timer -= Time.deltaTime;
					}
				} else {
					GameObject.Find ("SKELETON(Clone)1").GetComponent<Anima> ().animacao (2);
				}
			}
		}
	}

	public void setStart(bool set){
		start = set;
	}
	public bool getStart(){
		return start;
	}
	
}
