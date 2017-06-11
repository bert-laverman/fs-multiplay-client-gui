using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using MaterialDesignThemes.Wpf;

namespace FSMultiplay
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("MultiPlayDll.dll", EntryPoint = "srvmgr_start")]
        public static extern void srvmgr_start();

        private FsxManager.CsSimConnectEventHandler sceHandler;
        private MultiplayManager.CsMultiplayStatusHandler mpStHandler;
        private MultiplayManager.CsMultiplayDataChangedHandler mpDataHandler;

        private void setSimulatorNone()
        {
            txtSimSummary.Text = "Simulator not running";
            imgNoSim.Visibility = Visibility.Visible;
            imgFsx.Visibility = Visibility.Collapsed;
            imgFsxSe.Visibility = Visibility.Collapsed;
            imgP3d.Visibility = Visibility.Collapsed;
        }

        private void setSimulatorFsx()
        {
            txtSimSummary.Text = "Connected to FSX";
            imgNoSim.Visibility = Visibility.Collapsed;
            imgFsx.Visibility = Visibility.Visible;
            imgFsxSe.Visibility = Visibility.Collapsed;
            imgP3d.Visibility = Visibility.Collapsed;
        }

        private void setSimulatorFsxSe()
        {
            txtSimSummary.Text = "Connected to FSX-SE";
            imgNoSim.Visibility = Visibility.Collapsed;
            imgFsx.Visibility = Visibility.Collapsed;
            imgFsxSe.Visibility = Visibility.Visible;
            imgP3d.Visibility = Visibility.Collapsed;
        }

        private void setSimulatorP3d()
        {
            txtSimSummary.Text = "Connected to Prepar3D";
            imgNoSim.Visibility = Visibility.Collapsed;
            imgFsx.Visibility = Visibility.Collapsed;
            imgFsxSe.Visibility = Visibility.Collapsed;
            imgP3d.Visibility = Visibility.Visible;
        }

        private void setSimulator()
        {
            if (FsxManager.isConnected())
            {
                string name = FsxManager.getSimName();
                if (name.Equals("Microsoft Flight Simulator X"))
                {
                    int build = FsxManager.getSimBuildMajor();
                    if ((build == 62608) || (build == 62613) || (build == 62615))
                    {
                        setSimulatorFsxSe();
                    }
                    else
                    {
                        setSimulatorFsx();
                    }
                }
                else
                {
                    setSimulatorP3d();
                }

                txtSimName.Text = name;
                txtSimName.Foreground = Brushes.Black;
                txtSimVersion.Text = "" + FsxManager.getSimVersionMajor() + "." + FsxManager.getSimVersionMinor();
                txtSimVersion.Foreground = Brushes.Black;
                txtSimBuild.Text = "" + FsxManager.getSimBuildMajor() + "." + FsxManager.getSimBuildMinor();
                txtSimBuild.Foreground = Brushes.Black;
                txtSimConVersion.Text = "" + FsxManager.getLibVersionMajor() + "." + FsxManager.getLibVersionMinor();
                txtSimConVersion.Foreground = Brushes.Black;
                txtSimConBuild.Text = "" + FsxManager.getLibBuildMajor() + "." + FsxManager.getLibBuildMinor();
                txtSimConBuild.Foreground = Brushes.Black;
            }
            else
            {
                setSimulatorNone();

                txtSimName.Text = "(simulator)";
                txtSimName.Foreground = Brushes.LightGray;
                txtSimVersion.Text = "(version)";
                txtSimVersion.Foreground = Brushes.LightGray;
                txtSimBuild.Text = "(build)";
                txtSimBuild.Foreground = Brushes.LightGray;
                txtSimConVersion.Text = "(version)";
                txtSimConVersion.Foreground = Brushes.LightGray;
                txtSimConBuild.Text = "(build)";
                txtSimConBuild.Foreground = Brushes.LightGray;
            }
        }

        private void setAircraft()
        {
            if (FsxManager.isConnected())
            {
                txtCallsign.Text = MultiplayManager.isOverrideCallsign() ? MultiplayManager.getCallsign() : FsxManager.getAtcId();
                txtCallsign.Foreground = Brushes.Black;
                txtTitle.Text = FsxManager.getTitle();
                txtTitle.Foreground = Brushes.Black;

                txtAtcId.Text = FsxManager.getAtcId();
                txtAtcId.Foreground = Brushes.Black;
                txtAtcType.Text = FsxManager.getAtcType();
                txtAtcType.Foreground = Brushes.Black;
                txtAtcModel.Text = FsxManager.getAtcModel();
                txtAtcModel.Foreground = Brushes.Black;
                txtAtcAirline.Text = FsxManager.getAtcAirline();
                txtAtcAirline.Foreground = Brushes.Black;
                txtAtcFlightNumber.Text = FsxManager.getAtcFlightnumber();
                txtAtcFlightNumber.Foreground = Brushes.Black;
            }
            else
            {
                txtCallsign.Text = "(callsign)";
                txtCallsign.Foreground = Brushes.LightGray;
                txtTitle.Text = "(aircraft title)";
                txtTitle.Foreground = Brushes.LightGray;

                txtAtcId.Text = "(callsign)";
                txtAtcId.Foreground = Brushes.LightGray;
                txtAtcType.Text = "(type)";
                txtAtcType.Foreground = Brushes.LightGray;
                txtAtcModel.Text = "(model)";
                txtAtcModel.Foreground = Brushes.LightGray;
                txtAtcAirline.Text = "(airline)";
                txtAtcAirline.Foreground = Brushes.LightGray;
                txtAtcFlightNumber.Text = "(flightnumber)";
                txtAtcFlightNumber.Foreground = Brushes.LightGray;
            }
        }

        private void setMultiplay()
        {
            txtUsername.Text = MultiplayManager.getUsername();
            txtMpCallsign.Text = MultiplayManager.getCallsign();
            txtUrl.Text = MultiplayManager.getServerUrl();
            txtWsUrl.Text = MultiplayManager.getWsServerUrl();
        }

        private void mainSceHandler(int iEvent)
        {
            string msg = "Something happened";
            switch (iEvent) {
                case 0: // SCE_CONNECT
                    msg = "Connected to simulator";

                    this.Dispatcher.Invoke(() =>
                    {
                        setSimulator();
                        setAircraft();
                    });
                    break;

                case 1: // SCE_DISCONNECT
                    msg = "Disconnected from simulator";

                    this.Dispatcher.Invoke(() =>
                    {
                        setSimulator();
                        setAircraft();
                    });
                    break;

                case 2: // SCE_START
                    msg = "Simulator engine started";
                    break;
                case 3: // SCE_STOP
                    msg = "Simulator engine stopped";
                    break;
                case 4: // SCE_PAUSE
                    msg = "Simulator engine paused";
                    break;
                case 5: // SCE_UNPAUSE
                    msg = "Simulator engine continued";
                    break;
                case 6: // SCE_AIRCRAFTLOADED
                    msg = "Loaded aircraft model";

                    this.Dispatcher.Invoke(() =>
                    {
                        setAircraft();
                    });
                    break;
//                case 7: // SCE_OBJECTADDED
//                    msg = "AI object added";
//                    break;
//                case 8: // SCE_OBJECTREMOVED
//                    msg = "AI object removed";
//                    break;
                default:
                    break;
            }
            this.Dispatcher.Invoke(() =>
            {
                MainSnackbar.MessageQueue.Enqueue(msg);
            });
        }

        private void mainMpStatusHandler(int iEvent)
        {
            this.Dispatcher.Invoke(() =>
            {
                switch (iEvent)
                {
                    case 1: // MPSTAT_DISCONNECTED
                        txtServerStatus.Text = "Not logged in to server.";
                        txtLiveStatus.Text = "No live data.";
                        btn_MpConnect.IsEnabled = true;
                        btn_MpDisconnect.IsEnabled = false;
                        break;
                    case 2: // MPSTAT_CONNECTING
                        txtServerStatus.Text = "Connecting to server.";
                        txtLiveStatus.Text = "No live data.";
                        btn_MpConnect.IsEnabled = false;
                        btn_MpDisconnect.IsEnabled = true;
                        break;
                    case 3: // MPSTAT_WS_CONNECTING
                        txtServerStatus.Text = "Connected to server.";
                        txtLiveStatus.Text = "Connecting to live data.";
                        btn_MpConnect.IsEnabled = false;
                        btn_MpDisconnect.IsEnabled = true;
                        break;
                    case 4: // MPSTAT_CONNECTED
                        txtServerStatus.Text = "Connected to server.";
                        txtLiveStatus.Text = "Connected to live data.";
                        btn_MpConnect.IsEnabled = false;
                        btn_MpDisconnect.IsEnabled = true;
                        break;
                    case 5: // MPSTAT_CLOSING
                        txtServerStatus.Text = "Closing server connectiuon.";
                        txtLiveStatus.Text = "Closing live data.";
                        btn_MpConnect.IsEnabled = false;
                        btn_MpDisconnect.IsEnabled = true;
                        break;
                    case 6: // MPSTAT_SYNC
                        txtServerStatus.Text = "Updating session data.";
                        txtLiveStatus.Text = "No live data.";
                        btn_MpConnect.IsEnabled = false;
                        btn_MpDisconnect.IsEnabled = true;
                        break;
                    case 7: // MPSTAT_ERROR
                        txtServerStatus.Text = "Got error from server.";
                        txtLiveStatus.Text = "No live data.";
                        btn_MpConnect.IsEnabled = false;
                        btn_MpDisconnect.IsEnabled = true;
                        break;
                    default:
                        break;
                }
            });
        }

        private void mainMpDataChangedHandler()
        {
            this.Dispatcher.Invoke(() =>
            {
                setMultiplay();
            });
        }

        public void settingsChanged()
        {
            setSimulator();
            setAircraft();
            setMultiplay();
        }

        public MainWindow()
        {
            InitializeComponent();

            sceHandler = new FsxManager.CsSimConnectEventHandler(mainSceHandler);
            FsxManager.addSimconnectHandler(sceHandler);
            mpStHandler = new MultiplayManager.CsMultiplayStatusHandler(mainMpStatusHandler);
            MultiplayManager.addStatusHandler(mpStHandler);
            mpDataHandler = new MultiplayManager.CsMultiplayDataChangedHandler(mainMpDataChangedHandler);
            MultiplayManager.addDataChangedHandler(mainMpDataChangedHandler);

            srvmgr_start();

            setSimulator();
            setAircraft();
            setMultiplay();
        }

        private void menu_MultiplayConnect(object sender, RoutedEventArgs e)
        {
            MultiplayManager.connect();
        }

        private void menu_MultiplayDisconnect(object sender, RoutedEventArgs e)
        {
            MultiplayManager.disconnect();
        }

        private void menu_Quit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private async void btn_ServerDetails_Click(object sender, RoutedEventArgs e)
        {
            MenuToggleButton.IsChecked = false;

            var serverDetailsDialog = new ServerDetailsDialog();

            await DialogHost.Show(serverDetailsDialog, "RootDialog");
        }
    }
}
