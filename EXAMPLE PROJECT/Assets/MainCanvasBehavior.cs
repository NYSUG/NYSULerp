using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainCanvasBehavior : MonoBehaviour {

	public Image lerpScaleImage;
	public Image lerpMoveImage;
	public Text lerpScalePercText;
	public Text lerpMovePercText;

	private float _xScale;
	private Vector3 _startPos = new Vector3 (-350f, -150f, 0);
	private Vector3 _endPos = new Vector3 (350f, -150f, 0);

	// Lerp Scale Animation
	private float _lerpTime = 5f;
	private bool _shouldLerpScale;
	private string _lerpScaleRequestGuid = string.Empty;

	// Lerp Move Animation
	private float _lerpMoveTime = 5f;
	private bool _shouldLerpMove;
	private string _lerpMoveRequestGuid = string.Empty;

	public void ScaleButtonPressed ()
	{
		// Reset the scale;
		_xScale = 0.0f;

		// Set the request
		SetLerpScaleRequest ();
	}

	public void MoveButtonPressed ()
	{
		// Reset the position
		lerpMoveImage.transform.position = _startPos;

		// Set the request
		SetLerpMoveRequest ();
	}

	private void Update ()
	{
		// Should we run the scale animation lerp?
		if (_shouldLerpScale) {
			LerpScaleAnimation ();
		}

		// Should we run the move animation lerp?
		if (_shouldLerpMove) {
			LerpMoveAnimation ();
		}
	}

	private void SetLerpScaleRequest ()
	{
		_shouldLerpScale = true;

		// Set up a lerp request
		if (string.IsNullOrEmpty (_lerpScaleRequestGuid)) {
			_lerpScaleRequestGuid = NYSULerp.AddLerpRequestor (_lerpTime);
		} else {
			// Remove the old requestor first
			NYSULerp.RemoveLerpRequestor (_lerpScaleRequestGuid);
			_lerpScaleRequestGuid = NYSULerp.AddLerpRequestor (_lerpTime);
		}
	}
	
	private void LerpScaleAnimation ()
	{
		// Choose your lerp algorithm here: i.e. TBLerp.LerpExponential (_lerpScaleRequestGuid)
		float perc = NYSULerp.LerpNoEasing (_lerpScaleRequestGuid);

		if (perc == 1f) {
			_shouldLerpScale = false;
			NYSULerp.RemoveLerpRequestor (_lerpScaleRequestGuid);
			_lerpScaleRequestGuid = string.Empty;
		}

		_xScale = 1f * perc;
		lerpScalePercText.text = _xScale.ToString ();
		lerpScaleImage.transform.localScale = new Vector3 (_xScale, 1f, 1f);
	}

	private void SetLerpMoveRequest ()
	{
		_shouldLerpMove = true;

		// Set up a lerp request
		if (string.IsNullOrEmpty (_lerpMoveRequestGuid)) {
			_lerpMoveRequestGuid = NYSULerp.AddLerpRequestor (_lerpMoveTime);
		} else {
			// Remove the old requestor first
			NYSULerp.RemoveLerpRequestor (_lerpMoveRequestGuid);
			_lerpMoveRequestGuid = NYSULerp.AddLerpRequestor (_lerpMoveTime);
		}
	}

	private void LerpMoveAnimation ()
	{
		// Choose your lerp algorithm here: i.e. TBLerp.LerpExponential (_lerpScaleRequestGuid)
		float perc = NYSULerp.LerpSmootherStep (_lerpMoveRequestGuid);
		
		if (perc == 1f) {
			_shouldLerpMove = false;
			NYSULerp.RemoveLerpRequestor (_lerpMoveRequestGuid);
			_lerpMoveRequestGuid = string.Empty;
		}
		
		lerpMoveImage.transform.localPosition = Vector3.Lerp (_startPos, _endPos, perc);
		lerpMovePercText.text = perc.ToString ();
	}
}
