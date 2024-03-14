using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WebSocketSharp;
using Newtonsoft.Json;

namespace Fall_fall
{
    public class Accelerator
    {
        //private dynamic values;

        //public dynamic AccVal { get; set; }
        public void Accelator()
        {

            WebSocket ws = new WebSocket("ws://192.168.2000.105:8080/sensor/connect?type=android.sensor.accelerometer");

            ws.OnMessage += Ws_OnMessage;

            ws.Connect();

                //Console.ReadKey();

                //if (this.fall_status)
                //{
                //    fall_status = false;
                //    return true;
                //} 
            

            //return false;//this.AccVal;
    }

        private void Ws_OnMessage(object? sender, MessageEventArgs e)
        {
            dynamic stuff1 = JsonConvert.DeserializeObject(e.Data);

            //FallDectection fd = new FallDectection();
            double yz = (stuff1.values[0] * stuff1.values[0]) + (stuff1.values[1] * stuff1.values[1]) + (stuff1.values[2] * stuff1.values[2]);
            dynamic TotalAcc = Math.Sqrt(yz);
            dynamic acc = TotalAcc / 9.8;
            //var acc = fd.Fall(stuff1);
            this.ACCCC = acc;

            if (acc >= 0.3 && acc <= 0.7) state = 1;
            else if (acc > 5 && state==1)
            {
                state = 0; //trigger the timer
                this.clock.Start();
                this.timePassedMili = clock.ElapsedMilliseconds;
                this.fall_status = true;
            }
            //else if (state == 2 && clock.ElapsedMilliseconds < 300)
            //{
            //    // ALso if time is less then 300 ms
            //    // trigger if values match the condition
            //    //finally timer is at 290 and ACC is stii meeting condition
            //    //we found fall
            //    if (acc >= 0.4 && acc <= 2.0)
            //    {
            //        if (this.counter > 1 && clock.ElapsedMilliseconds > 290)
            //        {
            //            this.fall_status = true;
            //            this.counter = 0;

            //        }
            //        this.counter++;

            //    }
            //}
        }

        public int state = 0;
        public bool fall_status = false;
        public Stopwatch clock = new Stopwatch();
        public double timePassedMili;
        public double ACCCC;
        public int counter = 0;

        
        
    }
}
