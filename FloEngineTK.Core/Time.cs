namespace FloEngineTK.Core
{
    public class Time
    {
        public TimeSpan deltaTime {  get; set; }   
        public TimeSpan time { get; set; }

        public Time()
        {
            deltaTime = TimeSpan.Zero;
            time = TimeSpan.Zero;
        }

        public Time(TimeSpan deltaTime, TimeSpan totalTime)
        {
            this.deltaTime = deltaTime;
            time = totalTime;
        }   
    }
}
