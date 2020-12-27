namespace MachineScheduler.ViewModel
{
    public class ErrorViewModel
    {
        public string Message { get; set; }
        public string Sender { get; set; }

        public ErrorViewModel(string message)
        {
            Message = message;
            //Sender = sender;
        }
    }
}