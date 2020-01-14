using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
	public class AdventureGame : MonoBehaviour
	{
		[SerializeField] Text textComponent;
		[SerializeField] State introduction;
		[SerializeField] Image imageBack;

		State state;

		// Start is called before the first frame update
		void Start()
		{
			state = introduction;
			textComponent.text = state.GetStateStory();
			imageBack.sprite = state.GetImageState();
		}

		// Update is called once per frame
		void Update()
		{
			ManageState();
		}

		private void ManageState()
		{
			var nextStates = state.GetNextState();

			if (state.name.Equals("Introduction") || state.name.Contains("End"))
			{
				if (Input.GetKeyDown(KeyCode.Space)) state = nextStates[0];
			}
			else if (state.name.Contains("false"))
			{
				if (Input.GetKeyDown(KeyCode.R)) state = nextStates[0];
			}
			else
			{
				for (var i = 0; i < nextStates.Length; i++)
				{
					if (Input.GetKeyDown(KeyCode.A + i))
					{
						state = nextStates[i];
					}
				}
			}

			textComponent.text = state.GetStateStory();
			imageBack.sprite = state.GetImageState();
		}
	}
}
