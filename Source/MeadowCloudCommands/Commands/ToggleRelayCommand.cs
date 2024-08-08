﻿using Meadow.Cloud;

namespace MeadowCloudCommands.Commands;

/*

Command Name:

    ToggleRelayCommand

Arguments:

{
    "Relay" : 4,
    "IsOn": true
}

*/

public class ToggleRelayCommand : IMeadowCommand
{
    public int Relay { get; set; }

    public bool IsOn { get; set; }
}