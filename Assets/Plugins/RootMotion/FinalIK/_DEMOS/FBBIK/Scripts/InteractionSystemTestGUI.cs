using UnityEngine;
using System.Collections;
using RootMotion.FinalIK;

namespace RootMotion.Demos {

	/// <summary>
	/// Simple GUI for quickly testing out interactions.
	/// </summary>
	public class InteractionSystemTestGUI : MonoBehaviour
	{

		public Transform parentTransform;
		public Transform originTransform;
		public GameObject pickupObject;
		
		[Tooltip("The object to interact to")]
		public InteractionObject interactionObject;
		[Tooltip("The effectors to interact with")]
        public FullBodyBipedEffector[] effectors;

		private InteractionSystem interactionSystem;
		
		void Awake() {
			interactionSystem = GetComponent<InteractionSystem>();
		}

		void OnGUI() {
			if (interactionSystem == null) return;

			if (GUILayout.Button("Start Interaction With " + interactionObject.name)) {
				if (effectors.Length == 0) Debug.Log("Please select the effectors to interact with.");

				foreach (FullBodyBipedEffector e in effectors) {
					interactionSystem.StartInteraction(e, interactionObject, true);
				}
			}

			if (effectors.Length == 0) return;

			if (interactionSystem.IsPaused(effectors[0])) {
				if (GUILayout.Button("Resume Interaction With " + interactionObject.name)) {

					interactionSystem.ResumeAll();
				}
			}
		}

		public void InteractionStart()
		{
			foreach (FullBodyBipedEffector e in effectors) 
				interactionSystem.StartInteraction(e, interactionObject, true);
		}

		public void InteractionEnd()
		{
			interactionSystem.ResumeAll();
			pickupObject.transform.SetParent(originTransform);
		}

		public void ActivateAvatar()
		{
			pickupObject.transform.SetParent(parentTransform);
			pickupObject.transform.localPosition = new Vector3(0.0199999996f, 0.856000006f, 0.586000025f);
			pickupObject.transform.localRotation = new Quaternion(-0.0414019376f, -0.873169065f, -0.0465670004f, -0.483418286f);
		}
	}
}
