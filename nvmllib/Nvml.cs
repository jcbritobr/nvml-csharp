using System;
using System.Runtime.InteropServices;

namespace Nvidia.Nvml
{
    internal static class Api
    {
        const string NvmlSharedLibrary = "nvml.dll";

        [DllImport(NvmlSharedLibrary, EntryPoint = "nvmlInit")]
        internal static extern NvmlReturn NvmlInit();
        [DllImport(NvmlSharedLibrary, EntryPoint = "nvmlInit_v2")]
        internal static extern NvmlReturn NvmlInitV2();
        [DllImport(NvmlSharedLibrary, EntryPoint = "nvmlDeviceGetHandleByIndex")]
        internal static extern NvmlReturn NvmlDeviceGetHandleByIndex(uint index, out IntPtr device);
        [DllImport(NvmlSharedLibrary, EntryPoint = "nvmlDeviceGetTemperature")]
        internal static extern NvmlReturn NvmlDeviceGetTemperature(IntPtr device, NvmlTemperatureSensor sensorType, out uint temperature);
        [DllImport(NvmlSharedLibrary, EntryPoint = "nvmlShutdown")]
        internal static extern NvmlReturn NvmlShutdown();
    }

    public class NvGpu
    {
        public static void NvmlInitV2()
        {
            NvmlReturn res;
            res = Api.NvmlInitV2();
            if (NvmlReturn.NVML_SUCCESS != res)
            {
                throw new SystemException(res.ToString());
            }
        }

        public static void NvmlShutdown()
        {
            NvmlReturn res;
            res = Api.NvmlInitV2();
            if (NvmlReturn.NVML_SUCCESS != res)
            {
                throw new SystemException(res.ToString());
            }
        }
    }
}