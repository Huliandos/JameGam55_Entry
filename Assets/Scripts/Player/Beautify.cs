using UnityEngine;

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

    public void EnableCurlyBlondeWig()
    {
        CurlyBlondeWig.SetActive(true);
        collectedItems++;
    }
    public void EnableFullLips()
    {
        FullLips.SetActive(true);
        collectedItems++;
    }
    public void EnableGOSports()
    {
        GOSportsLeft.SetActive(true);
        GOSportsRight.SetActive(true);
        collectedItems++;
    }
    public void EnableBowTie()
    {
        BowTie.SetActive(true);
        collectedItems++;
    }
    public void EnableRedBoots()
    {
        RedBootsLeft.SetActive(true);
        RedBootsRight.SetActive(true);
        collectedItems++;
    }


}
