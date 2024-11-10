using UnityEngine;

public class Airplane
{
    public string WingModel { get; set; }
    public string LandingGear { get; set; }
    public string EngineModel { get; set; }

    public Airplane(string wingModel, string landingGear, string engineModel)
    {
        WingModel = wingModel;
        LandingGear = landingGear;
        EngineModel = engineModel;
    }
}
