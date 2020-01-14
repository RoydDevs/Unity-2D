using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
	[CreateAssetMenu(menuName = "State")]
	public class State : ScriptableObject
	{
		[TextArea(24, 10)] [SerializeField] string storyText;
		[SerializeField] State[] nextStates;
		[SerializeField] Sprite image;

		public string GetStateStory()
		{
			return storyText;
		}

		public State[] GetNextState()
		{
			return nextStates;
		}

		public Sprite GetImageState()
		{
			return image;
		}
	}
}
