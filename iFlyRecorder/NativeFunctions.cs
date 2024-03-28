using System.Runtime.InteropServices;

namespace ElderlyCare.VoiceRecognition
{
    internal static class NativeFunctions
    {

        [DllImport("record_util.dll", EntryPoint = "init_recorder", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool InitRecorder(
            [Out][In] ref IntPtr handle,
            [In] string loginParams,
            [MarshalAs(UnmanagedType.FunctionPtr)] ResultCallback resultCallback,
            [MarshalAs(UnmanagedType.FunctionPtr)] StartCallback startCallback,
            [MarshalAs(UnmanagedType.FunctionPtr)] StopCallback stopCallback
            );

        [DllImport("record_util.dll", EntryPoint = "start_record_mic", CallingConvention = CallingConvention.Cdecl)]
        public static extern int StartRecording(IntPtr handle);

        [DllImport("record_util.dll", EntryPoint = "stop_record_mic", CallingConvention = CallingConvention.Cdecl)]
        public static extern int StopRecording(IntPtr handle);

        [DllImport("record_util.dll", EntryPoint = "free_record", CallingConvention = CallingConvention.Cdecl)]
        public static extern void FreeRecorder(IntPtr handle);
    }

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void ResultCallback(string str, int isLast);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void StartCallback();
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void StopCallback(int reason);
}
