﻿using Meadow.Units;

namespace AmbientMonitor.Models;

public class AtmosphericConditions
{
    public Temperature Temperature { get; set; }

    public Pressure Pressure { get; set; }

    public RelativeHumidity Humidity { get; set; }
}