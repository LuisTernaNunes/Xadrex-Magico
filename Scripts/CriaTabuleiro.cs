using UnityEngine;
using System.Collections;

public class CriaTabuleiro : MonoBehaviour {

	public Vector3[,] Tabuleiro = new Vector3[9,9];

	public void SetTabuleiro(){
		Vector3 x1 = new Vector3 (249,-238,810);
		Vector3 x2 = new Vector3 (-249,-238,810);
		Vector3 x4 = new Vector3 (249,244,810);
		Vector3 x3 = new Vector3 (217,-206,810);
		Vector3 posi = x3;

		float tamanhoy = Vector3.Distance (x1 , x4);
		tamanhoy = tamanhoy / 8;
		
		int tamanhov = Mathf.RoundToInt(tamanhoy);

		float tamanhot = Vector3.Distance (x1 , x2);
		tamanhot = tamanhot / 8;

		int tamanho = Mathf.RoundToInt(tamanhot);

		for (int i = 0; i < 8; i++) {
			for (int y = 0; y < 8; y++) {
				Tabuleiro[i,y] = posi;
				posi.y = posi.y + tamanhov;
			}
			posi.x = posi.x - tamanho;
			posi.y = x3.y;
		}
	}
	public Vector3[,] GetTabuleiro(){
		return Tabuleiro;
	}
}
