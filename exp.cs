bool isRunning = true;
        public MainWindow()
        {
            InitializeComponent();
            var listener = new Thread(Listner);
            listener.SetApartmentState(ApartmentState.STA);
            listener.Start();
        }

        [STAThread]
        void Listner()
        {
            while (isRunning)
            {
                Thread.Sleep(40);
                if ((Keyboard.GetKeyStates(Key.LeftCtrl) & KeyStates.Down) > 0 &&
                    (Control.MouseButtons & MouseButtons.Left) == MouseButtons.Left)
                {
                    System.Windows.Clipboard.SetText("your Text");
                    //Dispatcher.Invoke(() => { SendKeys.Send("^V"); }, System.Windows.Threading.DispatcherPriority.Normal);
                    
                }
            }
        }
