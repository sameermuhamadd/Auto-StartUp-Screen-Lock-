using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;

namespace StartUpApp
{
    public partial class Form1 : Form
    {
        FilterInfoCollection filterInfoCollection;
        VideoCaptureDevice videoCaptureDevice;

        public Form1()
        {
            InitializeComponent();
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams params_ = base.CreateParams;
                params_.ClassStyle |= 0x200;
                return params_;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                if (textBox1.Text == "virus")
                {
                    Application.Exit();
                   // timer1.Stop();
                }
                else
                {
                    MessageBox.Show("INTRUDER ALERT!!");
                }
            }
          
        }

        int milisec = 120;
        public void showTime()
        {
            label3.Text = (milisec).ToString();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            milisec--;
            showTime();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (textBox1.Text != "")
                {
                    if (textBox1.Text == "virus")
                    {
                        Application.Exit();
                       // timer1.Stop();
                    }
                    else
                    {
                        MessageBox.Show("INTRUDER ALERT!!");
                    }
                }
            }
            //else
            //{
            //    MessageBox.Show("ACCES DENIED");
            //}
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo filterInfo in filterInfoCollection)
            {
                comboBox1.Items.Add(filterInfo.Name);
            }
            comboBox1.SelectedIndex = 0;
            videoCaptureDevice = new VideoCaptureDevice(filterInfoCollection[comboBox1.SelectedIndex].MonikerString);
            videoCaptureDevice.NewFrame += videoCaptureDevice_NewFrame;
            videoCaptureDevice.Start();

        }
        private void videoCaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pictureBox3.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (videoCaptureDevice.IsRunning == true)
            {
                videoCaptureDevice.Stop();
            }
        }
    }
}
