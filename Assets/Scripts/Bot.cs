using UnityEngine;

public class Bot : MonoBehaviour
{
    public Texture damagedBody;
    public Texture normalBody;

    private Material fatMaterial;
    private Material fatBodyMaterial;

    public Transform fatShaderObject;
    public Transform fatBodyShaderObject;

    public Transform[] muscleTransform;
    public GameObject acne;

    public Animator anims;

    public Transform spotTrans;
    public float SpotItemRadius = 1f;
    public float maxFatAmount = 0.03f;
    public float maxBodyFatAmount = 0.01f;
    public Collider EatTrigger;

    public static Bot instance;

    private void Start()
    {
        instance = this;
        fatMaterial = fatShaderObject.GetComponent<SkinnedMeshRenderer>().material;
        fatBodyMaterial = fatBodyShaderObject.GetComponent<SkinnedMeshRenderer>().material;
    }
    private void Update()
    {
        var elements = Physics.OverlapSphere(spotTrans.position, SpotItemRadius);

        if(elements.Length > 1)
        {
            for (int i = 0; i < elements.Length; i++)
            {
                var el = elements[i].GetComponent<Food>();
                if (el != null && 
                    (el.type == FoodType.Burger || el.type == FoodType.BrokenGlass || el.type == FoodType.Dynamite || el.type == FoodType.Poison || el.type == FoodType.Candy))
                {
                    print(el.type);
                    EatTrigger.enabled = true;
                    anims.SetBool("Enabled", true);
                }
            }
        }
        else
        {
            EatTrigger.enabled = false;
            anims.SetBool("Enabled", false);
        }

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
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(spotTrans.position, SpotItemRadius);
    }
}
