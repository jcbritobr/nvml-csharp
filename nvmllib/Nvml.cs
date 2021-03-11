using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Encodings;

using NvmlDeviceArchitecture = System.UInt32;

namespace Nvidia.Nvml
{
    internal static class Api
    {
        const string NVML_SHARED_LIBRARY_STRING = "nvml.dll";

        [DllImport(NVML_SHARED_LIBRARY_STRING, EntryPoint = "nvmlInit")]
        internal static extern NvmlReturn NvmlInit();
        [DllImport(NVML_SHARED_LIBRARY_STRING, EntryPoint = "nvmlInitWithFlags")]
        internal static extern NvmlReturn NvmlInitWithFlags(uint flags);
        [DllImport(NVML_SHARED_LIBRARY_STRING, EntryPoint = "nvmlInit_v2")]
        internal static extern NvmlReturn NvmlInitV2();
        [DllImport(NVML_SHARED_LIBRARY_STRING, EntryPoint = "nvmlDeviceGetHandleByIndex")]
        internal static extern NvmlReturn NvmlDeviceGetHandleByIndex(uint index, out IntPtr device);
        [DllImport(NVML_SHARED_LIBRARY_STRING, EntryPoint = "nvmlDeviceGetTemperature")]
        internal static extern NvmlReturn NvmlDeviceGetTemperature(IntPtr device, NvmlTemperatureSensor sensorType, out uint temperature);
        [DllImport(NVML_SHARED_LIBRARY_STRING, EntryPoint = "nvmlShutdown")]
        internal static extern NvmlReturn NvmlShutdown();
        [DllImport(NVML_SHARED_LIBRARY_STRING, EntryPoint = "nvmlSystemGetCudaDriverVersion")]
        internal static extern NvmlReturn NvmlSystemGetCudaDriverVersion(out int cudaDriverVersion);
        [DllImport(NVML_SHARED_LIBRARY_STRING, EntryPoint = "nvmlSystemGetCudaDriverVersion_v2")]
        internal static extern NvmlReturn NvmlSystemGetCudaDriverVersionV2(out int cudaDriverVersion);
        [DllImport(NVML_SHARED_LIBRARY_STRING, CharSet = CharSet.Ansi, EntryPoint = "nvmlSystemGetDriverVersion")]
        internal static extern NvmlReturn NvmlSystemGetDriverVersion([Out, MarshalAs(UnmanagedType.LPArray)] byte[] version, uint length);
        [DllImport(NVML_SHARED_LIBRARY_STRING, CharSet = CharSet.Ansi, EntryPoint = "nvmlSystemGetNVMLVersion")]
        internal static extern NvmlReturn NvmlSystemGetNVMLVersion([Out, MarshalAs(UnmanagedType.LPArray)] byte[] version, uint length);
        [DllImport(NVML_SHARED_LIBRARY_STRING, CharSet = CharSet.Ansi, EntryPoint = "nvmlSystemGetProcessName")]
        internal static extern NvmlReturn NvmlSystemGetProcessName(uint pid, [Out, MarshalAs(UnmanagedType.LPArray)] byte[] name, uint length);
        [DllImport(NVML_SHARED_LIBRARY_STRING, CharSet = CharSet.Ansi, EntryPoint = "nvmlDeviceGetComputeRunningProcesses")]
        internal static extern NvmlReturn NvmlDeviceGetComputeRunningProcesses(IntPtr device, out uint infoCount, [Out, MarshalAs(UnmanagedType.LPArray)] NvmlProcessInfo[] infos);
        [DllImport(NVML_SHARED_LIBRARY_STRING, CharSet = CharSet.Ansi, EntryPoint = "nvmlDeviceGetAPIRestriction")]
        internal static extern NvmlReturn NvmlDeviceGetAPIRestriction(IntPtr device, NvmlRestrictedAPI apiType, out NvmlEnableState isRestricted);
        [DllImport(NVML_SHARED_LIBRARY_STRING, CharSet = CharSet.Ansi, EntryPoint = "nvmlDeviceGetApplicationsClock")]
        internal static extern NvmlReturn NvmlDeviceGetApplicationsClock(IntPtr device, NvmlClockType clockType, out uint clockMhz);
        [DllImport(NVML_SHARED_LIBRARY_STRING, CharSet = CharSet.Ansi, EntryPoint = "nvmlDeviceGetArchitecture")]
        internal static extern NvmlReturn NvmlDeviceGetArchitecture(IntPtr device, out NvmlDeviceArchitecture arch);
        [DllImport(NVML_SHARED_LIBRARY_STRING, CharSet = CharSet.Ansi, EntryPoint = "nvmlDeviceAttributes")]
        internal static extern NvmlReturn NvmlDeviceGetAttributes(IntPtr device, out NvmlDeviceAttributes attributes);
        [DllImport(NVML_SHARED_LIBRARY_STRING, CharSet = CharSet.Ansi, EntryPoint = "nvmlDeviceGetAutoBoostedClocksEnabled")]
        internal static extern NvmlReturn NvmlDeviceGetAutoBoostedClocksEnabled(IntPtr device, out NvmlEnableState isEnabled, out NvmlEnableState defaultIsEnabled);
        [DllImport(NVML_SHARED_LIBRARY_STRING, CharSet = CharSet.Ansi, EntryPoint = "nvmlDeviceGetBAR1MemoryInfo")]
        internal static extern NvmlReturn NvmlDeviceGetBAR1MemoryInfo(IntPtr device, NvmlBAR1Memory bar1Memory);
        [DllImport(NVML_SHARED_LIBRARY_STRING, CharSet = CharSet.Ansi, EntryPoint = "nvmlDeviceGetBoardId")]
        internal static extern NvmlReturn NvmlDeviceGetBoardId(IntPtr device, out uint boardId);
        [DllImport(NVML_SHARED_LIBRARY_STRING, CharSet = CharSet.Ansi, EntryPoint = "nvmlDeviceGetBoardPartNumber")]
        internal static extern NvmlReturn NvmlDeviceGetBoardPartNumber(IntPtr device, [Out, MarshalAs(UnmanagedType.LPArray)] byte[] partNumber, uint length);
        [DllImport(NVML_SHARED_LIBRARY_STRING, CharSet = CharSet.Ansi, EntryPoint = "nvmlDeviceGetBrand")]
        internal static extern NvmlReturn NvmlDeviceGetBrand(IntPtr device, out NvmlBrandType type);
        [DllImport(NVML_SHARED_LIBRARY_STRING, CharSet = CharSet.Ansi, EntryPoint = "nvmlDeviceGetBridgeChipInfo")]
        internal static extern NvmlReturn NvmlDeviceGetBridgeChipInfo(IntPtr device, NvmlBridgeChipHierarchy bridgeHierarchy);
        [DllImport(NVML_SHARED_LIBRARY_STRING, CharSet = CharSet.Ansi, EntryPoint = "nvmlDeviceGetClock")]
        internal static extern NvmlReturn NvmlDeviceGetClock(IntPtr device, NvmlClockType clockType, NvmlClockId clockId, out uint clockMHz);
        [DllImport(NVML_SHARED_LIBRARY_STRING, CharSet = CharSet.Ansi, EntryPoint = "nvmlDeviceGetClockInfo")]
        internal static extern NvmlReturn nvmlDeviceGetClockInfo(IntPtr device, NvmlClockType type, out uint clock);
        [DllImport(NVML_SHARED_LIBRARY_STRING, CharSet = CharSet.Ansi, EntryPoint = "nvmlDeviceGetComputeMode")]
        internal static extern NvmlReturn NvmlDeviceGetComputeMode(IntPtr device, NvmlComputeMode mode);
        [DllImport(NVML_SHARED_LIBRARY_STRING, CharSet = CharSet.Ansi, EntryPoint = "nvmlDeviceGetComputeRunningProcesses")]
        internal static extern NvmlReturn NvmlDeviceGetComputeRunningProcesses(IntPtr device, out uint infoCount, out NvmlProcessInfo infos);
        [DllImport(NVML_SHARED_LIBRARY_STRING, CharSet = CharSet.Ansi, EntryPoint = "nvmlDeviceGetCount_v2")]
        internal static extern NvmlReturn nvmlDeviceGetCount_v2(out uint deviceCount);
        [DllImport(NVML_SHARED_LIBRARY_STRING, CharSet = CharSet.Ansi, EntryPoint = "nvmlDeviceGetCudaComputeCapability")]
        internal static extern NvmlReturn NvmlDeviceGetCudaComputeCapability(IntPtr device, out int major, out int minor);
        [DllImport(NVML_SHARED_LIBRARY_STRING, CharSet = CharSet.Ansi, EntryPoint = "nvmlDeviceGetCurrPcieLinkGeneration")]
        internal static extern NvmlReturn NvmlDeviceGetCurrPcieLinkGeneration(IntPtr device, out uint currLinkGen);
        [DllImport(NVML_SHARED_LIBRARY_STRING, CharSet = CharSet.Ansi, EntryPoint = "nvmlDeviceGetCurrPcieLinkWidth")]
        internal static extern NvmlReturn NvmlDeviceGetCurrPcieLinkWidth(IntPtr device, out uint currLinkWidth);
        [DllImport(NVML_SHARED_LIBRARY_STRING, CharSet = CharSet.Ansi, EntryPoint = "nvmlDeviceGetCurrentClocksThrottleReasons")]
        internal static extern NvmlReturn NvmlDeviceGetCurrentClocksThrottleReasons(IntPtr device, out ulong clocksThrottleReasons);
        [DllImport(NVML_SHARED_LIBRARY_STRING, CharSet = CharSet.Ansi, EntryPoint = "nvmlDeviceGetDecoderUtilization")]
        internal static extern NvmlReturn NvmlDeviceGetDecoderUtilization(IntPtr device, uint utilization, out uint samplingPeriodUs);
        [DllImport(NVML_SHARED_LIBRARY_STRING, CharSet = CharSet.Ansi, EntryPoint = "nvmlDeviceGetDefaultApplicationsClock")]
        internal static extern NvmlReturn NvmlDeviceGetDefaultApplicationsClock(IntPtr device, NvmlClockType clockType, out uint clockMHz);
        [DllImport(NVML_SHARED_LIBRARY_STRING, CharSet = CharSet.Ansi, EntryPoint = "nvmlDeviceGetDetailedEccErrors")]
        internal static extern NvmlReturn NvmlDeviceGetDetailedEccErrors(IntPtr device, NvmlMemoryErrorType errorType, NvmlEccCounterType counterType, out NvmlEccErrorCounts eccCounts);
    }

