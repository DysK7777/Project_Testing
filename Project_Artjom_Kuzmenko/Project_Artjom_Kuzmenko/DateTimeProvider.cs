using AccessControlTests;

namespace Project_Artjom_Kuzmenko
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now { get; private set; }

        public DateTimeProvider()
        {
            Now = DateTime.Now;
        }

        public void SetTime(DateTime dateTime)
        {
            Now = dateTime;
        }
    }
}
