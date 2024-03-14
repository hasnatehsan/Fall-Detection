using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Diagnostics.Stopwatch;


using WebSocketSharp;
using Newtonsoft.Json;
using System.Diagnostics;

namespace AccelarationTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Creating instance for webSocketserver
            WebSocket ws = new WebSocket("ws://192.168.0.27:8081/sensor/connect?type=android.sensor.accelerometer");

            ws.OnMessage += Ws_OnMessage;

            ws.Connect();

            if (fall_status)
            {
                Console.WriteLine("Fall Detected");
            }

            Console.ReadKey();
        }

        public dynamic XYZreturn(dynamic value)
        {
            return value;
        }

        private static void Ws_OnMessage(object sender, MessageEventArgs e)
        {
            dynamic stuff1 = Newtonsoft.Json.JsonConvert.DeserializeObject(e.Data);

            //FallDectection fd = new FallDectection();
            //dynamic acc = Fall(stuff1);
            //this.ACCCC = acc;
            double yz = (stuff1.values[0] * stuff1.values[0]) + (stuff1.values[1] * stuff1.values[1]) + (stuff1.values[2] * stuff1.values[2]);
            dynamic TotalAcc = Math.Sqrt(yz);
            dynamic acc = TotalAcc / 9.8;

            //Console.WriteLine(acc);
            //Console.WriteLine(clock.ElapsedMilliseconds);
            if (acc >= 0.3 && acc <= 0.7)
            {
                state = 1;
                //Console.WriteLine("Phase 1");
            }
                
            
            else if (acc > 5 && state == 1)
            {
                state = 2; //trigger the timer
                clock.Start();
                timePassedMili = clock.ElapsedMilliseconds;
                Console.WriteLine("Phase 2");
            }
            else if (state == 2 && clock.ElapsedMilliseconds < 300)
            {
                // ALso if time is less then 300 ms
                // trigger if values match the condition
                //finally timer is at 290 and ACC is stii meeting condition
                //we found fall
                

                if (acc >= 0.4 && acc <= 2.0)
                {
                    if ((clock.ElapsedMilliseconds - timePassedMili) > 290)
                    {
                        fall_status = true;
                        counter = 0;
                        clock.Stop();
                        Console.WriteLine("Phase 3-------------------");
                    }
                    //counter++;

                }
            }

            //XYZreturn(stuff1);
            //Console.WriteLine("Recieved from mobile" + stuff1.values[1]);
        }

        public static double Fall(dynamic values)
        {
            var ax = values[0];
            var ay = values[1];
            var az = values[2];

            var TotalAcc = Math.Sqrt(Math.Pow(ax, 2) + Math.Pow(ay, 2) + Math.Pow(az, 2));
            var AccMinimise = TotalAcc / 9.8;

            return AccMinimise;

        }

        public static int state = 0;
        public static bool fall_status = false;
        public static Stopwatch clock = new Stopwatch();
        public static double timePassedMili;
        public static double ACCCC;
        public static int counter = 0;

    }
}
