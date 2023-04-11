using System.Runtime.InteropServices;

namespace FireTaskMan.Helpers
{
    class WindowsSystemDispatcherQeueuHelper
    {
        //this handles the struct layout
        [StructLayout(LayoutKind.Sequential)]

        //thread struct for window
        struct DispatcherQueueOptions
        {
            //this to the byte size
            internal int dwSize;
            //this corresponds to the thread
            internal int threadType;
            //this is the window place
            internal int apartmentType;
        }

        //this is the window manager dll
        [DllImport("CoreMessaging.dll")]

        //this handles the qeue
        // CORRECT NAME: CreateDispatcherQueueController
        private static extern int CreateDispatcherQueueController([In] DispatcherQueueOptions options, [In, Out, MarshalAs(UnmanagedType.IUnknown)] ref object dispatcherQueueController);

        //the object part
        object m_dispactqueuController = null;

        //this is the handle class

        public void EnsureDispatchQeuerController()
        {
            if (Windows.System.DispatcherQueue.GetForCurrentThread() != null)
            {
                //this is default
                return;
            }

    

            if (m_dispactqueuController == null)
            {
                DispatcherQueueOptions options;
                //this values have to by exactly the same as shown else it wont work
                options.dwSize = Marshal.SizeOf(typeof(DispatcherQueueOptions));
                options.threadType = 2;
                options.apartmentType = 2;
          

                //this creates the handler
                CreateDispatcherQueueController(options, ref m_dispactqueuController);
            }
        }

        //now run to see if no error 
    }
}
