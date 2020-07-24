using System;
using System.IO;

namespace WebServiceGilBT.Shared {
    class Debuger {
        const string debugfilename = "log.txt";
        static DateTime now;
        //writefile locker in case other hreads
        static object wf_locker = new object();

        static private void p(string txt) {
#if DEBUG
            now = DateTime.Now;
            Console.Write(now.ToString());
            Console.Write("-> ");
            Console.WriteLine(txt);
#else
				//dodajemy 2 h dla serwera gdzies za granica
                now = DateTime.Now.AddHours(2);
				lock(wf_locker){
					using (FileStream fs = new FileStream(debugfilename, FileMode.Append)) {
						using (BinaryWriter bw = new BinaryWriter(fs)) {
							bw.Write(now.ToString());
							bw.Write("-> ");
							bw.Write(txt);
							bw.Write("\n");
						}
					}
				}
#endif
        }

        static public void PrintLn(string txt) {
            String output = string.Format(txt);
            p(output);
        }

        static public void PrintLn(string txt, object arg0) {
            string output = string.Format(txt, arg0);
            p(output);
        }

        static public void PrintLn(string txt, object arg0, object arg1) {
            string output = string.Format(txt, arg0, arg1);
            p(output);
        }

        static public void PrintLn(string txt, object arg0, object arg1, object arg2) {
            string output = string.Format(txt, arg0, arg1, arg2);
            p(output);
        }

        static public void PrintLn(string txt, object arg0, object arg1, object arg2, object arg3) {
            string output = string.Format(txt, arg0, arg1, arg2, arg3);
            p(output);
        }
    }
}
