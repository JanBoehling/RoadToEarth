using UnityEngine;
using UnityEngine.UI;

public class ShapesCatcha : MonoBehaviour
{
    [SerializeField] private ShapesGame gamePrefab;

    [Header("Children")]
    [SerializeField] private ShapesGame gameInstance;

    private void Awake()
    {
        ConnectEvents(gameInstance);
    }

    private void ResetCaptcha()
    {
        var gameParent = gameInstance.transform.parent;
        Destroy(gameInstance.gameObject);
        gameInstance = Instantiate(gamePrefab, gameParent);
        ConnectEvents(gameInstance);

        if (!gameInstance.TryGetComponent<RectTransform>(out var rect)) return;
        rect.anchorMin = new Vector2(0.5f, 0.5f);
        rect.anchorMax = new Vector2(0.5f, 0.5f);
        rect.anchoredPosition = new Vector2(0, 0);
    }

    private void ConnectEvents(ShapesGame game)
    {
        game.ResetPressed += OnResetPressed;
        game.ConfirmPressed += OnConfirmPressed;
    }

    private void OnResetPressed()
    {
        ResetCaptcha();
    }

    private void OnConfirmPressed()
    {
        // TODO
    }
}
