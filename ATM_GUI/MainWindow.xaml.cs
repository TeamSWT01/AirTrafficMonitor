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
using AirTrafficMonitor.Implementation;
using AirTrafficMonitor.Interfaces;
using Decoder = AirTrafficMonitor.Implementation.Decoder;
using System.Collections.Generic;

namespace ATM_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<ITrack> tracks;
        private List<ITrack> tracksEntered;
        private List<ITrack> tracksLeaving;
        public MainWindow()
        {
            InitializeComponent();

            IDecoder program = new Decoder();
            tracks = new List<ITrack>();
            tracksEntered = new List<ITrack>();
            tracksLeaving = new List<ITrack>();

            program.OnTracksReady += OnTracksReadyHandler;
            program.TrackEntered += TrackEntered;
            program.TrackLeaving += TrackLeaving;
        }

        private void TrackLeaving(ITrack track)
        {
            tracksLeaving.Add(track);
        }

        private void TrackEntered(ITrack track)
        {
            tracksEntered.Add(track);
        }

        private void OnTracksReadyHandler(List<ITrack> _tracks)
        {
            List<ITrack> tempList = new List<ITrack>(tracksEntered);
            foreach (var track in tempList)
            {
                var result = DateTime.Now - track.TimeStamp;
                if (result.TotalSeconds > 5)
                {
                    tracksEntered.Remove(track);
                }
            }

            tempList = new List<ITrack>(tracksLeaving);
            foreach (var track in tempList)
            {
                var result = DateTime.Now - track.TimeStamp;
                if (result.TotalSeconds > 5)
                {
                    tracksLeaving.Remove(track);
                }
            }

            tracks = new List<ITrack>(_tracks);
            
            Application.Current.Dispatcher.Invoke(new Action(() => {
                DataGrid.ItemsSource = this.tracks; DataGrid.Items.Refresh();
                DataGridEntered.ItemsSource = tracksEntered; DataGridEntered.Items.Refresh();
                DataGridLeaving.ItemsSource = tracksLeaving; DataGridLeaving.Items.Refresh();
            }));
        }
    }
}
