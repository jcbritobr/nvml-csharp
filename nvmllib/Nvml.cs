using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Encodings;

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
        [DllImport(NvmlSharedLibrary, EntryPoint = "nvmlSystemGetCudaDriverVersion_v2")]
        internal static extern NvmlReturn NvmlSystemGetCudaDriverVersionV2(out int cudaDriverVersion);
        [DllImport(NvmlSharedLibrary, CharSet = CharSet.Ansi, EntryPoint = "nvmlSystemGetDriverVersion")]
        internal static extern NvmlReturn NvmlSystemGetDriverVersion([Out, MarshalAs(UnmanagedType.LPArray)] byte[] version, uint length);
        [DllImport(NvmlSharedLibrary, CharSet = CharSet.Ansi, EntryPoint = "nvmlSystemGetNVMLVersion")]
        internal static extern NvmlReturn NvmlSystemGetNVMLVersion([Out, MarshalAs(UnmanagedType.LPArray)] byte[] version, uint length);
        [DllImport(NvmlSharedLibrary, CharSet = CharSet.Ansi, EntryPoint = "nvmlSystemGetProcessName")]
        internal static extern NvmlReturn NvmlSystemGetProcessName(uint pid, [Out, MarshalAs(UnmanagedType.LPArray)] byte[] name, uint length);
        [DllImport(NvmlSharedLibrary, CharSet = CharSet.Ansi, EntryPoint = "nvmlDeviceGetComputeRunningProcesses")]
        internal static extern NvmlReturn NvmlDeviceGetComputeRunningProcesses(IntPtr device, out uint infoCount, [Out, MarshalAs(UnmanagedType.LPArray)] NvmlProcessInfo[] infos);
    }

    public class NvGpu
    {
        public const uint NVML_INIT_FLAG_NO_GPUS = 1;
        public const uint NVML_INIT_FLAG_NO_ATTACH = 2;

        public static int CudaDriverVersionMajor(int version)
        {
            return version / 1000;
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