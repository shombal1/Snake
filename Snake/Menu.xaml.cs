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
using System.Windows.Shapes;

namespace Snake
{
    /// <summary>
    /// Логика взаимодействия для Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        List<RowDefinition> ar = new List<RowDefinition>();
        List<RowStatsMap> ar2 = new List<RowStatsMap>();

        public Menu()
        {
            InitializeComponent();
        }

        public void RemoveRow(int ind)
        {
            SaveData.Default.Save();
            Update();
        }

        public void Update()
        {
            Gride.RowDefinitions.Clear();
            Gride.Children.Clear();
            ar.Clear();
            ar2.Clear();
            GridLength k = new GridLength(80, GridUnitType.Pixel);

            for (int q = 0; q < SaveData.Default.Map.Count; q++)
            {
                ar.Add(new RowDefinition());
                ar[q].Height = k;
                Gride.RowDefinitions.Add(ar[q]);
                ar2.Add(new RowStatsMap());
                ar2[q].HorizontalAlignment = HorizontalAlignment.Stretch;
                ar2[q].VerticalAlignment = VerticalAlignment.Stretch;
                Gride.Children.Add(ar2[q]);
                ar2[q].Set(Save.GetName(q), q);
                Grid.SetRow(ar2[q], q);
                ar2[q].DelRow = RemoveRow;
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Focusable = false;
            GridLength k = new GridLength(80, GridUnitType.Pixel);


            for (int q = 0; q < SaveData.Default.Map.Count; q++)
            {
                ar.Add(new RowDefinition());
                ar[q].Height = k;
                Gride.RowDefinitions.Add(ar[q]);
                ar2.Add(new RowStatsMap());
                ar2[q].HorizontalAlignment = HorizontalAlignment.Stretch;
                ar2[q].VerticalAlignment = VerticalAlignment.Stretch;
                Gride.Children.Add(ar2[q]);
                ar2[q].Set(Save.GetName(q), q);
                Grid.SetRow(ar2[q],q);
                ar2[q].DelRow = RemoveRow;
            }


            for (int q = 0; q < SaveData.Default.DefaultMap.Count; q++)
            {
                RowDefinition row = new RowDefinition();
                RowStatsMap rowStats = new RowStatsMap();
                row.Height = k;
                GrideDefault.RowDefinitions.Add(row);
                rowStats.HorizontalAlignment= HorizontalAlignment.Stretch;
                rowStats.VerticalAlignment= VerticalAlignment.Stretch;
                GrideDefault.Children.Add(rowStats);
                rowStats.Set(Save.GetDefaultName(q), q,1);
                Grid.SetRow(rowStats, q);


            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Save.FindMap(TB_Name.Text) == -1 && TB_Name.Text.Length>0)
            {
                CreateMap createMap = new CreateMap(TB_Name.Text, TaskMap.Save);
                createMap.menu = this;
                createMap.Show();
            }
            
        }
    }
}
