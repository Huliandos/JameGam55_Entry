using UnityEngine;
using UnityEngine.Events;

public class Beautify : MonoBehaviour
{
    [SerializeField] GameObject CurlyBlondeWig;
    [SerializeField] GameObject FullLips;
    [SerializeField] GameObject GOSportsLeft;
    [SerializeField] GameObject GOSportsRight;
    [SerializeField] GameObject BowTie;
    [SerializeField] GameObject RedBootsLeft;
    [SerializeField] GameObject RedBootsRight;

    static public int collectedItems = 0;

    public UnityEvent<int> OnCollectedItemsUpdated;

    public void EnableCurlyBlondeWig()
    {
        CurlyBlondeWig.SetActive(true);
        collectedItems++;

        OnCollectedItemsUpdated?.Invoke(collectedItems);
    }
    public void EnableFullLips()
    {
        FullLips.SetActive(true);
        collectedItems++;

        OnCollectedItemsUpdated?.Invoke(collectedItems);
    }
    public void EnableGOSports()
    {
        GOSportsLeft.SetActive(true);
        GOSportsRight.SetActive(true);
        collectedItems++;

        OnCollectedItemsUpdated?.Invoke(collectedItems);
    }
    public void EnableBowTie()
    {
        BowTie.SetActive(true);
        collectedItems++;

        OnCollectedItemsUpdated?.Invoke(collectedItems);
    }
    public void EnableRedBoots()
    {
        RedBootsLeft.SetActive(true);
        RedBootsRight.SetActive(true);
        collectedItems++;

        OnCollectedItemsUpdated?.Invoke(collectedItems);
    }


}
