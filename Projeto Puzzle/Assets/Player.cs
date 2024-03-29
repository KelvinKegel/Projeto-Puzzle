﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 2f;
    public float jumpHeight = 5f;

    public float walkSpeed = 3f;
    public float runSpeed = 8f;
    public float gravity = -12f;

    [SerializeField]
    float velocityY;

    [SerializeField]
    Transform cameraT;

    [SerializeField]
    bool running = false;

    float smoothRotationVelocity;
    [SerializeField]
    float smoothRotationTime = 0.2f;

    float smoothSpeedVelocity;
    [SerializeField]
    float smoothSpeedTime = 0.2f;

    [SerializeField]
    CharacterController charController;

    [SerializeField]
    bool isOnPlat = false;

    // Start is called before the first frame update
    void Start()
    {
        //pegando referências
        cameraT = Camera.main.transform;
        charController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        walkingRotating();
        //walkSideways();


    }

    void walkSideways()
    {
        // pegar input do jogador
        // Input.getaxis retorna um valor de -1 a 1
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        //normalizamos para pegar apenas a direção

        Vector3 direction = input.normalized;
        //nossa velocidade será a direção multiplicada pela nossa velocidade
        Vector3 velocity = direction * speed;

        //por fim a distância para percorrer será essa velocidade multiplicada pelo tempo
        Vector3 moveAmount = velocity * Time.deltaTime;

        //aqui iremos mover nosso jogador pela distância que iremos percorrer
        transform.Translate(moveAmount);

    }

    void walkingRotating()
    {
        // pegar input do jogador
        // Input.getaxis retorna um valor de -1 a 1
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        //normalizamos para pegar apenas a direção
        Vector2 inputDir = input.normalized;

        float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;

        //rotação
        if (inputDir != Vector2.zero)
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref smoothRotationVelocity, smoothRotationTime);


        running = (Input.GetKey(KeyCode.LeftShift));

        float targetSpeed = (running) ? runSpeed : walkSpeed * inputDir.magnitude;

        speed = Mathf.SmoothDamp(speed, targetSpeed, ref smoothSpeedVelocity, smoothSpeedTime);

        //aumentando a aceleração da gravidade
        velocityY += gravity * Time.deltaTime;

        Vector3 velocity = transform.forward * speed * inputDir.magnitude + Vector3.up * velocityY;

        charController.Move(velocity * Time.deltaTime);

        speed = new Vector2(charController.velocity.x, charController.velocity.z).magnitude;

        Jump();

        if (charController.isGrounded)
        {
            velocityY = 0;

        }

        Jump();
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gravity = -12f;

            if (charController.isGrounded || isOnPlat)
            {


                float jumpVelocity = Mathf.Sqrt(-2 * gravity * jumpHeight);
                velocityY = jumpVelocity;

            }

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlatformAttach>())
        {
            isOnPlat = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlatformAttach>())
        {
            isOnPlat = false;
        }
    }

}
