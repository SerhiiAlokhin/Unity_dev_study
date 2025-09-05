using UnityEngine;
using MoreMountains.Feedbacks;

public class HudFx : MonoBehaviour
{
    public static HudFx I { get; private set; }

    [SerializeField] private MMF_Player MyFeedbacks;

    private void Awake()
    {
        if (I != null && I != this)
        {
            Destroy(gameObject);
            return;
        }

        I = this;

    }

    public void PlayCounter()
    {
        if (MyFeedbacks != null)
        {
            MyFeedbacks.PlayFeedbacks();
        }
    }
}