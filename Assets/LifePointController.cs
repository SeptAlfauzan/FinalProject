using UnityEngine;
using UnityEngine.UI;
public class LifePointController : MonoBehaviour
{
    [SerializeField] private SceneInfo sceneInfo;
    [SerializeField] private RawImage lifePointImage;
    void Start()
    {
        for (int i = 0; i < sceneInfo.lifePoint; i++){
            Instantiate(lifePointImage, this.transform);
        }
    }
}
