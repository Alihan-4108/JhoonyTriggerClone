using UnityEngine;
using UnityEngine.UI;

public class UIBulletsContainer : MonoBehaviour
{
    public static UIBulletsContainer instance;

    [Header("Elements")]
    [SerializeField] private Transform bulletsParent;

    [Header("Settings")]
    [SerializeField] private Color activeColor;
    [SerializeField] private Color inactiveColor;
    private int bulletsShot;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        PlayerShooter.onShot += OnShotCallBack;

        PlayerMovement.onEnteredWarzone += EnteredWarzoneCallback;
        PlayerMovement.onExitedWarzone += ExitedWarzoneCallback;
    }

    private void Start()
    {
        bulletsParent.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        PlayerShooter.onShot -= OnShotCallBack;

        PlayerMovement.onEnteredWarzone -= EnteredWarzoneCallback;
        PlayerMovement.onExitedWarzone -= ExitedWarzoneCallback;
    }

    private void OnShotCallBack()
    {
        bulletsShot++;

        bulletsShot = Mathf.Min(bulletsShot, bulletsParent.childCount);

        bulletsParent.GetChild(bulletsShot - 1).GetComponent<Image>().color = inactiveColor;
    }

    private void EnteredWarzoneCallback()
    {
        bulletsParent.gameObject.SetActive(true);
    }

    private void ExitedWarzoneCallback()
    {
        bulletsParent.gameObject.SetActive(false);

        Reload();
    }

    private void Reload()
    {
        bulletsShot = 0;

        for (int i = 0; i < bulletsParent.childCount; i++)
        {
            bulletsParent.GetChild(i).GetComponent<Image>().color = activeColor;
        }
    }

    public bool CanShot()
    {
        return bulletsShot < bulletsParent.childCount;
    }
}