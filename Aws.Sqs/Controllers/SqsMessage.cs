namespace Aws.Controllers
{
    public class SqsMessage
    {
        public SqsMessage(string text)
        {
            Text = text;
        }

        public string Text { get; set; }
    }
}