    public class NvGpu
    {
        public const uint NVML_INIT_FLAG_NO_GPUS = 1;
        public const uint NVML_INIT_FLAG_NO_ATTACH = 2;

        public static int CudaDriverVersionMajor(int version)
        {
            return version / 1000;
        }

        public static string NvmlDeviceGetBoardPartNumber(IntPtr device, uint length)
        {
            byte[] partNumber = new byte[length];
            NvmlReturn res = Api.NvmlDeviceGetBoardPartNumber(device, partNumber, length);
            if (NvmlReturn.NVML_SUCCESS != res)
            {
                throw new SystemException(res.ToString());
            }

            return Encoding.Default.GetString(partNumber).Replace("\0", "");
        }

        public static NvmlEnableState NvmlDeviceGetAPIRestriction(IntPtr device, NvmlRestrictedAPI apiType)
        {
            NvmlEnableState state;
            NvmlReturn res;
            res = Api.NvmlDeviceGetAPIRestriction(device, apiType, out state);
            if (NvmlReturn.NVML_SUCCESS != res)
            {
                throw new SystemException(res.ToString());
            }

            return state;
        }

        public static (List<NvmlProcessInfo>, uint) NvmlDeviceGetComputeRunningProcesses(IntPtr device)
        {
            NvmlReturn res;
            int size = Marshal.SizeOf<NvmlProcessInfo>();
            // IntPtr buffer = Marshal.AllocHGlobal(size * 5);
            uint count = 0;

            res = Api.NvmlDeviceGetComputeRunningProcesses(device, out count, null);
            if (count <= 0)
            {
                return (new List<NvmlProcessInfo>(), count);
            }

            NvmlProcessInfo[] buffer = new NvmlProcessInfo[count];
            res = Api.NvmlDeviceGetComputeRunningProcesses(device, out count, buffer);
            if (NvmlReturn.NVML_SUCCESS != res)
            {
                throw new SystemException(res.ToString());
            }

            return (new List<NvmlProcessInfo>(buffer), count);
        }

