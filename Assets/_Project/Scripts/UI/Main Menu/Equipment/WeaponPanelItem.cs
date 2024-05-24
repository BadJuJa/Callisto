using BadJuja.Core;
using BadJuja.Core.Data;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BadJuja.UI.MainMenu.EquipmentScreen {
    public class WeaponPanelItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {

        public PlayerCurrentEquipment PlayerCurrentEquipment;
        public TextMeshProUGUI Text;

        public WeaponSelection WeaponSelection;
        public ButtonsActions ButtonsActions;

        private PlayerWeaponTypes _weaponType;
        private WeaponData _weaponData;
        private string _descriptionString = "";
        private string _oldText;

        private SelectionOutlineHandler _outlineHandler;
        private UIOutline _outline;

        private void OnEnable()
        {
            if (_outline.IsSelected)
                Text.SetText(_descriptionString);
        }

        private void Awake()
        {
            _outlineHandler = GetComponentInParent<SelectionOutlineHandler>();
            _outline = GetComponent<UIOutline>();
        }

        public void Init(WeaponData weaponData, PlayerWeaponTypes weaponType)
        {
            _weaponData = weaponData;
            _weaponType = weaponType;

            _descriptionString += $"{_weaponData.Name}\nDamage: {_weaponData.Damage}\nRadius: {_weaponData.Radius}\n";

            switch (_weaponType)
            {
                case PlayerWeaponTypes.Melee:
                    _descriptionString += (_weaponData as MeleeWeaponData).Behaviour.ToString();
                    break;
                case PlayerWeaponTypes.Light:
                case PlayerWeaponTypes.Heavy:
                    RangedWeaponData _ = _weaponData as RangedWeaponData;
                    _descriptionString += $"Fire rate: {_.FireRate}\n\nBehaviour: {_.Behaviour}";
                    break;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _oldText = Text.text;

            Text.SetText(_descriptionString);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!_outline.IsSelected)
                Text.SetText(_oldText);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _outlineHandler.ChangeSelectedItem(_outline);
            _oldText = _descriptionString;
            switch (_weaponType)
            {
                case PlayerWeaponTypes.Melee:
                    PlayerCurrentEquipment.MeleeWeaponData = _weaponData as MeleeWeaponData;
                    WeaponSelection.ShowLightScreen();
                    break;
                case PlayerWeaponTypes.Light:
                    PlayerCurrentEquipment.LightRangedWeaponData = _weaponData as RangedWeaponData;
                    WeaponSelection.ShowHeavyScreen();
                    break;
                case PlayerWeaponTypes.Heavy:
                    PlayerCurrentEquipment.HeavyRangedWeaponData = _weaponData as RangedWeaponData;
                    break;
            }
            ButtonsActions.CheckEquipment();
        }

        
    }
}