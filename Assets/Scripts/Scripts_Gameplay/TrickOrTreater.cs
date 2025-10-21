using UnityEngine;

public class TrickOrTreater : MonoBehaviour
{
    [SerializeField] public RequestSprites requestSprites; // ← CHANGED!

    public CandyType requestedCandy;

    private SpriteRenderer bodyRenderer;
    private SpriteRenderer requestDisplay;

    private float timer = 1f;

    private void Awake()
    {
        bodyRenderer = GetComponent<SpriteRenderer>();
        requestDisplay = transform.Find("RequestDisplay").GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        // FORCE ASSIGNMENT - This GUARANTEES requestSprites is never null
        if (requestSprites == null)
        {
            requestSprites = GameManager.Instance.GetComponent<GameManager>().requestSprites;
            Debug.Log("FORCE ASSIGNED requestSprites from GameManager!");
        }

        Debug.Log($"Requested Candy: {requestedCandy}, Index: {(int)requestedCandy}");

        // DEFAULT TO RED IF ANYTHING GOES WRONG
        CandyType candyToUse = requestedCandy != default ? requestedCandy : CandyType.Red;
        int spriteIndex = (int)candyToUse;

        // SAFETY CHECKS
        if (requestSprites != null && requestSprites.requestSprites != null && spriteIndex < requestSprites.requestSprites.Length)
        {
            requestDisplay.sprite = requestSprites.requestSprites[spriteIndex];
            requestDisplay.gameObject.SetActive(true);
            Debug.Log($"Set sprite: {requestSprites.requestSprites[spriteIndex].name} (Index: {spriteIndex})");
        }
        else
        {
            // EMERGENCY DEFAULT: ALWAYS SHOW RED
            if (requestSprites != null && requestSprites.requestSprites.Length > 0)
            {
                requestDisplay.sprite = requestSprites.requestSprites[0]; // RED = Index 0
                requestDisplay.gameObject.SetActive(true);
                Debug.Log("EMERGENCY DEFAULT: Set RED sprite!");
            }
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