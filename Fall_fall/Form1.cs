using WebSocketSharp;
using Newtonsoft.Json;
using System.Diagnostics;

//using AutoClosing

namespace Fall_fall
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //using()
            
        }
        public int state = 0;
        public bool fall_status = false;
        public Stopwatch clock = new Stopwatch();
        public double timePassedMili;

        private void Ws_OnMessage(object? sender, MessageEventArgs e)
        {
            dynamic stuff1 = JsonConvert.DeserializeObject(e.Data);

            

            //FallDectection fd = new FallDectection();
            double yz = (stuff1.values[0] * stuff1.values[0]) + (stuff1.values[1] * stuff1.values[1]) + (stuff1.values[2] * stuff1.values[2]);
            dynamic TotalAcc = Math.Sqrt(yz);
            dynamic acc = TotalAcc / 9.8;


            if (acc >= 0.3 && acc <= 0.7)
            {
                state = 1;
                //MessageBox.Show("Calling for help");


            }

            else if (acc > 3 && state == 1)
            {
                state++; //trigger the timer
                this.clock.Start();
                this.timePassedMili = clock.ElapsedMilliseconds;
                //this.fall_status = true;

                //var result = MessageBox.Show("Are you okay?", "Delete", MessageBoxButtons.YesNo);
                //var result = AutoClosingMessageBox.Show(
                //    text: "Are you alright?",
                //    caption: "Hello there!",
                //    timeout: 5000,
                //    buttons: MessageBoxButtons.YesNo,
                //    defaultResult: DialogResult.No
                //    );
                //if (result == DialogResult.Yes)
                //{
                //    MessageBox.Show("Glad you are okay");
                //}
                //else
                //{
                //    //this.textBox1.Text = "Yo Yo";
                //    MessageBox.Show("Calling for help");
                //}
                //MessageBox.Show("Calling for help");
                acc = 0;

            }
            else if (state == 2 && acc > 0.4 && acc < 2.0)
            {
                state = 0; //trigger the timer
                //this.clock.Start();
                //this.timePassedMili = clock.ElapsedMilliseconds;
                this.fall_status = true;
                this.clock.Stop();

                var result = MessageBox.Show("Are you okay?", "Delete", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    MessageBox.Show("Glad you are okay");
                }
                else
                {
                    //this.textBox1.Text = "Yo Yo";
                    MessageBox.Show("Calling for help");
                }
                acc = 0;
                state= 0;
            }



            //var acc = fd.Fall(stuff1);
            //this.ACCCC = acc;


        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            WebSocket ws = new WebSocket("ws://192.168.200.105:8080/sensor/connect?type=android.sensor.accelerometer");
            //WebSocket ws = new WebSocket("ws://Redmi-Note-10-Pro:8081");
            //WebSocket ws = new WebSocket("ws://localhost:8080/sensor/connect?type=android.sensor.accelerometer");

            ws.OnMessage += Ws_OnMessage;

            ws.Connect();

            //if (this.fall_status) MessageBox.Show("Hello hello");
            state = 0;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnAnalytics_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnSettings_Click(object sender, EventArgs e)
        {

        }
    }
}