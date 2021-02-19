using System;
using System.Windows.Forms;
using Roscosmos.Properties;

namespace Roscosmos
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private (double, double) pos;

        private double t;
        private const double dt = 0.01;

        private void launchButton_Click(object sender, EventArgs e)
        {
            pauseButton.BackgroundImage = Resources.circled_pause;
            pauseButton.Visible = true;

            Body.Y0 = (double) edHeight.Value;
            Body.V0 = (double) edSpeed.Value;
            Body.Angle = (double) edAngle.Value;
            Body.Area = (double) edArea.Value;
            Body.Mass = (double) edMass.Value;

            pos.Item1 = 0;
            pos.Item2 = Body.Y0;
            t = 0;

            Body.InitFlightParams();

            chart1.ChartAreas[0].AxisX.Maximum = Math.Round(Body.MaxDistance, 3);
            chart1.ChartAreas[0].AxisY.Maximum = Math.Round(Body.MaxHeight, 3);
            chart1.Series[0].Points.Clear();
            chart1.Series[0].Points.AddXY(pos.Item1, pos.Item2);

            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label4.Text = Math.Round(t, 2).ToString();

            t += dt;
            Body.CalculatePosition(ref pos, dt);
            chart1.Series[0].Points.AddXY(pos.Item1, pos.Item2);
            if (!(pos.Item2 <= 0)) return;
            timer1.Stop();
            pauseButton.Visible = false;
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            timer1.Enabled = !timer1.Enabled;
            pauseButton.BackgroundImage =
                timer1.Enabled ? Resources.circled_pause : Resources.circled_play;
        }
    }
}