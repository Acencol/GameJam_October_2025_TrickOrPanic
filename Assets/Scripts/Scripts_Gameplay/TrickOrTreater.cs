using UnityEngine;

public class TrickOrTreater : MonoBehaviour
{
    [SerializeField] public RequestSprites requestSprites; // ← CHANGED!

    public CandyType requestedCandy;

    private SpriteRenderer bodyRenderer;
    private SpriteRenderer requestDisplay;

    private float timer = 90f;

    private void Awake()
    {
        bodyRenderer = GetComponent<SpriteRenderer>();
        requestDisplay = transform.Find("RequestDisplay").GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        Debug.Log($"Requested Candy: {requestedCandy}, Index: {(int)requestedCandy}");

        if (requestSprites != null && requestedCandy != default)
        {
            int spriteIndex = (int)requestedCandy;
            Debug.Log($"Using sprite index: {spriteIndex} from array size: {requestSprites.requestSprites.Length}");

            if (spriteIndex < requestSprites.requestSprites.Length)
            {
                requestDisplay.sprite = requestSprites.requestSprites[spriteIndex];
                requestDisplay.gameObject.SetActive(true);
                Debug.Log($"Set sprite: {requestSprites.requestSprites[spriteIndex].name}");
            }
            else
            {
                Debug.LogError($"Index {spriteIndex} out of bounds! Array size: {requestSprites.requestSprites.Length}");
            }
        }
        else
        {
            Debug.LogError("RequestSprites is NULL or requestedCandy is default!");
        }
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            GameManager.Instance.OnFailure(this);
        }
    }
}