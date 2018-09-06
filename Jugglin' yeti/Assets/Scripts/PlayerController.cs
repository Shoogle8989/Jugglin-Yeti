using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Camera cam;
    private float boundMax, boundMin; // Limites da direita e esquerda da tela
    private Vector3 direction;//direcao do movimento do personagem
    public float speed; // velocidade do player 
    public float Force; // forca que o player rebate os objetos

    // Use this for initialization
    void Start () {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        ScreenSize(cam);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Input.touchCount != 0)
        {  //para cada touch input do jogador no frame
            Vector3 playerPosScreen = cam.WorldToScreenPoint(transform.position); // converte a posição do objeto para a posição da tela
            Vector3 fingerPosition = cam.ScreenToWorldPoint(Input.GetTouch(0).position); // converte a posição do dedo para global
                                                                                         // ve se a diferença entre o clique e o objeto é menor que 0.2f
            if (fingerPosition.x - transform.position.x <= 0.2f && fingerPosition.x - transform.position.x >= 0)
            {
                direction = Vector3.zero; // se sim, não se mexe
            }
            else
            { //se não
              // verifica se está a esquerda ou direita da bolinha e atribui o valor
                direction = (Input.GetTouch(0).position.x < playerPosScreen.x) ? Vector3.left : Vector3.right;
            }
            Movement(Input.GetTouch(0).phase, direction); // chama a função de movimento e informa a direção e qual fase do toque está		
        }

        //garante que a tela é o limite para o yeti
        if (transform.position.x >= boundMax)
        {
            transform.position = new Vector2(boundMax, transform.position.y);
        }
        else if (transform.position.x <= boundMin)
        {
            transform.position = new Vector2(boundMin, transform.position.y);
        }
    }

    private void ScreenSize(Camera cam) //seta o boundMin e boundMax
    {
        Vector3 newVector = new Vector3(Screen.width, Screen.height, 0);
        Vector3 screenSize = cam.ScreenToWorldPoint(newVector);
        Vector3 playerSize = GetComponent<Renderer>().bounds.size;

        boundMin = (playerSize.x / 2) - screenSize.x;
        boundMax = screenSize.x - (playerSize.x / 2);
    }

    private void Movement(TouchPhase touch, Vector3 direction)
    {
        switch (touch)
        {  // irá ler o touch input e
            case TouchPhase.Began:  // se tiver começado
            case TouchPhase.Stationary:  // ou contínuo (parado)
            case TouchPhase.Moved: // ou mover o dedo
                transform.Translate(direction * speed * Time.deltaTime);  // movimenta na direção informada, com a velocidade estipulada
                break;
            default:  // senão, para
                break;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Bounceable")
        {
            Vector2 direction = collision.transform.position - transform.position;//define a direcao que a forca deve ser aplicada
            direction.Normalize();//normaliza a direcao da forca
            collision.rigidbody.AddForce(direction * Force);
        }
    }
}
