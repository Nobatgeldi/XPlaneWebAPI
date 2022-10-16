using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XPlaneConnector.DataRefs;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using ArrayToExcel;

namespace QTGTest
{
    public partial class Form1 : Form
    {
        XPlaneConnector.XPlaneConnector connector;

        List<Data> recordedData;
        private float s_pressure_altitude, s_ias, s_vertical_speed;
        private float s_left_manifold_pressure, s_right_manifold_pressure;
        private float s_left_rpm, s_right_rpm;
        private float s_left_power_lever, s_right_power_lever;
        private float s_pitch_control, s_pitch_angle, s_column_force;
        private float s_wheel_deflection, s_wheel_force;
        private float s_rudder_force, s_rudder_control;
        private float s_flaps_position, s_gear_position, s_rudder_pedal_position;
        private float s_roll_angle, s_roll_rate;
        private float s_sideslip, s_heading;
        private float s_normal_acceleration, s_stall_warning;
        private float s_instr_heading, s_instr_pitch, s_instr_roll;
        private float s_visual_heading, s_visual_pitch, s_visual_roll;

        public Form1()
        {
            InitializeComponent();
            recordedData = new List<Data>();
            //Buraya kasanin IP adresi
            connector = new XPlaneConnector.XPlaneConnector("127.0.0.1");            

        }
        private void test1_Click(object sender, EventArgs e)
        {
            connector.Subscribe(DataRefs.CockpitPressureCabinAltitudeActualMMsl, 5, (dr, value) =>
            {
                SaveData("altitude", value);
            });
            connector.Subscribe(DataRefs.Cockpit2GaugesIndicatorsAirspeedKtsPilot, 5, (dr, value) =>
            {
                SaveData("ias", value);
            });

            connector.Subscribe(DataRefs.Cockpit2GaugesIndicatorsVviFpmPilot, 5, (dr, value) =>
            {
                SaveData("vs", value);
            });
            connector.Start();
        }
        private void SaveData(string name, float value) {
            switch (name)
            {
                case "altitude":
                    s_pressure_altitude = value;
                    break;
                case "ias":
                    s_ias = value;
                    break;
                case "vs":
                    s_vertical_speed = value;
                    break;
            }
            
            recordedData.Add(new Data(s_pressure_altitude, s_ias, s_vertical_speed));

            var jsonString = JsonConvert.SerializeObject(recordedData, Formatting.Indented);
            File.WriteAllText(Directory.GetCurrentDirectory() +"\\data.json", jsonString);
        }

        private void stop_Click(object sender, EventArgs e)
        {
            var stringJson = File.ReadAllText(Directory.GetCurrentDirectory() + "\\data.json");
            var jsonData = JsonConvert.DeserializeObject<List<Data>>(stringJson);
            MessageBox.Show(jsonData[0].Altitude.ToString());
            var excel =jsonData.ToExcel();
            File.WriteAllBytes(Directory.GetCurrentDirectory() +"\\data.xlsx",excel);
            connector.Stop();
        }
    }
}
