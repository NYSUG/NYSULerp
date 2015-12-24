### NYSULerp.cs tool for lerping

### Usage

In order to simplify and make lerping across a game uniform use this utility. Here's how to use it and then we'll explain the different methods.

To properly lerp with this tool you will need to declare 4 variables:

    private float _lerpTime = 5f;
    private float _lerpValue = 0.0f;
    private bool _shouldLerp;
    private string _lerpRequestGuid = string.Empty;

Next you will need to write a method that will create a lerp request:

    private void SetThrustActivityLerpRequest (float output)
	{
		_setCurrentThrust = output;

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

    private void LerpCurrentThrustAnimation ()
	{
		float perc = TBLerp.LerpSmootherStep (_lerpRequestGuid);
		if (perc == 1f) {
			_shouldThrustLerp = false;
			TBLerp.RemoveLerpRequestor (_lerpRequestGuid);
		}

		_currentThrust = Mathf.Lerp (_currentThrust, _setCurrentThrust, perc);
		_animator.SetFloat ("Acceleration", _currentThrust/100);
		AkSoundEngine.SetRTPCValue("ThrustPower", _currentThrust);
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
