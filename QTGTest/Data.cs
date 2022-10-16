namespace QTGTest
{
    public class BaseData {
        public string DateTime = $"{System.DateTime.Now:HH:mm:ss.ff}";
    }
    public class Data:BaseData
    {
        public Data(float altitude, float ias, float vs)
        {
            Altitude = altitude;
            Ias = ias;
            VerticalSpeed = vs;
        }

        public float Altitude { get; set; }
        public float Ias { get; set; }
        public float VerticalSpeed { get; set; }
    }
    public class Data1F : BaseData
    {
        public Data1F(float leftManifoldPressure, float rightManifoldPressure, float leftRpm, float rightRpm, float leftPowerLever, float rightPowerLever)
        {
            LeftManifoldPressure = leftManifoldPressure;
            RightManifoldPressure = rightManifoldPressure;
            LeftRpm = leftRpm;
            RightRpm = rightRpm;
            LeftPowerLever = leftPowerLever;
            RightPowerLever = rightPowerLever;
        }
        private float LeftManifoldPressure { get; set; }
        private float RightManifoldPressure { get; set; }
        private float LeftRpm { get; set; }
        private float RightRpm { get; set; }
        private float LeftPowerLever { get; set; }
        private float RightPowerLever { get; set; }
    }
    public class Data2Ai2 : BaseData
    {
        public Data2Ai2(float ias, float pitchControl, float columnForce)
        {
            Ias = ias;
            PitchControl = pitchControl;
            ColumnForce = columnForce;
        }
        private float Ias { get; set; }
        private float PitchControl { get; set; }
        private float ColumnForce { get; set; }
    }
    public class Data2Aii2 : BaseData
    {
        private float WheelDeflection { get; set; }
        private float WheelForce { get; set; }

        public Data2Aii2(float wheelDeflection, float wheelForce)
        {
            WheelDeflection = wheelDeflection;
            WheelForce = wheelForce;
        }
    }
    public class Data2Aiii2 : BaseData
    {
        private float _rudderPedalPosition;
        private float _rudderForce;

        public Data2Aiii2(float rudderPedalPosition, float rudderForce)
        {
            _rudderPedalPosition = rudderPedalPosition;
            _rudderForce = rudderForce;
        }
    }
}
