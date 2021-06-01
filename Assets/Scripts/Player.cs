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

    public GameObject blackBall;
    public Transform smilesSpawn;

    public Transform happySmile;
    public Transform unhappySmile;

    public Transform fatShaderObject;
    public Transform fatBodyShaderObject;
    public Transform[] muscleTransform;

    public Transform spine1, spine2, leftLeg, rightLeg, head;
    public GameObject acne;

    public Collider eatZone;

    public FoodType prevFood;
    public static Player instance;

    public float maxFatAmount = 0.03f;
    public float maxBodyFatAmount = 0.01f;

    Rigidbody body;
    void Start()
    {
        blackBall.SetActive(false);
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
    public void OnGameProceed()
    {
        blackBall.SetActive(true);
    }
    public void OnEat(FoodType type)
    {
        switch (type)
        {
            case FoodType.Burger:
                fatMaterial.SetFloat("_Amount", Mathf.Clamp(fatMaterial.GetFloat("_Amount") + 0.01f, 0f, maxFatAmount));
                fatBodyMaterial.SetFloat("_Amount", Mathf.Clamp(fatMaterial.GetFloat("_Amount") + 0.01f, 0f, maxBodyFatAmount));
                spine1.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                leftLeg.localScale = new Vector3(1.75f, 1f, 1f);
                rightLeg.localScale = new Vector3(1.75f, 1f, 1f);
                head.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                spine2.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                head.localScale = new Vector3(1f, 1f, 1f);
                
                if (prevFood != type)
                {
                    Instantiate(unhappySmile, smilesSpawn).GetComponent<Smile>();
                    prevFood = type;
                }
                break;
            case FoodType.WaterBottle:
            case FoodType.Vegetables:
                fatMaterial.SetFloat("_Amount", Mathf.Clamp(fatMaterial.GetFloat("_Amount") - 0.01f, 0f, maxFatAmount));
                fatBodyMaterial.SetFloat("_Amount", Mathf.Clamp(fatMaterial.GetFloat("_Amount") - 0.01f, 0f, maxBodyFatAmount));
                spine1.localScale = new Vector3(1f, 1f, 1f);
                spine2.localScale = new Vector3(1f, 1f, 1f);
                leftLeg.localScale = new Vector3(1f, 1f, 1f);
                rightLeg.localScale = new Vector3(1f, 1f, 1f);
                if (prevFood != type)
                {
                    Instantiate(happySmile, smilesSpawn);
                    prevFood = type;
                }
                break;
            case FoodType.Protein:
                foreach (var muscle in muscleTransform)
                    muscle.transform.localScale = new Vector3(1.408f, 1f, 1f);
                if (prevFood != type)
                {
                    Instantiate(happySmile, smilesSpawn);
                    prevFood = type;

                }
                break;
            case FoodType.Candy:
                acne.SetActive(true);
                if (prevFood != type)
                {
                    Instantiate(unhappySmile, smilesSpawn);
                    prevFood = type;
                }
                break;
            case FoodType.Dynamite:
            case FoodType.BrokenGlass:
            case FoodType.Poison:
                Instantiate(unhappySmile, smilesSpawn);
                prevFood = type;
                fatBodyMaterial.mainTexture = damagedBody;
                break;

                //fatMaterial.mainTexture = normalBody;
        }
    }
}
