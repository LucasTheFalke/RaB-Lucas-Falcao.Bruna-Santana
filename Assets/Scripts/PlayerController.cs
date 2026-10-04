using System.Collections;
using System.Collections.Generic;
using Unity.Collections.Tests.CoreCLR.TestJobs;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
private Rigidbody rb;
public float velocidade;
private int count;
public TextMeshProUGUI countText;
public GameObject winTextObject;
//Declarações de variáveis adicionadas para controlar a rotação e a aceleração do carro
public float velocidadeRotacao = 100f;
private float velocidadeAtual = 0f;
    public float aceleracao = 10f;

    
    void Start() 
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText(); 
        winTextObject.SetActive(false);
    }

    
    void Update()
    {float movimentoHorizontal = Input.GetAxis("Horizontal");
    float movimentoVertical    = Input.GetAxis("Vertical");
    // Declaração adicional que acelera ou desacelera suavemente com base no input
        if (movimentoVertical != 0)
        {
            velocidadeAtual = Mathf.MoveTowards(velocidadeAtual, movimentoVertical * velocidade, aceleracao * Time.deltaTime);
        }
        else
        {
            // Desaceleração adicional que realiza o movimento de desaceleração ao soltar o acelerador
            velocidadeAtual = Mathf.MoveTowards(velocidadeAtual, 0f, aceleracao * Time.deltaTime);
        }

        // Declaração adicional que permite virar se o carro estiver em movimento (evitando girar parado)
        if (Mathf.Abs(velocidadeAtual) > 0.1f)
        {
            // Declaração adicional que inverte a direção do giro se estiver dando ré para ficar natural
            float direcaoRe = (velocidadeAtual < 0) ? -1f : 1f;
            float rotacao = movimentoHorizontal * velocidadeRotacao * direcaoRe * Time.deltaTime;
            transform.Rotate(0, rotacao, 0);
        }

        // Declaração adicional que move o carro baseado na velocidade atual 
        Vector3 movimento = transform.forward * velocidadeAtual * Time.deltaTime;
        transform.position += movimento;
    
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 8)
        {
            winTextObject.SetActive(true);
        }
    }

    void OnTriggerEnter(Collider other)
    {
    if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;

            SetCountText();
        }
    }
}
