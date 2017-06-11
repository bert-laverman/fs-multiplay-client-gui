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

namespace FSMultiplay
{
    /// <summary>
    /// Interaction logic for ServerDetailsDialog.xaml
    /// </summary>
    public partial class ServerDetailsDialog : UserControl
    {
        public ServerDetailsDialog()
        {
            InitializeComponent();

            inputUrl.Text = MultiplayManager.getServerUrl();
            inputWsUrl.Text = MultiplayManager.getWsServerUrl();
            inputUsername.Text = MultiplayManager.getUsername();
            inputPassword.Password = MultiplayManager.getPassword();
            inputCallsign.Text = MultiplayManager.getCallsign();
            checkCallsign.IsChecked = MultiplayManager.isOverrideCallsign();
            inputSession.Text = MultiplayManager.getSession();
        }

        private void onOk(object sender, RoutedEventArgs e)
        {
            MultiplayManager.setServerUrl(inputUrl.Text);
            MultiplayManager.setWsServerUrl(inputWsUrl.Text);
            MultiplayManager.setUsername(inputUsername.Text);
            MultiplayManager.setPassword(inputPassword.Password);
            MultiplayManager.setCallsign(inputCallsign.Text);
            MultiplayManager.setOverrideCallsign((checkCallsign.IsChecked.GetValueOrDefault(false) ? 1 : 0));
            MultiplayManager.setSession(inputSession.Text);
        }
    }
}
