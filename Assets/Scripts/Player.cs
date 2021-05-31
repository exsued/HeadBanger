using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public Animator playerAnims;
    public Vector3 direction;

    public Texture damagedBody;
    public Texture normalBody;

    private Material fatMaterial;
    private Material fatBodyMaterial;

    public Transform fatShaderObject;
    public Transform fatBodyShaderObject;

    public Transform[] muscleTransform;
    public GameObject acne;

    public Collider eatZone;

    public static Player instance;

    public float maxFatAmount = 0.03f;
    public float maxBodyFatAmount = 0.01f;

    Rigidbody body;
    void Start()
    {
        body = GetComponent<Rigidbody>();
        fatMaterial = fatShaderObject.GetComponent<SkinnedMeshRenderer>().material;
        fatBodyMaterial = fatBodyShaderObject.GetComponent<SkinnedMeshRenderer>().material;

        if (instance == null)
            instance = this;
        else
            throw new Exception("Multiple players in game");
    }

    void FixedUpdate()
    {
        var isPressed = Input.touchCount > 0 || Input.GetMouseButton(0);
        playerAnims.SetBool("Enabled", isPressed);
        eatZone.enabled = isPressed;
        body.MovePosition(body.position + direction * Time.deltaTime);
    }
    public void OnEat(FoodType type)
    {
        switch (type)
        {
            case FoodType.Burger:
                fatMaterial.SetFloat("_Amount", Mathf.Clamp(fatMaterial.GetFloat("_Amount") + 0.01f, 0f, maxFatAmount));
                fatBodyMaterial.SetFloat("_Amount", Mathf.Clamp(fatMaterial.GetFloat("_Amount") + 0.01f, 0f, maxBodyFatAmount));
                break;
            case FoodType.WaterBottle:
            case FoodType.Vegetables:
                fatMaterial.SetFloat("_Amount", Mathf.Clamp(fatMaterial.GetFloat("_Amount") - 0.01f, 0f, maxFatAmount));
                fatBodyMaterial.SetFloat("_Amount", Mathf.Clamp(fatMaterial.GetFloat("_Amount") - 0.01f, 0f, maxBodyFatAmount));
                break;
            case FoodType.Protein:
                foreach (var muscle in muscleTransform)
                    muscle.transform.localScale = new Vector3(1.408f, 1f, 1f);
                break;
            case FoodType.Candy:
                acne.SetActive(true);
                break;
            case FoodType.BrokenGlass:
            case FoodType.Poison:
                fatBodyMaterial.mainTexture = damagedBody;
                break;
                //fatMaterial.mainTexture = normalBody;
        }

    }
}
