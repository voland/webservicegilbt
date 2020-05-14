using System;
using System.Collections.Generic;

//serializatoin
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace WebServiceGilBT.Shared {

    public class ScreenList {
        public List<Screen> Screens;

        public void Add(List<Screen> argList) {
            if (Screens == null) Screens = new List<Screen>();
            foreach (Screen s in argList) {
                Screens.Add(s);
            }
        }

        private static string ScreenListFileName = "ScreenList.json";

        private static object locker = new object();

        public static void Save(List<Screen> argScreens) {
            if (argScreens != null) {
                lock (locker) {
                    string serialised_list = JsonSerializer.Serialize(argScreens);
		    File.Copy(ScreenListFileName, $"{ScreenListFileName}.back");
                    File.WriteAllText(ScreenListFileName, serialised_list);
                }
            }
        }

        public static List<Screen> Load() {
            List<Screen> sl = new List<Screen>();
            lock (locker) {
                try {
                    String serialised_list = File.ReadAllText(ScreenListFileName);
                    sl = JsonSerializer.Deserialize<List<Screen>>(serialised_list);
                } catch {
                    Console.WriteLine("Reading {0} failed.", ScreenListFileName);
                }
            }
            return sl;
        }
    }
}
