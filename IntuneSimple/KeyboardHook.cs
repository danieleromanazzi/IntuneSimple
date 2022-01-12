using System.Collections;
using System.Threading;
using System.Windows.Input;

namespace IntuneSimple
{
    public static class KeyboardHook
    {
        public static void Start(Credential credential)
        {
            System.Windows.Clipboard.SetText(credential.UserName);

            var queue = new Queue();
            queue.Enqueue(credential.Password);

            var listener = new Thread(() =>
            {
                while (queue.Count > 0)
                {
                    Thread.Sleep(40);
                    
                    if ((Keyboard.GetKeyStates(Key.LeftCtrl) & KeyStates.Down) > 0 &&
                        (Keyboard.GetKeyStates(Key.V) & KeyStates.Down) > 0 &&
                        queue.Dequeue() is string value)
                    {
                        System.Windows.Clipboard.SetText(value);
                    }
                }
            });

            listener.SetApartmentState(ApartmentState.STA);
            listener.Start();
        }
    }
}
