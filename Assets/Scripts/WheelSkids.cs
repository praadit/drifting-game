using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSkids : MonoBehaviour {
	[SerializeField] float intensityMod = 1.5f;

	Skidmarks skidmarksController;
	CarController _car;
    CarController_AutoAcc _autoAccCar;
    CarController_AutoTurn _autoTurnCar;
    ParticleSystem smoke;
	int lastSkidId = -1;
	
	void Start () {
		skidmarksController = FindObjectOfType<Skidmarks>();
		_car = GetComponentInParent<CarController>();
        _autoAccCar = GetComponentInParent<CarController_AutoAcc>();
        _autoTurnCar = GetComponentInParent<CarController_AutoTurn>();
        smoke = GetComponent<ParticleSystem>();
	}
	
	void LateUpdate () {
        if (_car != null)
        {
            float intensity = _car.SideSlipAmount;
            DriftEffect(intensity);
        }
        else if (_autoAccCar != null)
        {
            float intensity = _autoAccCar.SideSlipAmount;
            DriftEffect(intensity);
        }
        else if (_autoTurnCar)
        {
            float intensity = _autoTurnCar.SideSlipAmount;
            DriftEffect(intensity);
        }
	}

    private void DriftEffect(float intensity)
    {
        if (intensity < 0)
            intensity = -intensity;

        if (intensity > 0.2f)
        {
            lastSkidId = skidmarksController.AddSkidMark(transform.position, transform.up, intensity * intensityMod, lastSkidId);
            if (smoke != null) smoke.Play();

        }
        else
        {
            lastSkidId = -1;
            if (smoke != null && smoke.isPlaying) smoke.Stop();
        }
    }
}
