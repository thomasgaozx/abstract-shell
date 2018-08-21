using System;
using static AbstractShell.ConsoleFormat;

namespace AbstractShell
{
    public abstract class BaseShell
    {
        public const string QuitKeyword = "quit";
        protected ICommandProcessor processor;
        private string default_pointer = "> ";

        public void Start()
        {
            Welcome();

            while (HandleCommand()) { }

            PrintWarning("Program exits ...");
            System.Threading.Thread.Sleep(300);
        }

        /// <summary>
        /// Shows welcome message
        /// </summary>
        protected abstract void Welcome();

        private bool HandleCommand()
        {
            Prompt(default_pointer);
            try
            {
                string raw = Console.ReadLine().Trim().ToLower();
                if (!String.IsNullOrWhiteSpace(raw))
                {
                    string[] command = raw.Split(null);
                    if (command[0].Equals(QuitKeyword))
                        return false;

                    // Use the processor to process the command otherwise
                    processor.ProcessCommand(command);
                }
            }
            catch (Exception e)
            {
                PrintWarning("An error occurred ...\n");
                PrintDanger(e.Message);
            }

            return true;
        }

        public void ResetPointer(string newPointer)
        {
            if (newPointer == null)
                return;

            default_pointer = newPointer;
        }

        #region Life Cycle

        public BaseShell(ICommandProcessor processor)
        {
            this.processor = processor;
        }

        #endregion

    }
}
