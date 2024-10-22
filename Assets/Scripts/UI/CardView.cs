using Entities.Plants;
using Events;
using Managers;
using UnityEngine.UI;
using UnityEngine;

namespace UI
{
    public class CardView : MonoBehaviour
    {
        public GameObject contentSection;
        public ToggleGroup toggleGroup;
        public GameObject cardPreset;
        public Text timeText;

        public GameObject selectPlantPrefab = null;

        public DeployPlant deployPlant;
        
        private void Start()
        {
            selectPlantPrefab = null;
            foreach (Transform child in contentSection.transform) Destroy(child.gameObject);

            foreach (var plant in StageManager.Instance.plantSets)
            {
                var obj = Instantiate(cardPreset, contentSection.transform);

                var texts = obj.GetComponentsInChildren<Text>();
                var toggle = obj.GetComponent<Toggle>();
                texts[0].text = plant.gameObject.name;
                texts[1].text = plant.GetComponent<Plant>().cost.ToString();

                toggle.group = toggleGroup;
                toggle.isOn = false;
                toggle.onValueChanged.AddListener((bool active) =>
                {
                    if (active)
                    {
                        Debug.Log($"Select Plant: {plant.gameObject.name}");
                        selectPlantPrefab = plant;
                    }
                });

                var plantPreviewObj = Instantiate(plant, obj.transform);
                Destroy(plantPreviewObj.GetComponent<Unit>());
                plantPreviewObj.transform.localPosition = new Vector3(0, 0, 0);
                plantPreviewObj.transform.localRotation = Quaternion.Euler(0,-40,0);
                plantPreviewObj.transform.localScale = Vector3.one * 50;

            }
        }

        void Update()
        {
            timeText.text = $"{(int)StageManager.Instance.timer}\n-------\n{(int)StageManager.Instance.endTime}";
        }
    }
}