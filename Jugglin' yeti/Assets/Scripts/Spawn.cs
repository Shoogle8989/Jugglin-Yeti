using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {
    private float timer;
    private int BouncyNum;
    private List<GameObject> Lista = new List<GameObject>();//lista que guarda os Objetos ativos
    public GameObject Bouncy;
    public GameObject Bouncy2;
    public float SpawnTimer; // quantidade de tempo para spawnar um novo objeto
    public int MaxBouncyNum;// quantidade maxima de objetos ativos


	// Use this for initialization
	void Start () {
        timer = 0f;
        BouncyNum = 1;
	}
	
	// Update is called once per frame
	void Update () {
        if (BouncyNum <= MaxBouncyNum)
        {
            timer += Time.deltaTime;
        }
        if (timer >= SpawnTimer)
        {
            if (Random.value < 0.5f)//randomicamente adiciona um novo objeto ao jogo
            {
                Lista.Add(Instantiate(Bouncy, new Vector2(0, 15), Quaternion.identity));
            }
            else
            {
                Lista.Add(Instantiate(Bouncy2, new Vector2(0, 15), Quaternion.identity));
            }
            timer = timer % SpawnTimer;
            BouncyNum += 1;
        }
        foreach (GameObject objeto in Lista)
        {
            if (objeto.transform.position.y <= -9f)// caso o objeto caia, retire ele do jogo
            {
                Lista.Remove(objeto);
                Destroy(objeto);
                BouncyNum -= 1;
            }
        }
	}
}
