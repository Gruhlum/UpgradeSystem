using HexTecGames.Basics.UI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HexTecGames.UpgradeSystem.TowerDefense
{
    public class UpgradeDisplay : Display<UpgradeDisplay, Upgrade>, IPointerMoveHandler
    {
        [SerializeField] private TMP_Text descriptionGUI = default;

        public void OnPointerMove(PointerEventData eventData)
        {
            Vector2 mousePosition = eventData.position;
            //mousePosition.z = 0;
            int index = TMP_TextUtilities.FindIntersectingLink(descriptionGUI, mousePosition, Camera.main);
            if (index <= 0)
            {
                return;
            }

            TMP_LinkInfo linkInfo = descriptionGUI.textInfo.linkInfo[index];
            string linkID = linkInfo.GetLinkID();
        }

        protected override void DrawItem(Upgrade item)
        {
            //descriptionGUI.text = item.GetDescription();
        }
    }
}