        public static string NvmlSystemGetProcessName(uint pid, uint length)
        {
            NvmlReturn res;
            byte[] name = new byte[length];
            res = Api.NvmlSystemGetProcessName(pid, name, length);
            if (NvmlReturn.NVML_SUCCESS != res)
            {
                throw new SystemException(res.ToString());
            }

            return Encoding.Default.GetString(name).Replace("\0", "");
        }

        public static string nvmlSystemGetNVMLVersion(uint length)
        {
            NvmlReturn res;
            byte[] version = new byte[length];
            res = Api.NvmlSystemGetNVMLVersion(version, length);
            if (NvmlReturn.NVML_SUCCESS != res)
            {
                throw new SystemException(res.ToString());
            }

            return Encoding.Default.GetString(version).Replace("\0", "");
        }

        public static string nvmlSystemGetDriverVersion(uint length)
        {
            NvmlReturn res;
            byte[] version = new byte[length];
            res = Api.NvmlSystemGetDriverVersion(version, length);
            if (NvmlReturn.NVML_SUCCESS != res)
            {
                throw new SystemException(res.ToString());
            }

            return Encoding.Default.GetString(version).Replace("\0", "");
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

        public static int NvmlSystemGetCudaDriverVersionV2()
        {
            int driverVersion;
            NvmlReturn res;
            res = Api.NvmlSystemGetCudaDriverVersionV2(out driverVersion);
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