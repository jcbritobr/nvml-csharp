using System.Runtime.InteropServices;

namespace Nvidia.Nvml
{
    [StructLayout(LayoutKind.Sequential)]
    public struct NvmlProcessInfo
    {
        uint pid;
        ulong usedGpuMemory;
        uint gpuInstanceId;
        uint computeInstanceId;

        public uint Pid { get { return pid; } set { pid = value; } }
        public ulong UsedGpuMemory { get { return usedGpuMemory; } set { usedGpuMemory = value; } }
        public uint GpuInstanceId { get { return gpuInstanceId; } set { gpuInstanceId = value; } }
        public uint ComputeInstanceId { get { return computeInstanceId; } set { computeInstanceId = value; } }
    }
}