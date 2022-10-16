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

namespace QTGTest
{
    public partial class Form1 : Form
    {
        XPlaneConnector.XPlaneConnector connector;

        List<Data> recordedData;
        private float altitud, ias, vs;
        public Form1()
        {
            InitializeComponent();
            recordedData = new List<Data>();
            //Buraya kasanin IP adresi
            connector = new XPlaneConnector.XPlaneConnector("127.0.0.1");
            //true_airspeed
            connector.Subscribe(DataRefs.CockpitPressureCabinAltitudeActualMMsl, 5, (e, v) =>
            {
                this.Invoke((MethodInvoker)delegate {saveData("altitude", v);});

            });
            connector.Subscribe(DataRefs.Cockpit2GaugesIndicatorsAirspeedKtsPilot, 5, (e, v) =>
            {
                this.Invoke((MethodInvoker)delegate { saveData("ias", v);  });
            });

            connector.Subscribe(DataRefs.Cockpit2GaugesIndicatorsVviFpmPilot, 5, (e, v) =>
            {
                this.Invoke((MethodInvoker)delegate { saveData("vs", v); });
            });
            connector.Start();

        }
        private void saveData(string name, float value) {
            switch (name)
            {
                case "altitude":
                    altitud = value;
                    break;
                case "ias":
                    ias = value;
                    break;
                case "vs":
                    vs = value;
                    break;
                default:
                    break;
            }
            recordedData.Add(new Data(altitud, ias, vs));

            var jsonString = JsonConvert.SerializeObject(recordedData);
            File.WriteAllText(Directory.GetCurrentDirectory() +"data.json", jsonString);
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
