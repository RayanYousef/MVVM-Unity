using UnityEngine;
using UnityEngine.UI;

namespace AdorableAssets.MVVM
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image _healthBarFillImage;
        [SerializeField] private Text _healthValueText;

        public Image HealthBarFillImage { get => _healthBarFillImage; set => _healthBarFillImage = value; }
        public Text HealthValueText { get => _healthValueText; set => _healthValueText = value; }
    }
}
