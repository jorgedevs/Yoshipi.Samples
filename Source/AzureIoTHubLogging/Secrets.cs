namespace AzureIoTHubLogging;

public class Secrets
{
    /// <summary>
    /// Name of the Azure IoT Hub created
    /// </summary>
    public const string HUB_NAME = "HUB_NAME";

    /// <summary>
    /// Name of the Azure IoT Hub created
    /// </summary>
    public const string DEVICE_ID = "DEVICE_ID";

    /// <summary>
    /// example "SharedAccessSignature sr=MeadowIoTHub ..... "
    /// 
    /// az iot hub generate-sas-token --hub-name jorgedevs-iot-hub-02 --device-id jorgedevs-device --resource-group jorgedevs-resource-group --login HostName=jorgedevs-iot-hub-02.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=DaCmwV1p9T1AVyDwfKofdGmt4titFwRbkAIoTBdwwMk=
    /// </summary>
    public const string SAS_TOKEN = "SAS_TOKEN";
}