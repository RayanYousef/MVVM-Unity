using UnityEngine;
using UnityEngine.UI;

namespace AdorableAssets.MVVM
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image _healthBarFillImage;
        [SerializeField] private Text _healthValueText;

        public Image HealthBarFillImage  => _healthBarFillImage;
        public Text HealthValueText  => _healthValueText; 
    }
}
