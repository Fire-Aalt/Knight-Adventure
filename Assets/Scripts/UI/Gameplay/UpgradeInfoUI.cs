using Game.UI;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class UpgradeInfoUI : MonoBehaviour
    {
        public static readonly int MaxDisplayedMods = 3;
        public event Action<UpgradeInfoUI> OnSlotClicked;

        [Title("Stats")]
        [SerializeField] private UpgradeStatUI _upgradeStatPrefab;

        [Title("References")]
        [SerializeField] private SelectableRect _selectableRect;
        [SerializeField] private RectTransform _textsHolder;
        [SerializeField] private RectTransform _statsHolder;
        [SerializeField] private Image _abilityIcon;
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private TextMeshProUGUI _upgradeText;
        [SerializeField] private TextMeshProUGUI _descriptionText;

        [Title("Look")]
        [SerializeField] private string _nameFormat;
        [SerializeField] private string _upgradeFormat;
        [SerializeField] private float _newAbilityTextY;
        [SerializeField] private float _upgradeAbilityTextY;

        public int Id { get; private set; }

        private List<UpgradeStatUI> _upgradeStatInfos;

        private void Start()
        {
            _upgradeStatInfos = new List<UpgradeStatUI>();
            for (int i = 0; i < MaxDisplayedMods; i++)
            {
                var statUI = Instantiate(_upgradeStatPrefab);
                statUI.transform.SetParent(_statsHolder, false);
                _upgradeStatInfos.Add(statUI);
            }
        }

        public void Initialize(int id)
        {
            Id = id;
        }

        [Button]
        public void PostitionForNewAbility()
        {
            _textsHolder.anchoredPosition = new Vector2(_textsHolder.anchoredPosition.x, _newAbilityTextY);
            _upgradeText.gameObject.SetActive(false);
        }

        [Button]
        public void PostitionForAbilityUpgrade()
        {
            _textsHolder.anchoredPosition = new Vector2(_textsHolder.anchoredPosition.x, _upgradeAbilityTextY);
            _upgradeText.gameObject.SetActive(true);
        }

        public void Show(float duration)
        {
            _selectableRect.Show(duration);
        }

        public void Hide(float duration)
        {
            _selectableRect.Hide(duration);
        }

        public void ShowNewAbility(AbilitySO ability)
        {
            UpdateInfo(ability);
            PostitionForNewAbility();

            for (int i = 0; i < MaxDisplayedMods; i++)
            {
                _upgradeStatInfos[i].gameObject.SetActive(false);
            }
        }

        public void ShowAbilityUpgrade(AbilitySO ability)
        {
            UpdateInfo(ability);
            _upgradeText.text = _upgradeFormat.Replace("{lvl}", (ability.UpgradeLevel + 2).ToString());
            PostitionForAbilityUpgrade();

            var mods = ability.GetNextUpgradeInfo();
            int modsToShow = Mathf.Min(mods.Count, MaxDisplayedMods);
            for (int i = 0; i < modsToShow; i++)
            {
                _upgradeStatInfos[i].gameObject.SetActive(true);
                _upgradeStatInfos[i].Initialize(ability, mods[i]);
            }
            for (int i = modsToShow; i < MaxDisplayedMods; i++)
            {
                _upgradeStatInfos[i].gameObject.SetActive(false);
            }
        }

        public void ProcessClick()
        {
            OnSlotClicked?.Invoke(this);
        }

        private void UpdateInfo(AbilitySO ability)
        {
            string title;
            title = _nameFormat.Replace("{name}", ability.abilityName);
            title = title.Replace("{lvl}", (ability.UpgradeLevel + 1).ToString());
            _abilityIcon.sprite = ability.icon;
            _titleText.text = title;
            _descriptionText.text = ability.quickDescription;
        }
    }
}
