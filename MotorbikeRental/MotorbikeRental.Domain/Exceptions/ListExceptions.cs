namespace MotorbikeRental.Domain.Exceptions
{
    public class ListExceptions : Exception
    {
        public List<string> Errors { get; set; }

        public ListExceptions(List<string> errors)
        {
            Errors = errors;
        }

        public override string Message
        {
            get
            {
                return string.Join(Environment.NewLine, Errors);
            }
        }
    }
}