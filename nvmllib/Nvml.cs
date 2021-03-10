using System;
using System.Runtime.InteropServices;

namespace Nvidia.Nvml
{
    internal static class Api
    {
        const string NvmlSharedLibrary = "nvml.dll";

        [DllImport(NvmlSharedLibrary, EntryPoint = "nvmlInit")]
        internal static extern NvmlReturn NvmlInit();
        [DllImport(NvmlSharedLibrary, EntryPoint = "nvmlInitWithFlags")]
        internal static extern NvmlReturn NvmlInitWithFlags(uint flags);
        [DllImport(NvmlSharedLibrary, EntryPoint = "nvmlInit_v2")]
        internal static extern NvmlReturn NvmlInitV2();
        [DllImport(NvmlSharedLibrary, EntryPoint = "nvmlDeviceGetHandleByIndex")]
        internal static extern NvmlReturn NvmlDeviceGetHandleByIndex(uint index, out IntPtr device);
        [DllImport(NvmlSharedLibrary, EntryPoint = "nvmlDeviceGetTemperature")]
        internal static extern NvmlReturn NvmlDeviceGetTemperature(IntPtr device, NvmlTemperatureSensor sensorType, out uint temperature);
        [DllImport(NvmlSharedLibrary, EntryPoint = "nvmlShutdown")]
        internal static extern NvmlReturn NvmlShutdown();
        [DllImport(NvmlSharedLibrary, EntryPoint = "nvmlSystemGetCudaDriverVersion")]
        internal static extern NvmlReturn NvmlSystemGetCudaDriverVersion(out int cudaDriverVersion);
    }

    public class NvGpu
    {
        public const uint NVML_INIT_FLAG_NO_GPUS = 1;
        public const uint NVML_INIT_FLAG_NO_ATTACH = 2;

        public static int CudaDriverVersionMajor(int version)
        {
            return version / 1000;
        }

        public static int NvmlSystemGetCudaDriverVersion()
        {
            int driverVersion;
            NvmlReturn res;
            res = Api.NvmlSystemGetCudaDriverVersion(out driverVersion);
            if (NvmlReturn.NVML_SUCCESS != res)
            {
                throw new SystemException(res.ToString());
            }

            return driverVersion;
        }

        public static void NvmlInitV2()
        {
            NvmlReturn res;
            res = Api.NvmlInitV2();
            if (NvmlReturn.NVML_SUCCESS != res)
            {
                throw new SystemException(res.ToString());
            }
        }

        public static void NvmlInit()
        {
            NvmlReturn res;
            res = Api.NvmlInit();
            if (NvmlReturn.NVML_SUCCESS != res)
            {
                throw new SystemException(res.ToString());
            }
        }

        public static void NvmlInitWithFlags(uint flags)
        {
            NvmlReturn res;
            res = Api.NvmlInitWithFlags(flags);
            if (NvmlReturn.NVML_SUCCESS != res)
            {
                throw new SystemException(res.ToString());
            }
        }

        public static void NvmlShutdown()
        {
            NvmlReturn res;
            res = Api.NvmlShutdown();
            if (NvmlReturn.NVML_SUCCESS != res)
            {
                throw new SystemException(res.ToString());
            }
        }

        public static IntPtr NvmlDeviceGetHandleByIndex(uint index)
        {
            var device = IntPtr.Zero;
            NvmlReturn res;
            res = Api.NvmlDeviceGetHandleByIndex(index, out device);
            if (NvmlReturn.NVML_SUCCESS != res)
            {
                throw new SystemException(res.ToString());
            }

            return device;
        }

        public static uint NvmlDeviceGetTemperature(IntPtr device, NvmlTemperatureSensor sensorType)
        {
            NvmlReturn res;
            uint temperature;
            res = Api.NvmlDeviceGetTemperature(device, sensorType, out temperature);
            if (NvmlReturn.NVML_SUCCESS != res)
            {
                throw new SystemException(res.ToString());
            }

            return (uint)temperature;
        }
    }
}