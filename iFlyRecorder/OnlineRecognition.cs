using ElderlyCare.VoiceRecognition;

namespace iFlyRecorder
{
    public class OnlineRecognition : IDisposable
    {
        public delegate void OnResultArrivedEventHandler(string result, bool isLast);
        public event OnResultArrivedEventHandler? ResultArrived;

        public delegate void OnStopEventHandler(int errCode);
        public event OnStopEventHandler? OnStop;

        public delegate void OnStartEventHandler();
        public event OnStartEventHandler? OnStart;

        public List<string> Results { get; private set; }

        private IntPtr _nativeHandle;
        private bool _disposed;

        public OnlineRecognition(string loginParams)
        {
            if(!NativeFunctions.InitRecorder(ref _nativeHandle, loginParams, ResultHandler, StartHandler, StopHandler))
            {
                throw new InvalidOperationException("Unable to initialize voice recorder.");
            }

            Results = new();
        }

        public bool StartRecord()
        {
            if (_disposed)
                throw new ObjectDisposedException("native recorder");
            int errCode = NativeFunctions.StartRecording(_nativeHandle);
            return errCode == 0;
        }

        public int StopRecord()
        {
            if (_disposed)
                throw new ObjectDisposedException("native recorder");
            return NativeFunctions.StopRecording(_nativeHandle);
        }

        private void StopHandler(int reason)
        {
            OnStop?.Invoke(reason);
        }

        private void StartHandler()
        {
            OnStart?.Invoke();
        }

        private void ResultHandler(string str, int isLast)
        {
            Results.Add(str);
            ResultArrived?.Invoke(str, isLast != 0);
        }


        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if(disposing)
                {

                }

                NativeFunctions.FreeRecorder(_nativeHandle);
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~OnlineRecognition()
        {
            Dispose(false);
        }
    }
}
