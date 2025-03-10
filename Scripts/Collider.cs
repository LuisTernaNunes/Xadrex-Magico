using UnityEngine;
using System.Collections;

public class Collider : MonoBehaviour {
	public bool triger;
	public bool trigerAnima;
	public bool trigerMark;
	public bool trigerInimigo;
	public bool trigerNpc;


	void Start()

	{
	
	}


	void OnCollisionEnter(Collision colider)
	{
		if (colider.gameObject.tag == "colisao")
		{
			triger = true;
		}
		if (colider.gameObject.tag == "marcador")
		{
			trigerAnima = true;
		}
		if (colider.gameObject.tag == "Triger")
		{
			trigerMark = true;
		}
		if (colider.gameObject.tag == "Inimigo")
		{
			trigerInimigo = true;
		}
		if (colider.gameObject.tag == "MovNpc")
		{
			trigerNpc = true;
		}
	}

	public bool getTriger()
	{
		return triger;
	}
	
	public bool getTrigerMark()
	{
		return trigerMark;
	}

	public bool getTrigerAnima()
	{
		return trigerAnima;
	}
	public bool getTrigerInimigo()
	{
		return trigerInimigo;
	}

	public void setTrigerAnima()
	{
		trigerAnima = false;
	}
	public void setTriger()
	{
		triger = false;
	}
	public void setTrigerInimigo()
	{
		trigerInimigo = false;
	}
	public void setTrigerNpc()
	{
		trigerNpc = false;
	}
}
