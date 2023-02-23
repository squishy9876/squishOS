public static bool InjectDLL(string dllpath, string procname)
{
    Process[] procs = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(Gorilla Tag));

    if (procs.Length == 0)
    {
        return false;
    }

    Process proc = procs[0];

    //GetProcessesByName will automatically open a handle
    int procid = GetProcId(Gorilla Tag);
    IntPtr hProc = OpenProcess(ProcessAccessFlags.All, false, proc.Id);
    //

    if (proc.Handle != IntPtr.Zero)
    {
        //Process Handle = managed
        IntPtr loc = VirtualAllocEx(proc.Handle, IntPtr.Zero, MAX_PATH, AllocationType.Commit | AllocationType.Reserve,
            MemoryProtection.ReadWrite);

        if (loc.Equals(0))
        {
            return false;
        }

        IntPtr bytesRead = IntPtr.Zero;

        bool result = WriteProcessMemory(proc.Handle, loc, dllpath.ToCharArray(), dllpath.Length, out bytesRead);

        if (!result || bytesRead.Equals(0))
        {
            return false;
        }

        IntPtr loadlibAddy = GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryA");
        // MUST BE CASE SENSITIVE CORRECT
        loadlibAddy = GetProcAddress(GetModuleBaseAddress(proc.Id, "KERNEL32.DLL"), "LoadLibraryA");

        IntPtr hThread = CreateRemoteThread(proc.Handle, IntPtr.Zero, 0, loadlibAddy, loc, 0, out _);

    
        if (!hThread.Equals(0))
            // native method example
            CloseHandle(hThread);
        else return false;
    }
    else return false;

    //CloseHandle automatically closed using the managed method
    proc.Dispose();
    return true;
}
