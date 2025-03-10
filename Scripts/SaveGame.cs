using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class SaveGame : MonoBehaviour {

	int save = 1;
	String[,] jogosSalvos = new string[5,4];

	public void BuscaJogos(){
		while(save <= 4){
		try
		{
			StreamReader objReader = new StreamReader(@"C:\Users\Luis\Documents\My Games\XadrezMagico\Saves\SaveGame"+save+".load");
			int linha = 1;
			string sLine = "";
			while (sLine != null)
			{
				sLine = objReader.ReadLine();	
				if (sLine != null){
					jogosSalvos[save,linha] = sLine;
					linha++;
					}
			}
		}
		catch (Exception ex)
		{
				Debug.Log("erro");
		}
			save++;
		}
	}
	public void SalvaJogo(int level, int savex){
		try
		{
			DateTime agora = DateTime.Now;
			using (FileStream fs = File.Create(@"C:\Users\Luis\Documents\My Games\XadrezMagico\Saves\SaveGame"+ savex +".load"))
			{
				using (StreamWriter sw = new StreamWriter(fs))
				{
					sw.WriteLine(savex);
					sw.WriteLine(level);
					sw.WriteLine (agora.ToString("dd/MM/yyyy"));

				}
			}
		}
		catch (Exception ex)
		{
			Debug.Log("Erro ao criar Arquivo");
		}
			Debug.Log("Arquivo criado com sucesso!!!");
	}
	public String[,] getJogos(){
		return jogosSalvos;
	} 
}
