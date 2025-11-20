using UnityEngine;
using UnityEngine.Events;

public class Collectible : MonoBehaviour
{
    public enum CollectibleType {BOW_TIE, LIPSTICK, RED_BOOT, SPORT_GLOVE, BLOND_WIG}
    [SerializeField] CollectibleType _collectibleType;

    public void Collect()
    {
        switch (_collectibleType)
        {
            case CollectibleType.BOW_TIE:
                Player.PlayerBrain.Instance.GetComponent<Beautify>().EnableBowTie();
                break;
            case CollectibleType.LIPSTICK:
                Player.PlayerBrain.Instance.GetComponent<Beautify>().EnableFullLips();
                break;
            case CollectibleType.RED_BOOT:
                Player.PlayerBrain.Instance.GetComponent<Beautify>().EnableRedBoots();
                break;
            case CollectibleType.SPORT_GLOVE:
                Player.PlayerBrain.Instance.GetComponent<Beautify>().EnableGOSports();
                break;
            case CollectibleType.BLOND_WIG:
                Player.PlayerBrain.Instance.GetComponent<Beautify>().EnableCurlyBlondeWig();
                break;
        }
    }
}
