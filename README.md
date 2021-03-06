### NYSULerp.cs tool for lerping
This utility was developed for my game Atomic Space Command. Check it out: http://atomicspacecommand.net

### Usage

In order to simplify and make lerping across a game uniform use this utility. Here's how to use it and then we'll explain the different methods.

To properly lerp with this tool you will need to declare 4 variables:

    private float _lerpTime = 5f;
    private float _lerpValue = 0.0f;
    private bool _shouldLerp;
    private string _lerpRequestGuid = string.Empty;

Next you will need to write a method that will create a lerp request:

    private void SetLerpMoveRequest ()
	{
		// Setup a Lerp Request
		_shouldLerp = true;
		if (string.IsNullOrEmpty (_lerpRequestGuid)) {
			_lerpRequestGuid = TBLerp.AddLerpRequestor (_lerpTime);
		} else {
			// Remove the old requestor first
			TBLerp.RemoveLerpRequestor (_lerpRequestGuid);
			_lerpRequestGuid = TBLerp.AddLerpRequestor (_lerpTime);
		}
	}

Then you will need to write a method that will burn the lerp down and set the animations:

    private void LerpMoveAnimation ()
	{
		float perc = TBLerp.LerpSmootherStep (_lerpRequestGuid);
		if (perc == 1f) {
			_shouldThrustLerp = false;
			TBLerp.RemoveLerpRequestor (_lerpRequestGuid);
		}

		this.transform.localPosition = Vector3.Lerp (_startPos, _endPos, perc);
	}

Finally you will need to add a call to the burn down method from the update loop:

    private void Update ()
	{
		// Lerp the engine activity
		if (_shouldLerp) {
			LerpCurrentPowerAnimation ();
		}
    }

### Methods
- LerpNoEasing
- LerpEaseOut
- LerpEaseIn
- LerpExponential
- LerpSmoothStep
- LerpSmootherStep

### LerpNoEasing
This is a standard linear lerp with no acceleration. Things start and stop at a constant.

### LerpEaseOut
This is performed using sinerp

![Sinerp](http://www.noyoushutupgames.com/images/github/LerpUtilDoc/interp-sinerp.png)

### LerpEaseIn
This is performed using coserp

![Coserp](http://www.noyoushutupgames.com/images/github/LerpUtilDoc/interp-coserp.png)

### LerpExponential
Performed with t * t

![Exponential/quadratic](http://www.noyoushutupgames.com/images/github/LerpUtilDoc/interp-quad.png)

### LerpSmoothStep
Uses the famous 'smooth step' formula: t = t*t * (3f - 2f*t)

![SmoothStep](http://www.noyoushutupgames.com/images/github/LerpUtilDoc/interp-smooth.png)

### LerpSmootherStep
A play on the smooth step formula: t = t*t*t * (t * (6f*t - 15f) + 10f)

![SmootherStep](http://www.noyoushutupgames.com/images/github/LerpUtilDoc/interp-smoother.png)
