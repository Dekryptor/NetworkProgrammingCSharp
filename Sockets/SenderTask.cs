using System;

namespace TaskParallelization
{
    class SenderTask
    {
        private readonly string _input;
        //private readonly ErrorLogger _errorLogger;

        //public SenderTask(string input, ErrorLogger errorLogger)
        public SenderTask(string input)
        {
            _input = input;
            //_errorLogger = errorLogger;
        }
        
        public bool Do()
        {
            try
            {
                Console.WriteLine("- Sending {0}", _input);
                
                return true;
            }
            catch (Exception e)
            {
                //_errorLogger.Log(String.Format("- An error sending {0}: {1}", _input, e.Message));
                
                return false;
            }
        }
    }
}