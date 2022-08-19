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

namespace Snake
{
    /// <summary>
    /// Логика взаимодействия для UserControl2.xaml
    /// </summary>
    public partial class RowStatsMap : UserControl
    {
        public int ind = 0;
        public RemoveRow DelRow;
        string name;

        public RowStatsMap()
        {
            InitializeComponent();
        }

        public void Set(String name,int ind,int style=0)
        {
            if (style == 0)
            {
                L_Name.Content = name;
                this.ind = ind;
            }
            else
            {
                B_Delete.Click -= B_Delete_Click;
                grid.MouseDown -= Grid_MouseDown;
                B_Delete.Visibility = Visibility.Hidden;
                B_Update.Visibility = Visibility.Hidden;
                grid.MouseDown += Grid_MouseDown_OpenDefaultMap;
                L_Name.Content = name;
                
                this.ind = ind;

            }
            this.name = name;
        }

        private void Grid_MouseDown_OpenDefaultMap(object sender, MouseButtonEventArgs e)
        {
            MainWindow b = new MainWindow(Save.GetDefaultName(ind));
            b.Focusable = true;
            b.Focus();
            b.Show();

        }
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow b = new MainWindow(Save.GetName(ind));
            b.Focusable = true;
            b.Focus();
            b.Show();

        }

        private void B_Delete_Click(object sender, RoutedEventArgs e)
        {
            SaveData.Default.Map.Remove(SaveData.Default.Map[ind]);
            DelRow(ind);
        }

        private void B_Update_Click(object sender, RoutedEventArgs e)
        {
            CreateMap a = new CreateMap(name, TaskMap.Upgrade);
            a.Show();
        }
    }
}
